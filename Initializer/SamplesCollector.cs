using vz_generator.Commands.Settings;

namespace vz_generator.Initializer;
public static class SamplesCollector
{
    // 嵌入式资源，维护模板路径和对应的 setting
    public static List<Example> GetExamples()
    {
        var samples = new List<Example>();
        // vue-pinia
        samples.AddVuePinia();
        // abp
        samples.AddAbp();

        return samples;
    }

    public static void AddVuePinia(this List<Example> samples)
    {
        var setting = new GeneratorSetting
        {
            Option = "Create Pinia",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                "vue.pinia",
                "{{store}}.js"
            ),
            Output = Path.Combine(
                ".",
                "src",
                "pinia",
                "modules",
                "{{store|camelCase}}.js")
        };

        setting.Variables.Add(new TemplateVariable { Name = "store", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "model", Type = TemplateVariableType.String });

        samples.Add(new Example
        {
            Name = "vue.pinia",
            Setting = setting
        });
    }

    public static void AddAbp(this List<Example> samples)
    {
        var setting = new GeneratorSetting
        {
            Option = "Create Abp",
            TemplatePath = Path.Combine(
               ".",
               VzConsts.ConfigRoot,
               VzConsts.TemplateRoot,
               VzConsts.SampleRoot,
               "abp"
           ),
            Output = Path.Combine(
               ".",
               "src")
        };

        setting.Variables.Add(new TemplateVariable { Name = "project", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "entity", Type = TemplateVariableType.String });

        samples.Add(new Example
        {
            Name = "abp",
            Setting = setting
        });
    }
}