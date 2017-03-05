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
        public static void FsGetDefRootName(
            IntPtr DefRootName,
            int maxlen)
        {
            var name = WfxManagedWrapper.Singleton.GetDefRootName();
            if(!String.IsNullOrWhiteSpace(name))
                MarshallingUtils.WriteStringToUnmanagedBuffer(name, DefRootName, maxlen, false, true);
        }
    }
}
