using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem;
using Hopsoft.TotalCommander.TotalWfx.WinApi;

namespace Hopsoft.TotalCommander.TotalWfx.ManagedWrapper
{
    internal class WfxManagedWrapper
    {
        #region Static members

        internal static WfxManagedWrapper Singleton = new WfxManagedWrapper();

        #endregion

        #region Instance members

        private WfxPluginBase m_Plugin;
        private EnumerationsManager m_EnumerationsManager = new EnumerationsManager();

        private WfxManagedWrapper()
        {
            m_Plugin = PluginFactory.Create();
        }

        /// <summary>
        /// Plugin's unique identifier passed by Total Commander
        /// in InitFs function.
        /// </summary>
        internal int PluginNr { get; set; }

        internal IntPtr FindFirst(string path, ref WIN32_FIND_DATA_ANSI findData)
        {
            try
            {
                // Find directory in virtual filesystem representing %path.
                var entryForPath = VirtualFilesystemUtils.TryFindFilesystemEntry(path, m_Plugin.GetRootDirectoryEntry());

                if (entryForPath is DirectoryEntryBase directoryEntryForPath)
                {
                    var enumeration = directoryEntryForPath.EnumerateChildFilesystemEntries();
                    if(enumeration != null)
                    {
                        var enumerator = enumeration.GetEnumerator();
                        if (enumerator != null && enumerator.MoveNext())
                        {
                            findData = enumerator.Current.ToFindDataAnsi();
                            return m_EnumerationsManager.Add(enumerator);
                        }
                    }

                    // Directory is empty
                    Kernel32Functions.SetLastError(ErrorCodes.ERROR_NO_MORE_FILES);
                    return Handles.INVALID_HANDLE_VALUE;
                }

                // Path does not exists or is not directory
                Kernel32Functions.SetLastError(ErrorCodes.ERROR_PATH_NOT_FOUND);
                return Handles.INVALID_HANDLE_VALUE;
            }
            catch
            {
                Kernel32Functions.SetLastError(ErrorCodes.ERROR_INVALID_FUNCTION);
                return Handles.INVALID_HANDLE_VALUE;
            }

        }

        internal bool FindNext(IntPtr enumerationId, ref WIN32_FIND_DATA_ANSI findData)
        {
            try
            {
                var enumerator = m_EnumerationsManager.Get(enumerationId);
                if (enumerator.MoveNext())
                {
                    findData = enumerator.Current.ToFindDataAnsi();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        internal int Close(IntPtr enumerationId)
        {
            try
            {
                m_EnumerationsManager.Remove(enumerationId);
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        internal string GetDefRootName()
        {
            return m_Plugin.GetRootDirectoryEntry().Name;
        }

        #endregion
    }
}
