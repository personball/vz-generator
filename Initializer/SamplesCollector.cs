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
        yield return AddGitlab();
        // yield return AddSwagger2View();
    }

    private static Example AddGitlab()
    {
        var sample = new Example
        {
            Name = "gitlab.dnd-ci"
        };

        // option 1 
        var setting = new GeneratorSetting
        {
            Option = "Create gitlab-ci.yml",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                sample.Name
            ),
            Output = Path.Combine(
                ".")
                .EnsureEndsWithDirectorySeparatorChar()
        };

        // liquid variables: name namespace registry project tag context
        setting.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "namespace", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "registry", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "project", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "context", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "tag", Type = TemplateVariableType.String, DefaultValue = "latest" });
        setting.Variables.Add(new TemplateVariable { Name = "env", Type = TemplateVariableType.String, DefaultValue = "staging" });

        sample.Settings.Add(setting);

        return sample;
    }

    // one sample, multi-options
    public static Example AddK8s()
    {
        var k8sSample = new Example
        {
            Name = "k8s"
        };

        k8sSample.Settings.Add(CreateK8sDeploySvcOption(k8sSample.Name));
        k8sSample.Settings.Add(CreateK8sIngressOption(k8sSample.Name));
        k8sSample.Settings.Add(CreateK8sInitWebsiteOption(k8sSample.Name));

        return k8sSample;
    }

    private static GeneratorSetting CreateK8sInitWebsiteOption(string sampleName)
    {
        var setting = new GeneratorSetting
        {
            Option = "K8S-InitWebsite",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                sampleName,
                "init-website"
            ),
            Output = Path.Combine(
                ".")
                .EnsureEndsWithDirectorySeparatorChar()
        };

        setting.Variables.Add(new TemplateVariable { Name = "name", Type = TemplateVariableType.String });
        setting.Variables.Add(new TemplateVariable { Name = "baseImage", Type = TemplateVariableType.String, DefaultValue = "nginx:1.14.1" });

        return setting;
    }

    private static GeneratorSetting CreateK8sIngressOption(string sampleName)
    {
        var setting = new GeneratorSetting
        {
            Option = "K8S-Ingress",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                sampleName,
                "{{name___kebab_case}}-ing.yaml"
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
        setting.Variables.Add(new TemplateVariable { Name = "clusterIssuer", Type = TemplateVariableType.String, DefaultValue = "zerossl" });
        setting.Variables.Add(new TemplateVariable { Name = "host", Type = TemplateVariableType.String, DefaultValue = "example.com" });

        return setting;
    }

    private static GeneratorSetting CreateK8sDeploySvcOption(string sampleName)
    {
        var setting = new GeneratorSetting
        {
            Option = "K8S-DeployWithService",
            TemplatePath = Path.Combine(
                ".",
                VzConsts.ConfigRoot,
                VzConsts.TemplateRoot,
                VzConsts.SampleRoot,
                sampleName,
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
        setting.Variables.Add(new TemplateVariable { Name = "port", Type = TemplateVariableType.String, DefaultValue = "80" });

        return setting;
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