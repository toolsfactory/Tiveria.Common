using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace System
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesImplementing<T>(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(t => t.IsPublic &&
                                                          !t.IsAbstract &&
                                                          typeof(T).IsAssignableFrom(t));
        }

        public static string GetTextResource(this Assembly assembly, string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                // load a specific stream within the assembly
                Stream stream = assembly.GetManifestResourceStream(name);
                // make a text reader out of it
                StreamReader textReader = new StreamReader(stream);
                // and read the text ...
                return textReader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        public static string FindResource(this Assembly assembly, string name, bool pattern)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            string[] resources = assembly.GetManifestResourceNames();
            foreach (string res in resources)
            {
                if ((res == name) || ((res.EndsWith(name) && pattern)))
                    return res;
            }
            return null;
        }
    }

}

