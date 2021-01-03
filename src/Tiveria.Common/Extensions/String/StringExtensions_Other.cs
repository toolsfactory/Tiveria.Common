using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
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

    }
}
