using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem;

namespace Hopsoft.TotalCommander.TotalWfx
{
    /// <summary>
    /// Base class for file system plugin.
    /// Derived class must contain public parameterless contructor (will
    /// be called via reflection in PluginFactory when Total Commander
    /// loads plugin into memory).
    /// </summary>
    public abstract class WfxPluginBase
    {
        /// <summary>
        /// Plugin's unique identifier passed by by Total Commander
        /// in InitFs function.
        /// </summary>
        internal int PluginNr { get; set; }

        ///// <summary>
        ///// When overriden in derive class, gets name of plugin as will appear
        ///// in Total Commander under "Network Neighborhood".
        ///// </summary>
        //public virtual string PluginName => null;

        /// <summary>
        /// When overriden in derived class
        /// </summary>
        /// <returns></returns>
        public abstract DirectoryEntryBase GetRootDirectoryEntry();
    }
}
