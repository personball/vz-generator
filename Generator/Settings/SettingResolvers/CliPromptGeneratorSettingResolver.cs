using Sharprompt;
using vz_generator.Commands.Settings;

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
                context.Result.TemplatePath = Prompt.Input<string>($"Path of Template?");
            }

            foreach (var item in context.Result.Variables.Where(v => string.IsNullOrWhiteSpace(v.DefaultValue)))
            {
                if (item.Type == TemplateVariableType.String)
                {
                    item.DefaultValue = Prompt.Input<string>($"Set value of {item.Name}");
                }
                else
                {
                    item.DefaultValue = Prompt.Input<string>($"Set File Path of {item.Name}");
                }
            }

            if (string.IsNullOrWhiteSpace(context.Result.Output))
            {
                context.Result.Output = Prompt.Input<string>(
                    message: $"Path of Output?",
                    defaultValue: "./output",
                    placeholder: "./output");
            }
        }
    }
}