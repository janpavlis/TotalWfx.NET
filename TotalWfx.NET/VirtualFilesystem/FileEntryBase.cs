using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.WinApi;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    /// <summary>
    /// Represents file within WFX plugin's filesystem.
    /// </summary>
    public abstract class FileEntryBase: FilesystemEntry
    {
        internal override void FillFindDataAnsi(ref WIN32_FIND_DATA_ANSI findData)
        {
            //ToDo: Fill file size, attributes, dates, etc.
        }
    }
}
