using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Tiveria.Common.Extensions
{
    public static partial class StringExtensions
    {
        public static string MakeRelativePath(this string basepath, string file)
        {
            System.Uri uri1 = new Uri(basepath);
            System.Uri uri2 = new Uri(file);

            Uri relativeUri = uri1.MakeRelativeUri(uri2);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        public static bool IsPathRelativeTo(this string basepath, string file)
        {
            if (!System.IO.Path.IsPathRooted(basepath))
                return false;

            return file.StartsWith(basepath);
        }

        private static string pathValidatorExpression = "^[^" + string.Join("", Array.ConvertAll(Path.GetInvalidPathChars(), x => Regex.Escape(x.ToString()))) + "]+$";
        private static Regex pathValidator = new Regex(pathValidatorExpression, RegexOptions.Compiled);

        private static string fileNameValidatorExpression = "^[^" + string.Join("", Array.ConvertAll(Path.GetInvalidFileNameChars(), x => Regex.Escape(x.ToString()))) + "]+$";
        private static Regex fileNameValidator = new Regex(fileNameValidatorExpression, RegexOptions.Compiled);

        private static string pathCleanerExpression = "[" + string.Join("", Array.ConvertAll(Path.GetInvalidPathChars(), x => Regex.Escape(x.ToString()))) + "]";
        private static Regex pathCleaner = new Regex(pathCleanerExpression, RegexOptions.Compiled);

        private static string fileNameCleanerExpression = "[" + string.Join("", Array.ConvertAll(Path.GetInvalidFileNameChars(), x => Regex.Escape(x.ToString()))) + "]";
        private static Regex fileNameCleaner = new Regex(fileNameCleanerExpression, RegexOptions.Compiled);

        public static bool ValidatePath(this string path)
        {
            return pathValidator.IsMatch(path);
        }

        public static bool ValidateFileName(this string fileName)
        {
            return fileNameValidator.IsMatch(fileName);
        }

        public static string CleanPath(this string path)
        {
            return pathCleaner.Replace(path, "");
        }

        public static string CleanFileName(this string fileName)
        {
            return fileNameCleaner.Replace(fileName, "");
        }

        public static string CleanPathAndFileName(this string both)
        {
            both = pathCleaner.Replace(both, "");
            return fileNameCleaner.Replace(both, "");
        }

    }
}
