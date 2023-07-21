using System.Text.Json;

using Sharprompt;

using vz_generator.Commands.Settings;
using vz_generator.Localization;

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
            throw new Exception(
                VzLocales.L(
                    VzLocales.Keys.GSettingFileNotFound,
                    $"{VzConsts.ConfigRoot}{Path.DirectorySeparatorChar}{VzConsts.GenerateCmd.SettingFileName}"));
        }

        // load settings from file
        context.FileSettings = JsonSerializer.Deserialize<List<GeneratorSetting>>(
            await File.ReadAllTextAsync(filePath),
            new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? new List<GeneratorSetting>();

        if (!context.FileSettings.Any())
        {
            throw new Exception(
                VzLocales.L(
                    VzLocales.Keys.GSettingFileContentIsEmpty,
                    $"{VzConsts.ConfigRoot}{Path.DirectorySeparatorChar}{VzConsts.GenerateCmd.SettingFileName}"));
        }

        if (!string.IsNullOrWhiteSpace(context.SelectedOption)
            && context.FileSettings.FirstOrDefault(s => s.Option == context.SelectedOption) == null)
        {
            context.InvocationContext.Console.Out.Write(
                VzLocales.L(
                    VzLocales.Keys.GSettingOptionNotFound, context.SelectedOption));

            context.SelectedOption = null;
        }

        // which option?
        if (string.IsNullOrWhiteSpace(context.SelectedOption))
        {
            context.SelectedOption = Prompt.Select(
                VzLocales.L(VzLocales.Keys.GSettingOptionChoosePrompt),
                context.FileSettings.Select(f => f.Option));
        }

        context.Result = context.FileSettings.FirstOrDefault(s => s.Option == context.SelectedOption);
        if (context.Result == null)
        {
            throw new Exception(
                VzLocales.L(
                    VzLocales.Keys.GSettingOptionNotFoundError, context.SelectedOption));
        }
    }
}
