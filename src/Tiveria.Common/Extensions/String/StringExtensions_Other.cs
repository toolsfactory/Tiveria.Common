using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiveria.Common.Extensions
{
    public static partial class StringExtensions
    {
        public static string FromNullTerminatedBytes(this byte[] data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            var result = new String(Encoding.ASCII.GetChars(data));
            return result.TrimEnd('\0');
        }

        public static byte[] ToNullTerminatedBytes(this string input)
        {
            _ = input ?? throw new ArgumentNullException(nameof(input));
            return Encoding.ASCII.GetBytes(input + "\x00");
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static string RemoveAll(this string input, char removalchar)
        {
            int len = input.Length,
                index = 0,
                i = 0;
            var src = input.ToCharArray();
            char ch;
            for (; i < len; i++)
            {
                ch = src[i];
                if (ch != removalchar)
                    src[index++] = ch;
            }
            return new string(src, 0, index);
        }

        public static byte[] ToByteArray(this String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string NormalizeWhiteSpaces(this string input)
        {
            char chr;
            int index = 0;
            bool skip = false;
            var src = input.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                chr = src[i];
                switch (chr)
                {
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u2028':
                    case '\u2029':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                        if (skip) continue;
                        src[index++] = chr;
                        skip = true;
                        continue;
                    default:
                        skip = false;
                        src[index++] = chr;
                        continue;
                }
            }
            return new string(src, 0, index);
        }

        public static string AddPrefixSpaces(this string input, int count)
        {
            if (count < 1)
                return input;
            return new String(' ', count) + input;
        }

        public static bool IsDigits(this string s)
        {
            foreach (char c in s) if (!Char.IsDigit(c)) return false;
            return true;
        }

        public static bool ListContains(this string s, char separator, string value, bool ignorecase = true)
        {
            if (ignorecase)
            {
                s.ToLower();
                value.ToLower();
            }
            var parts = s.Split(separator);
            foreach (string part in parts)
            {
                if (part.Trim() == value) return true;
            }
            return false;
        }


        /// <summary>
        /// Array of valid wildcards
        /// </summary>
        private static char[] Wildcards = new char[] { '*', '?' };

        /// <summary>
        /// Returns true if the string matches the pattern which may contain * and ? wildcards.
        /// Matching is done without regard to case.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Wildcard(this string s, string pattern)
        {
            return Wildcard(s, pattern, false);
        }

        /// <summary>
        /// Returns true if the string matches the pattern which may contain * and ? wildcards.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static bool Wildcard(this string s, string pattern, bool caseSensitive)
        {
            if (pattern == "*") return true;

            // if not concerned about case, convert both string and pattern
            // to lower case for comparison
            if (!caseSensitive)
            {
                pattern = pattern.ToLower();
                s = s.ToLower();
            }

            // if pattern doesn't actually contain any wildcards, use simple equality
            if (pattern.IndexOfAny(Wildcards) == -1) return (s == pattern);

            // otherwise do pattern matching
            int i = 0;
            int j = 0;
            while (i < s.Length && j < pattern.Length && pattern[j] != '*')
            {
                if ((pattern[j] != s[i]) && (pattern[j] != '?'))
                {
                    return false;
                }
                i++;
                j++;
            }

            // if we have reached the end of the pattern without finding a * wildcard,
            // the match must fail if the string is longer or shorter than the pattern
            if (j == pattern.Length) return s.Length == pattern.Length;

            int cp = 0;
            int mp = 0;
            while (i < s.Length)
            {
                if (j < pattern.Length && pattern[j] == '*')
                {
                    if ((j++) >= pattern.Length)
                    {
                        return true;
                    }
                    mp = j;
                    cp = i + 1;
                }
                else if (j < pattern.Length && (pattern[j] == s[i] || pattern[j] == '?'))
                {
                    j++;
                    i++;
                }
                else
                {
                    j = mp;
                    i = cp++;
                }
            }

            while (j < pattern.Length && pattern[j] == '*')
            {
                j++;
            }

            return j >= pattern.Length;
        }

        public static string[] ToNotEmptyLines(this string value)
        {
            return value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] ToLines(this string value)
        {
            return value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string[] TrimStringArray(this IEnumerable<string> array)
        {
            return array.Select(item => item.Trim()).ToArray();
        }
    }
}
