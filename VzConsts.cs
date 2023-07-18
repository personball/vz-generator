namespace vz_generator;
public static class VzConsts
{
    // should not hard code '/' or '\'
#if DEBUG
    public const string ConfigRoot = ".vzx";
#else
    public const string ConfigRoot = ".vz";
#endif

    public const string TemplateRoot = "templates";

    public const string SampleRoot = "samples";

    public static class GenerateCmd
    {
        public const string Name = "generate";
        public const string SettingFileName = Name + ".settings.json";
        public const string SettingSchemaFileName = Name + ".settings.schema.json";
    }

    public static class InitCmd
    {
        public const string Name = "init";
    }
}