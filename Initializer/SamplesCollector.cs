using vz_generator.Commands.Settings;

namespace vz_generator.Initializer;
public static class SamplesCollector
{
    // 嵌入式资源，维护模板路径和对应的 setting
    public static IEnumerable<Example> GetExamples()
    {
        yield return AddVuePinia();
        yield return AddAbp();
        yield return AddK8s();
        yield return AddSwagger2Api();
        // yield return AddSwagger2View();
    }

    // one sample, multi-options
    public static Example AddK8s()
    {
        var k8sSample = new Example
        {
            Name = "k8s"
        };

        // option 1 
        var setting = new GeneratorSetting
        {
            Option = "K8S-DeployWithService",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                "k8s",
                "{{name___kebab_case}}-deployment.yaml"
            ),
            Output = Path.Combine(
                ".",
                "output",
                "k8s",
                "{{name___kebab_case}}")
                .EnsureEndsWithDirectorySeparatorChar()
        };

        setting.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "namespace", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "image", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "port", Type = TemplateVariableType.String });

        k8sSample.Settings.Add(setting);

        // option 2
        var setting2 = new GeneratorSetting
        {
            Option = "K8S-Ingress",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                "k8s",
                "{{name___kebab_case}}-ing.yaml"
            ),
            Output = Path.Combine(
                ".",
                "output",
                "k8s",
                "{{name___kebab_case}}")
                .EnsureEndsWithDirectorySeparatorChar()
        };

        setting2.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting2.Variables.Add(new TemplateVariable { Name = "namespace", Type = TemplateVariableType.String });
        setting2.Variables.Add(new TemplateVariable { Name = "host", Type = TemplateVariableType.String });
        setting2.Variables.Add(new TemplateVariable { Name = "clusterIssuer", Type = TemplateVariableType.String });
        k8sSample.Settings.Add(setting2);

        return k8sSample;
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
            Settings = new List<GeneratorSetting> { setting }
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
            Settings = new List<GeneratorSetting> { setting }
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
               "swagger2api",
               "webapiclient.sbn"
           ),
            Output = Path.Combine(
               ".",
               "output",
               "{{host}}Apis.cs")
        };

        setting.Variables.Add(new TemplateVariable { Name = "host", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "namespace", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable
        {
            Name = "swagger",
            Type = TemplateVariableType.JsonFile,
            DefaultValue = Path.Combine(
               ".",
               VzConsts.ConfigRoot,
               VzConsts.TemplateRoot,
               VzConsts.SampleRoot,
               "swagger2api",
               "openapi3.json"
            )
        });

        return new Example
        {
            Name = "swagger2api",
            Settings = new List<GeneratorSetting> { setting }
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
            Settings = new List<GeneratorSetting> { setting }
        };
    }
}