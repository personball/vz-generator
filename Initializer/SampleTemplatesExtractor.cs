using System.Reflection;
using System.IO.Compression;

namespace vz_generator.Initializer
{
    public static class SampleTemplatesExtractor
    {
        // zip files to keep directory constructure
        // see build task "zip samples" in .vscode/tasks.json
        public const string SamplesResourceName = "vz_generator.Initializer.samples.zip";
        public const string ZipEntryPrefix = "Initializer/Samples/";
        public static async Task ExportAsync(string extractToPath, string[] samples)
        {
            if (!samples.Any())
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(SamplesResourceName);
            using var archive = new ZipArchive(stream);

            string CreateDirectoryIfNotExists(params string[] paths)
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

                    if (entry.FullName.StartsWith(ZipEntryPrefix + name))
                    {
                        var destinationRoot = CreateDirectoryIfNotExists(extractToPath, name);
                        //sub folder
                        var subPath = CreateDirectoryIfNotExists(
                            extractToPath,
                            Path.Combine(
                                entry.FullName.Replace(ZipEntryPrefix, string.Empty)
                                              .Replace(entry.Name, string.Empty)
                                              .Split('/', StringSplitOptions.RemoveEmptyEntries)));
                        Console.WriteLine(subPath);
                        entry.ExtractToFile(Path.Combine(subPath, entry.Name), true);
                    }
                }
            }
        }
    }
}