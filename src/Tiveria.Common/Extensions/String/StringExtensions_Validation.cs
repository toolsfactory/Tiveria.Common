using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
     public static partial class StringExtensions
     {

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return String.IsNullOrWhiteSpace(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return String.IsNullOrEmpty(input);
        }

        public static bool IsValidEmail(this string input)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(input,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public static bool IsValidUrl(this string url)
        {
            Uri uri;
            var ok = Uri.TryCreate(url, UriKind.Absolute, out uri);
            if (!ok)
                return false;

            var result = Uri.CheckHostName(uri.Host);
            return (result != UriHostNameType.Unknown);
        }

        public static bool IsValidIP(this string url)
        {
            System.Net.IPAddress address;
            return System.Net.IPAddress.TryParse(url, out address);
        }

        public static bool IsValidHostName(this string url)
        {
            var regex = new Regex(@"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$");
            return regex.IsMatch(url);
        }
    }
}
