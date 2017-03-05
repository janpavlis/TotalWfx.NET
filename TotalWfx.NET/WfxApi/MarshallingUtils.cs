using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.WfxApi
{
    internal static class MarshallingUtils
    {
        /// <summary>
        /// Writes string into unmanaged byte buffer as null-terminated string.
        /// </summary>
        /// <param name="str">String to be written into unmanaged buffer.</param>
        /// <param name="unmanagedBuffer">Address of target unmanaged buffer.</param>
        /// <param name="unmanagedBufferLength">Length of target unmanaged buffer (including space for terminal ).</param>
        /// <param name="unicode">If true, string will be converted usinf Unicode encoding. Otherwise, ANSI encoding will be used.</param>
        /// <param name="allowStringTruncation">If true, string is truncated to fit unto unmanaged buffer, rather than throwing exception.</param>
        internal static void WriteStringToUnmanagedBuffer(string str, IntPtr unmanagedBuffer, int unmanagedBufferLength, bool unicode, bool allowStringTruncation)
        {
            if (str == null)
                throw new ArgumentNullException();

            var encoding = unicode ? Encoding.Unicode : Encoding.Default;
            var terminatingCharacterBytes = Encoding.Default.GetByteCount("\x0");
            var byteCount = encoding.GetByteCount(str);

            if(byteCount + terminatingCharacterBytes <= unmanagedBufferLength)
            {
                // Whole string fits into unmanaged buffer
                var bytes = encoding.GetBytes(str + '\x0');
                Debug.Assert(bytes.Length <= unmanagedBufferLength);
                Marshal.Copy(bytes, 0, unmanagedBuffer, bytes.Length);
            }
            else
            {
                // String is too long and must be shortened.
                if (!allowStringTruncation)
                    throw new Exception($"String is too long ({str.Length} characters) to fit into buffer of length {unmanagedBufferLength}!");

                var bytes = new byte[unmanagedBufferLength];
                var chars = str.ToCharArray();

                // Reserve space at the end of byte array - one character for
                // flushing encoder, the second for null-terminating character.
                var reservedCharactersBytes = encoding.GetMaxByteCount(2);

                var encoder = encoding.GetEncoder();
                int charsUsed;
                int bytesUsed;
                int bytesUsed2;
                bool completed;

                // Write as much characters as we can
                encoder.Convert(chars, 0, chars.Length, bytes, 0, unmanagedBufferLength - reservedCharactersBytes, false, out charsUsed, out bytesUsed, out completed);

                // Write null-terminating character and flush encoder
                chars = new char[] { '\x0' };
                encoder.Convert(chars, 0, 1, bytes, bytesUsed, reservedCharactersBytes, true, out charsUsed, out bytesUsed2, out completed);

                Debug.Assert(completed);
                Debug.Assert(bytesUsed + bytesUsed2 <= unmanagedBufferLength);

                Marshal.Copy(bytes, 0, unmanagedBuffer, bytesUsed + bytesUsed2);
            }
        }
    }
}
