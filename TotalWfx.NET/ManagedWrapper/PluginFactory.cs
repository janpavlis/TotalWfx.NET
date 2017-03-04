using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.ManagedWrapper
{
    /// <summary>
    /// Creates instance of class imlementing file system plugin.
    /// </summary>
    internal static class PluginFactory
    {
        /// <summary>
        /// Creates instance of class that derives from WfxPluginBase.
        /// </summary>
        /// <returns>Instance of managed file system plugin.</returns>
        public static WfxPluginBase Create()
        {
            // Ensure, that this assembly is properly resolved
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            // Load assemblies, that might contain implementation of filesystem
            // plugin, into current AppDomain.
            LoadAssemblies();

            // Searches all types in current AppDomain and finds first that
            // derives from WfxPluginBase and can be instantiated.
            var pluginType = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition)
                    .Where(type => type.IsSubclassOf(typeof(WfxPluginBase)))
                    .Where(type => type.GetConstructors().Any(
                        constructor => constructor.IsPublic && !constructor.GetParameters().Any()))
                    .FirstOrDefault();

            if (pluginType == null)
                throw new Exception($"No type implementing abstract class {nameof(WfxPluginBase)} was found!");

            return (WfxPluginBase)Activator.CreateInstance(pluginType);
        }

        /// <summary>
        /// This handler of AssemblyResolve event ensures, that assemblies referencing
        /// this assembly are able to resolve this assembly even though it has .wfx
        /// or .wfx64 extension (instead of .dll).
        /// </summary>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var requestedAssemblyName = new AssemblyName(args.Name);
            
            if (AssemblyName.ReferenceMatchesDefinition(requestedAssemblyName, executingAssembly.GetName()))
            {
                return executingAssembly;
            }
            return null;
        }

        /// <summary>
        /// Loads all assemblies from directorz where this assembly is located.
        /// </summary>
        private static void LoadAssemblies()
        {
            var executingAssemblyDirectoryStr = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var executingAssemblyDirectory = new System.IO.DirectoryInfo(executingAssemblyDirectoryStr);
            var assemblies = executingAssemblyDirectory.GetFiles("*.dll").ToArray();

            foreach (var a in assemblies)
            {
                Assembly.LoadFrom(a.FullName);
            }
        }
    }
}
