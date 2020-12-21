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

        public IEnumerable<IEvent> events = new List<IEvent>();
        public IEnumerable<Language> languages = new List<Language>();
        public IEnumerable<InitSelect> initSelects = new List<InitSelect>();

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

                events = LoadAssemblyTypes<IEvent>(assembly);
                initSelects = LoadAssemblyTypes<InitSelect>(assembly);
            }

            LOG.INFO($"Load mod {name} finished. events:{events.Count()}, initSelects:{initSelects.Count()}");

        }

        private IEnumerable<T> LoadAssemblyTypes<T>(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(T)))
                    .Select(x => (T)Activator.CreateInstance(x));
        }
    }
}