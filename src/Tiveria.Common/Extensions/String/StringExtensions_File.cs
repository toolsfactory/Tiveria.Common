using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
     public static partial class StringExtensions
     {

        public static bool FromFile(this string input, string fileName)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileName, Encoding.ASCII))
                {
                    input = sr.ReadToEnd();
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        public static bool ToFile(this string input, string fileName)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(fileName, false, Encoding.ASCII))
                {
                    sr.WriteLine(input);
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
