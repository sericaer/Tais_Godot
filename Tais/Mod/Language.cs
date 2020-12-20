using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tais.Mod
{
    class Language
    {
        public string locale;

        public Dictionary<string, string> dict;

        public PersonName personName;

        public Language(string path)
        {
            locale = Path.GetFileNameWithoutExtension(path);

            personName = PersonName.Load($"{path}/name/");

            dict = new Dictionary<string, string>();
            foreach (var sub in Directory.EnumerateFiles(path))
            {
                dict = LoadLanguageElement(path);
            }
        }

        internal static List<Language> Load(string path)
        {
            List<Language> rslt = new List<Language>();

            if (Directory.Exists(path))
            {
                foreach (var sub in Directory.EnumerateDirectories(path))
                {
                    rslt.Add(new Language(sub));
                }
            }

            return rslt;
        }

        private Dictionary<string, string> LoadLanguageElement(string path)
        {
            Dictionary<string, string> rslt = new Dictionary<string, string>();

            foreach (var file in System.IO.Directory.EnumerateFiles(path, "*.txt"))
            {
                var header = System.IO.Path.GetFileNameWithoutExtension(file).ToUpper();

                var lines = System.IO.File.ReadAllLines(file);
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Length == 0)
                    {
                        continue;
                    }

                    var splits = lines[i].Split(':');
                    if (splits.Count() != 2)
                    {
                        throw new Exception($"parse file error! must be XXX:XXX mode in {file}:{i}");
                    }

                    rslt.Add(header + "_" + splits[0], splits[1]);
                }
            }

            return rslt;
        }
    }
}

