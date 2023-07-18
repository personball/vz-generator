using System.Text.Json;
using Sharprompt;
using vz_generator.Commands.Settings;

namespace vz_generator.Generator.Settings.SettingResolvers;

public class DefaultGeneratorSettingResolver : IGeneratorSettingResolver
{
    public async Task ResolveAsync(ResolveContext context)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, VzConsts.ConfigRoot, VzConsts.GenerateCmd.SettingFileName);
        if (context.SettingFilePath != null)
        {
            filePath = context.SettingFilePath.FullName;
        }

        if (!File.Exists(filePath))
        {
            // 默认路径没有配置文件
            throw new Exception($"{VzConsts.ConfigRoot}{Path.DirectorySeparatorChar}{VzConsts.GenerateCmd.SettingFileName} Not Found! Please run init first!");
        }

        // load settings from file
        context.FileSettings = JsonSerializer.Deserialize<List<GeneratorSetting>>(
            await File.ReadAllTextAsync(filePath),
            new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? new List<GeneratorSetting>();

        if (!context.FileSettings.Any())
        {
            throw new Exception(
                $"Settings Not Found, please update {VzConsts.ConfigRoot}{Path.DirectorySeparatorChar}{VzConsts.GenerateCmd.SettingFileName}");
        }

        if (!string.IsNullOrWhiteSpace(context.SelectedOption)
            && context.FileSettings.FirstOrDefault(s => s.Option == context.SelectedOption) == null)
        {
            context.InvocationContext.Console.Out.Write($"WARN: option <{context.SelectedOption}> Not Found...");
            context.SelectedOption = null;
        }

        // which option?
        if (string.IsNullOrWhiteSpace(context.SelectedOption))
        {
            context.SelectedOption = Prompt.Select("Choose One (Use ↑↓)", context.FileSettings.Select(f => f.Option));
        }

        context.Result = context.FileSettings.FirstOrDefault(s => s.Option == context.SelectedOption);
        if (context.Result == null)
        {
            throw new Exception($"Option <{context.SelectedOption}> Not Found!");
        }
    }
}
