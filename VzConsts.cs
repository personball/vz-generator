namespace vz_generator;
public static class VzConsts
{
    // should not hard code '/' or '\'
    public const string ConfigRoot = ".vzx";

    public static class GenerateCmd
    {
        public const string CmdName = "generate";
        public const string SettingFileName = CmdName + ".settings.json";
        public const string SettingSchemaFileName = CmdName + ".settings.schema.json";

    }
}