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

    }
}
