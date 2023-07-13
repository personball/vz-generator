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

    public static class GenerateCmd
    {
        public const string CmdName = "generate";
        public const string SettingFileName = CmdName + ".settings.json";
        public const string SettingSchemaFileName = CmdName + ".settings.schema.json";

    }
}