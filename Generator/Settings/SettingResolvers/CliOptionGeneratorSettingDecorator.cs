using vz_generator.Commands;
using vz_generator.Commands.Settings;

namespace vz_generator.Generator.Settings.SettingResolvers
{
    // options can override settings from file
    public class CliOptionGeneratorSettingDecorator : IGeneratorSettingResolver
    {
        private readonly IGeneratorSettingResolver _inner;

        public CliOptionGeneratorSettingDecorator(IGeneratorSettingResolver inner)
        {
            _inner = inner;
        }

        public async Task ResolveAsync(ResolveContext context)
        {
            var configOpt = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.ConfigOpt);
            if (configOpt != null)
            {
                if (!configOpt.Exists)
                {
                    throw new ArgumentNullException(nameof(configOpt), $"{configOpt.FullName} Not Exists!");
                }

                context.SettingFilePath = configOpt;
            }

            context.SelectedOption = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.SelectOpt);

            await _inner.ResolveAsync(context);

            // override by cli options

            // variables merge 同名覆盖，不存在则添加
            var vars = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.VarStringOpt);
            foreach (var item in vars)
            {
                var entry = context.Result.Variables.FirstOrDefault(v => v.Type == TemplateVariableType.String && v.Name == item.Key);
                if (entry == null)
                {
                    context.Result.Variables.Add(
                        new TemplateVariable
                        {
                            Name = item.Key,
                            DefaultValue = item.Value,
                            Type = TemplateVariableType.String
                        });
                }
                else
                {
                    entry.DefaultValue = item.Value;
                }
            }

            var jsons = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.VarJsonFileOpt);
            foreach (var item in jsons)
            {
                var entry = context.Result.Variables.FirstOrDefault(v => v.Type == TemplateVariableType.JsonFile && v.Name == item.Key);
                if (entry == null)
                {
                    context.Result.Variables.Add(
                        new TemplateVariable
                        {
                            Name = item.Key,
                            DefaultValue = item.Value.FullName,
                            Type = TemplateVariableType.JsonFile
                        }
                    );
                }
                else
                {
                    entry.DefaultValue = item.Value.FullName;
                }
            }

            // templatePath 有则覆盖
            var tplPath = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.TplPathOpt);
            if (tplPath != null)
            {
                context.Result.TemplatePath = tplPath.FullName;
            }

            // output 有则覆盖
            var output = context.InvocationContext.ParseResult.GetValueForOption(GenerateCommand.OutputOpt);
            if (output != null)
            {
                context.Result.Output = output.FullName;
            }
        }
    }
}