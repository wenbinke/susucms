using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace SusuCMS
{
    public static class Starter
    {
        public static void Initialize()
        {
            InitializeModules();
        }

        private static void InitializeModules()
        {
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(i => i.FullName.StartsWith("SusuCMS"));
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes().Where(i => i.FullName.StartsWith("SusuCMS"));
                foreach (var type in types)
                {
                    if (type.GetInterface("IInitializer") != null)
                    {
                        var initializer = (IModuleInitializer)Activator.CreateInstance(type);
                        initializer.Initialize();
                    }
                }
            }
        }
    }
}
