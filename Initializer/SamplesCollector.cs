using vz_generator.Commands.Settings;

namespace vz_generator.Initializer;
public static class SamplesCollector
{
    // 嵌入式资源，维护模板路径和对应的 setting
    public static IEnumerable<Example> GetExamples()
    {
        yield return AddVuePinia();
        yield return AddAbp();
        // yield return AddSwagger2Api();
        // yield return AddSwagger2View();
    }

    public static Example AddVuePinia()
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

        return new Example
        {
            Name = "vue.pinia",
            Setting = setting
        };
    }

    public static Example AddAbp()
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
               .EnsureEndsWithDirectorySeparatorChar()
        };

        setting.Variables.Add(new TemplateVariable { Name = "project", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "entity", Type = TemplateVariableType.String });

        return new Example
        {
            Name = "abp",
            Setting = setting
        };
    }

    public static Example AddSwagger2Api()
    {
        var setting = new GeneratorSetting
        {
            Option = "Swagger2Api",
            TemplatePath = Path.Combine(
               ".",
               VzConsts.ConfigRoot,
               VzConsts.TemplateRoot,
               VzConsts.SampleRoot,
               "swagger2api"
           ),
            Output = Path.Combine(
               ".",
               "src",
               "apis",
               "modules",
               "{{name}}.js")
        };

        setting.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "module", Type = TemplateVariableType.String });

        return new Example
        {
            Name = "swagger2api",
            Setting = setting
        };
    }

    public static Example AddSwagger2View()
    {
        var setting = new GeneratorSetting
        {
            Option = "Swagger2View",
            TemplatePath = Path.Combine(
               ".",
               VzConsts.ConfigRoot,
               VzConsts.TemplateRoot,
               VzConsts.SampleRoot,
               "swagger2view"
           ),
            Output = Path.Combine(
               ".",
               "src",
               "view",
               "{{module}}",
               "{{name}}.vue")
        };

        setting.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "module", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable
        {
            Name = "swagger",
            Type = TemplateVariableType.JsonFile,
            DefaultValue = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                "swagger2view",
                "swagger.json"
            )
        });

        return new Example
        {
            Name = "swagger2view",
            Setting = setting
        };
    }

    private static string EnsureEndsWithDirectorySeparatorChar(this string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path), "path should not empty!");
        }

        if (Path.EndsInDirectorySeparator(path))
        {
            return path;
        }
        else
        {
            return path + Path.DirectorySeparatorChar;
        }
    }
}