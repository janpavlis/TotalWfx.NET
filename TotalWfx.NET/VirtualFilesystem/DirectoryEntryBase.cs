using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.WinApi;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    /// <summary>
    /// Represents directory within WFX plugin's filesystem.
    /// </summary>
    public abstract class DirectoryEntryBase: FilesystemEntry
    {
        public abstract IEnumerable<FilesystemEntry> EnumerateChildFilesystemEntries();

        internal override void FillFindDataAnsi(ref WIN32_FIND_DATA_ANSI findData)
        {
            findData.dwFileAttributes |= 16;//Directory attribute
            //ToDo: attributes, dates, etc.
        }
    }
}
