using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    /// <summary>
    /// Represents empty directory within WFX plugin's filesystem.
    /// </summary>
    public class EmptyDirectoryEntry: DirectoryEntryBase
    {
        public EmptyDirectoryEntry(string name)
        {
            this.Name = name;
        }

        public override string Name { get; protected set; }

        public override IEnumerable<FilesystemEntry> EnumerateChildFilesystemEntries() => Enumerable.Empty<FilesystemEntry>();
    }
}
