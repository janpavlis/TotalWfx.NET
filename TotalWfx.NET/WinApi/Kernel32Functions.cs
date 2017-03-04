using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.WinApi
{
    internal static class Kernel32Functions
    {
        [DllImport("kernel32.dll")]
        public static extern void SetLastError(uint dwErrorCode);
    }
}
