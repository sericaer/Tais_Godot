using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tais.Mod
{
    class Modder
    {
        internal Dictionary<string, ModItem> modItems;

        internal Dictionary<string, IEnumerable<IEvent>> events;

        internal Dictionary<string, IEnumerable<Language>> languages;

        internal Dictionary<string, PersonName> personName;

        internal static Modder Load(string modPath)
        {
            var modder = new Modder();

            foreach (var subpath in Directory.EnumerateDirectories(modPath))
            {
                modder.modItems.Add(Path.GetFileName(subpath), new ModItem(subpath));
            }

            modder.Init();

            return modder;
        }

        internal Modder()
        {
            modItems = new Dictionary<string, ModItem>();
        }

        private void Init()
        {
            events = modItems.ToDictionary(x=>x.Key, y=>y.Value.events);
            languages = modItems.ToDictionary(x => x.Key, y => y.Value.languages);

            personName = modItems.SelectMany(x => x.Value.languages).ToDictionary(x => x.locale, y => y.personName);
        }
    }
}
