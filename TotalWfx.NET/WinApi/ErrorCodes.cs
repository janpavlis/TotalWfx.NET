using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.WinApi
{
    /// <summary>
    /// WinAPI error codes
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/cs-cz/library/windows/desktop/ms681382(v=vs.85).aspx"/>
    internal static class ErrorCodes
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        internal const uint ERROR_SUCCESS = 0;

        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        internal const uint ERROR_INVALID_FUNCTION = 1;

        /// <summary>
        /// The system cannot find the file specified.
        /// </summary>
        internal const uint ERROR_FILE_NOT_FOUND = 2;

        /// <summary>
        /// The system cannot find the path specified.
        /// </summary>
        internal const uint ERROR_PATH_NOT_FOUND = 3;

        /// <summary>
        /// There are no more files.
        /// </summary>
        internal const uint ERROR_NO_MORE_FILES = 18;
    }
}
