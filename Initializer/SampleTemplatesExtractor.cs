using System.Reflection;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace vz_generator.Initializer
{
    public static class SampleTemplatesExtractor
    {
        // zip files to keep directory constructure
        // see build task "zip samples" in .vscode/tasks.json
        // ZipEntry should not contans prefix like "Initializer" "Samples"
        public const string SamplesResourceName = "vz_generator.Initializer.samples.zip";

        public static void Export(string extractToPath, string[] samples)
        {
            if (!samples.Any())
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(SamplesResourceName);
            if (stream == null)
            {
                return;
            }

            using var archive = new ZipArchive(stream);

            static string CreateDirectoryIfNotExists(params string[] paths)
            {
                var path = Path.Combine(paths);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }

            foreach (var name in samples)
            {
                foreach (var entry in archive.Entries)
                {
                    if (string.IsNullOrWhiteSpace(entry.Name))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(entry.FullName, $"^{Regex.Escape(name)}[/\\\\]"))
                    {
                        var destinationRoot = CreateDirectoryIfNotExists(extractToPath, name);
                        //sub folder
                        var subPath = CreateDirectoryIfNotExists(
                            extractToPath,
                            Path.Combine(
                                Regex.Replace(entry.FullName, $"{Regex.Escape(entry.Name)}$", string.Empty)
                                    .Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries)));
                        entry.ExtractToFile(Path.Combine(subPath, entry.Name), true);
                    }
                }
            }
        }
    }
}