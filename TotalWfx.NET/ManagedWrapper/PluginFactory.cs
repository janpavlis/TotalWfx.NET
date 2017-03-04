using System;
using System.Collections.Generic;
using System.Linq;
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
                .First();

            return (WfxPluginBase)Activator.CreateInstance(pluginType);
        }
    }
}
