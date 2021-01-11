using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tais.API;

namespace Tais.Mod
{
    class ModItem
    {
        public readonly string name;

        public IEnumerable<EventDef> events = new List<EventDef>();
        public IEnumerable<Language> languages = new List<Language>();
        public IEnumerable<InitSelect> initSelects = new List<InitSelect>();
        public IEnumerable<DepartDef> departs = new List<DepartDef>();
        public IEnumerable<AdjustDef> adjusts = new List<AdjustDef>();

        public ChaotingDef chaoting;

        private string path;
        

        private string assemblyPath => $"{path}/Assemblies/{name}.dll";

        

        public ModItem(string path)
        {
            LOG.INFO($"Load mod, path: {path}");

            this.path = path;
            this.name = Path.GetFileName(path);

            languages = Language.Load(path + "/Languages");

            if (File.Exists(assemblyPath))
            {
                var assembly = Assembly.LoadFile(assemblyPath);

                events = LoadAssemblyObjects<EventDef>(assembly);
                initSelects = LoadAssemblyObjects<InitSelect>(assembly);
                departs = LoadAssemblyObjects<DepartDef>(assembly);
                adjusts = LoadAssemblyObjects<AdjustDef>(assembly);
                chaoting = LoadAssemblyObject<ChaotingDef>(assembly);
            }

            LOG.INFO($"Load mod {name} finished. events:{events.Count()}, initSelects:{initSelects.Count()}, departs:{departs.Count()}, adjusts:{adjusts.Count()}");

        }

        private T LoadAssemblyObject<T>(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(T)));
            if(types.Count() != 1)
            {
                throw new Exception();
            }

            return (T)Activator.CreateInstance(types.First());
        }

        private IEnumerable<Type> LoadAssemblyTypes<T>(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(T)));
        }

        private IEnumerable<T> LoadAssemblyObjects<T>(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(T)))
                    .Select(x => (T)Activator.CreateInstance(x));
        }
    }
}