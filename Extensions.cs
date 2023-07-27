namespace vz_generator
{
    public static class Extensions
    {
        public static string EnsureEndsWith(this string origin, char c)
        {
            if (origin.EndsWith(c))
            {
                return origin;
            }

            return origin + c;
        }

        public static string EnsureEndsWithDirectorySeparatorChar(this string path)
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
}