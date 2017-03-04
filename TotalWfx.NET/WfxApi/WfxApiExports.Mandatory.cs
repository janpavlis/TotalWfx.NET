using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.ManagedWrapper;
using RGiesecke.DllExport;

namespace Hopsoft.TotalCommander.TotalWfx.WfxApi
{
    /// <summary>
    /// Exports .NET methods implementing Total Commander's file system API
    /// as unmanaged functions so that Total Commander is able to call them.
    /// Also performs marshalling of parameters between managed/unmanaged types.
    /// </summary>
    internal static partial class WfxApiExports
    {
        [DllExport]
        public static int FsInit(
            int PluginNr,
            IntPtr pProgressProc,
            IntPtr pLogProc,
            IntPtr pRequestProc)
        {
            WfxManagedWrapper.Singleton.PluginNr = PluginNr;
            return 0;
        }

        [DllExport]
        public static IntPtr FsFindFirst(
            [MarshalAs(UnmanagedType.LPStr)] string Path,
            ref WinApi.WIN32_FIND_DATA_ANSI FindData)
        {
            return WfxManagedWrapper.Singleton.FindFirst(Path, ref FindData);
        }

        [DllExport]
        public static bool FsFindNext(
            IntPtr Hdl,
            ref WinApi.WIN32_FIND_DATA_ANSI FindData)
        {
            return WfxManagedWrapper.Singleton.FindNext(Hdl, ref FindData);
        }

        [DllExport]
        public static int FsFindClose(IntPtr Hdl)
        {
            return WfxManagedWrapper.Singleton.Close(Hdl);
        }
    }
}
