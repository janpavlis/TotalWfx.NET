using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    /// <summary>
    /// Represents directory within WFX plugin's filesystem,
    /// with name and attributes and child filesystem entries
    /// predefined in contructor.
    /// </summary>
    public class SimpleDirectoryEntry: DirectoryEntryBase
    {
        private FilesystemEntry[] m_ChildFilesystemEntries;

        public SimpleDirectoryEntry(string name, IEnumerable<FilesystemEntry> childFilesystemEntries)
        {
            VirtualFilesystemUtils.CheckFilesystemEntryName(name);
            m_ChildFilesystemEntries = childFilesystemEntries?.ToArray() ?? new FilesystemEntry[0];
        }

        public override string Name { get; protected set; }

        public override IEnumerable<FilesystemEntry> EnumerateChildFilesystemEntries() => m_ChildFilesystemEntries;
    }
}
