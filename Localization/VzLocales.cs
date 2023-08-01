using System.Resources;

namespace vz_generator.Localization;
public static class VzLocales
{
    public const string ResourceName = "vz_generator.Localization.Resources.VzLocales";
    public static ResourceManager Instance = new ResourceManager(ResourceName, typeof(VzLocales).Assembly);

    public static class Keys
    {
        public const string MyName = nameof(MyName);
        public const string RootCommandDesc = nameof(RootCommandDesc);

        // Initializer
        public const string InitCommandDesc = nameof(InitCommandDesc);
        public const string InitOptListSampleDesc = nameof(InitOptListSampleDesc);
        public const string InitOptWithSampleDesc = nameof(InitOptWithSampleDesc);
        public const string JsonDeserializeFailError = nameof(JsonDeserializeFailError);
        public const string InitSettingOptionExistsSkipWarn = nameof(InitSettingOptionExistsSkipWarn);

        public const string VsCodeSettingsFileExistsPrompt = nameof(VsCodeSettingsFileExistsPrompt);

        // Renamer
        public const string RenameCommandDesc = nameof(RenameCommandDesc);
        public const string RTargetArgDesc = nameof(RTargetArgDesc);
        public const string ROptSkipContentOptDesc = nameof(ROptSkipContentOptDesc);
        public const string ROptReplacePairsOptDesc = nameof(ROptReplacePairsOptDesc);
        public const string ROptOutputOptDesc = nameof(ROptOutputOptDesc);
        public const string ROptOverrideOptDesc = nameof(ROptOverrideOptDesc);
        public const string ROptIncludeOptDesc = nameof(ROptIncludeOptDesc);
        public const string ROptIncludeExtOptDesc = nameof(ROptIncludeExtOptDesc);
        public const string ROptExcludeOptDesc = nameof(ROptExcludeOptDesc);
        public const string ROptExcludeExtOptDesc = nameof(ROptExcludeExtOptDesc);
        
        public const string ROptAllFilesOptDesc = nameof(ROptAllFilesOptDesc);

        public const string RenameFailedErrorResult = nameof(RenameFailedErrorResult);

        public const string RenameTargetNotFoundPrompt = nameof(RenameTargetNotFoundPrompt);
        public const string RenameOutputIsTargetPrompt = nameof(RenameOutputIsTargetPrompt);
        public const string RenameReplacePairsOptNotFoundPrompt = nameof(RenameReplacePairsOptNotFoundPrompt);


        // Generator
        public const string GenerateCommandDesc = nameof(GenerateCommandDesc);
        public const string GTemplateFileNotFoundPrompt = nameof(GTemplateFileNotFoundPrompt);
        public const string GMultiTplOutputToSingleFileError = nameof(GMultiTplOutputToSingleFileError);
        public const string GOutputFileExistsOverridePrompt = nameof(GOutputFileExistsOverridePrompt);

        public const string GOptConfigOptDesc = nameof(GOptConfigOptDesc);
        public const string GOptSelectOptDesc = nameof(GOptSelectOptDesc);
        public const string GOptTplPathOptDesc = nameof(GOptTplPathOptDesc);
        public const string GOptSyntaxOptDesc = nameof(GOptSyntaxOptDesc);
        public const string GOptVarStringOptDesc = nameof(GOptVarStringOptDesc);
        public const string GOptVarJsonFileOptDesc = nameof(GOptVarJsonFileOptDesc);
        public const string GOptVarYamlFileOptDesc = nameof(GOptVarYamlFileOptDesc);
        public const string GOptVarYamlFileContentContainsMultiObject = nameof(GOptVarYamlFileContentContainsMultiObject);
        public const string GOptOutputOptDesc = nameof(GOptOutputOptDesc);
        public const string GOptOverrideOptDesc = nameof(GOptOverrideOptDesc);
        public const string GOptWatchOptDesc = nameof(GOptWatchOptDesc);

        public const string GFailedErrorResult = nameof(GFailedErrorResult);

        public const string GSettingTemplatePathCliPrompt = nameof(GSettingTemplatePathCliPrompt);
        public const string GSettingTemplateVariableValueCliPrompt = nameof(GSettingTemplateVariableValueCliPrompt);
        public const string GSettingTemplateVariableValueForJsonFileCliPrompt = nameof(GSettingTemplateVariableValueForJsonFileCliPrompt);
        public const string GSettingTemplateVariableValueForYamlFileCliPrompt = nameof(GSettingTemplateVariableValueForYamlFileCliPrompt);
        public const string GSettingOutputCliPrompt = nameof(GSettingOutputCliPrompt);

        public const string GSettingFileNotFound = nameof(GSettingFileNotFound);
        public const string GSettingFileContentIsEmpty = nameof(GSettingFileContentIsEmpty);
        public const string GSettingOptionNotFound = nameof(GSettingOptionNotFound);
        public const string GSettingOptionChoosePrompt = nameof(GSettingOptionChoosePrompt);
        public const string GSettingOptionNotFoundError = nameof(GSettingOptionNotFoundError);
    }

    public static string L(string key, params string[] args)
    {
        if (args.Length > 0)
        {
            return string.Format(Instance.GetString(key), args);
        }

        return Instance.GetString(key);
    }
}
