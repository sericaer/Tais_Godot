using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tais.Mod
{
    internal class ModItem
    {
        public readonly string name;
        public IEnumerable<IEvent> events = new List<IEvent>();
        public IEnumerable<Language> languages = new List<Language>();

        private string path;
        
        private string assemblyPath => $"{path}/Assemblies/{name}.dll";

        public ModItem(string path)
        {
            LOG.INFO($"Load mod, path: {path}");

            this.path = path;
            this.name = Path.GetFileName(path);

            LoadAssemblies();

            languages = Language.Load(path + "/Languages");

        }

        private void LoadLanagues()
        {
            
        }

        private void LoadAssemblies()
        {
            if (File.Exists(assemblyPath))
            {
                var assembly = Assembly.LoadFile(assemblyPath);

                events = assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IEvent)))
                    .Select(x => Activator.CreateInstance(x) as IEvent);
            }
        }
    }
}