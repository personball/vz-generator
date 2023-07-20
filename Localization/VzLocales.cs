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

        public const string VsCodeSettingsFileExistsPrompt = nameof(VsCodeSettingsFileExistsPrompt);

        // Generator

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
