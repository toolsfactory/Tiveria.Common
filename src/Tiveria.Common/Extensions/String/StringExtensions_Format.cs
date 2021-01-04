using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tiveria.Common.Extensions
{
     public static partial class StringExtensions
     {
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string ToMD5Hash(this string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool CompareMd5Hash(this string input, string hash)
        {
            // Hash the input.
            string hashOfInput = input.ToMD5Hash();

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            // Now compare the hashes
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }


        public static string ReplaceInvalidPathChars(this string original, char replacement)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                if (original.IndexOf(c) != -1)
                    original = original.Replace(c, '_');

            return original;
        }

        public static bool Contains(this string input, string what, StringComparison stringComparison = StringComparison.InvariantCulture)
            => input.IndexOf(what, stringComparison) >= 0;

        /// <summary>
        /// Matches all the expressions inside '{ }' defined in <paramref name="pattern"/> for the <paramref name="query"/> and populates the <paramref name="args"/>.
        /// <para>Example: query: "Hello world", pattern: "{first} world" => args["first"] is "Hello".</para>
        /// </summary>
        /// <param name="query">Query string.</param>
        /// <param name="pattern">Pattern string defining the expressions to match inside '{ }'.</param>
        /// <param name="args">Key-value pair collection populated by <paramref name="pattern"/> keys and matches in <paramref name="query"/> if found.</param>
        /// <returns>True is all defined keys in <paramref name="pattern"/> are matched, false otherwise.</returns>
        public static bool TryMatch(this string query, string pattern, Dictionary<string, string> args)
        {
            var names = new List<string>();
            var regex = Regex.Replace(pattern, @"\{\w+\}", m =>
            {
                names.Add(m.Value.Substring(1, m.Value.Length - 1 - 1));
                return @"(.+?)";
            });

            //if regex is not employed, strings must match
            if (names.Count == 0)
                return String.Compare(query, regex, true) == 0;

            //make the last pattern greedy
            regex = replaceLastOccurrence(regex, @"(.+?)", @"(.+)");

            var match = Regex.Match(query, regex, RegexOptions.IgnoreCase);
            if (!match.Success) return false;

            for (int i = 0; i < Math.Min(names.Count, match.Groups.Count - 1); i++)
            {
                args.Add(names[i], match.Groups[i + 1].Value);
            }

            return true;
        }
         private static string replaceLastOccurrence(string source, string oldStr, string newStr)
        {
            int place = source.LastIndexOf(oldStr);

            if (place == -1)
                return source;

            string result = source.Remove(place, oldStr.Length).Insert(place, newStr);
            return result;
        }
    }
}
