using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.WinApi
{
    // The CharSet must match the CharSet of the corresponding PInvoke signature
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct WIN32_FIND_DATA_ANSI
    {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;

        /// <summary>
        /// Creates WIN32_FIND_DATA_ANSI structure and fills it with informations
        /// from FileSystemInfo instance.
        /// </summary>
        internal static WinApi.WIN32_FIND_DATA_ANSI FromFileInfo(FileSystemInfo fileSystemInfo)
        {
            var result = new WinApi.WIN32_FIND_DATA_ANSI();
            result.cFileName = fileSystemInfo.Name;
            var ct = new DateTime(2017, 2, 10, 11, 11, 11, DateTimeKind.Local).ToFileTime();
            result.ftLastWriteTime = GetFileTime(fileSystemInfo.LastWriteTime);
            if (fileSystemInfo is FileInfo fileInfo)
            {
                // File
                result.nFileSizeHigh = (uint)(fileInfo.Length >> 32);
                result.nFileSizeLow = (uint)(fileInfo.Length & 0xFFFFFFFFL);
                result.dwFileAttributes = (uint)fileInfo.Attributes;
            }
            else
            {
                // Directory
                result.dwFileAttributes |= 16;
            }
            return result;
        }

        private static System.Runtime.InteropServices.ComTypes.FILETIME GetFileTime(DateTime datetime)
        {
            var result = new System.Runtime.InteropServices.ComTypes.FILETIME();
            var filetime = datetime.ToFileTime();
            result.dwHighDateTime = (int)(filetime >> 32);
            result.dwLowDateTime = (int)(filetime & 0xFFFFFFFFL);
            return result;
        }
    }
}
