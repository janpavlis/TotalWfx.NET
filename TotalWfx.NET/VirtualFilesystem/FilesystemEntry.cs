using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.WinApi;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    /// <summary>
    /// Represents directory or file within WFX plugin's filesystem.
    /// </summary>
    public abstract class FilesystemEntry
    {
        public abstract string Name { get; protected set; }

        /// <summary>
        /// When overridden in derived class, creates WIN32_FIND_DATA_ANSI
        /// structure filled with informations from this instance.
        /// </summary>
        internal WIN32_FIND_DATA_ANSI ToFindDataAnsi()
        {
            var result = new WIN32_FIND_DATA_ANSI();
            result.cFileName = Name;
            FillFindDataAnsi(ref result);
            return result;
        }

        internal abstract void FillFindDataAnsi(ref WIN32_FIND_DATA_ANSI findData);
    }
}
