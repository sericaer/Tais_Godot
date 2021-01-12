using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaisGodot.Scripts
{
    static class TranslateServerEx
    {
        internal static void AddTranslate(string modName, IEnumerable<Tais.Mod.Language> languages)
        {
            foreach(var language in languages)
            {
                Translation tran = GetTranslate(language.locale);

                foreach (var pair in language.dict)
                {
                    tran.AddMessage(modName == "Native" ? pair.Key : $"{modName}_{pair.Key}", pair.Value);
                }
            }

        }

        internal static string Translate(string format, params string[] pp)
        {
            var tranFormat = TranslationServer.Translate(format);
            if(pp.Count() != 0 && format == tranFormat)
            {
                return $"{tranFormat}({String.Join(",", pp)})";
            }

            return String.Format(tranFormat, pp);
        }

        internal static void SetLocale(string locale)
        {
            TranslationServer.SetLocale(locale);
        }

        internal static string GetLocale()
        {
            return TranslationServer.GetLocale();
        }

        private static Translation GetTranslate(string locale)
        {
            Translation tran;
            if (!tranDict.TryGetValue(locale, out tran))
            {
                tran = new Translation();
                tran.Locale = locale;

                tranDict.Add(locale, tran);
                TranslationServer.AddTranslation(tran);
            }

            return tran;
        }

        private static Dictionary<string, Translation> tranDict = new Dictionary<string, Translation>();
    }
}
