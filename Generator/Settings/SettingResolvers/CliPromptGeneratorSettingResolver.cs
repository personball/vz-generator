using Sharprompt;

using vz_generator.Commands.Settings;
using vz_generator.Localization;

namespace vz_generator.Generator.Settings.SettingResolvers
{
    // prompt should collect missing settings
    public class CliPromptGeneratorSettingResolver : IGeneratorSettingResolver
    {
        private readonly IGeneratorSettingResolver _inner;
        public CliPromptGeneratorSettingResolver(IGeneratorSettingResolver inner)
        {
            _inner = inner;
        }

        public async Task ResolveAsync(ResolveContext context)
        {
            await _inner.ResolveAsync(context);

            if (string.IsNullOrWhiteSpace(context.Result.TemplatePath))
            {
                context.Result.TemplatePath = Prompt.Input<string>(
                    VzLocales.L(VzLocales.Keys.GSettingTemplatePathCliPrompt));
            }

            foreach (var item in context.Result.Variables.Where(v => string.IsNullOrWhiteSpace(v.DefaultValue)))
            {
                if (item.Type == TemplateVariableType.String)
                {
                    item.DefaultValue = Prompt.Input<string>(
                        VzLocales.L(VzLocales.Keys.GSettingTemplateVariableValueCliPrompt, item.Name));
                }
                else
                {
                    item.DefaultValue = Prompt.Input<string>(
                        VzLocales.L(VzLocales.Keys.GSettingTemplateVariableValueForJsonFileCliPrompt, item.Name));
                }
            }

            if (string.IsNullOrWhiteSpace(context.Result.Output))
            {
                context.Result.Output = Prompt.Input<string>(
                    message: VzLocales.L(VzLocales.Keys.GSettingOutputCliPrompt),
                    defaultValue: "./output",
                    placeholder: "./output");
            }
        }
    }
}