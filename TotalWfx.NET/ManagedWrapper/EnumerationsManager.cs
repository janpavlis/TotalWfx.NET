using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem;

namespace Hopsoft.TotalCommander.TotalWfx.ManagedWrapper
{
    /// <summary>
    /// Keeps track of filesystem entries enumerations that are currently in progress.
    /// </summary>
    internal class EnumerationsManager
    {
        private Dictionary<IntPtr, IEnumerator<FilesystemEntry>> m_CurrentEnumerations = new Dictionary<IntPtr, IEnumerator<FilesystemEntry>>();
        private IntPtr m_EnumerationIdGenerator = IntPtr.Zero;
        private Object m_Lock = new Object();

        /// <summary>
        /// Adds new enumeration.
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns>Unique identifier of added enumeration.</returns>
        internal IntPtr Add(IEnumerator<FilesystemEntry> enumerator)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            lock (m_Lock)
            {
                //ToDo: Handle m_EnumerationIdGenerator overflow
                m_EnumerationIdGenerator += 1;

                var enumerationId = m_EnumerationIdGenerator;
                m_CurrentEnumerations.Add(enumerationId, enumerator);
                return enumerationId;
            }
        }

        /// <summary>
        /// Returns enumeration with specified enumerationId. If specified
        /// enumerationId does not exists, throws exception.
        /// </summary>
        internal IEnumerator<FilesystemEntry> Get(IntPtr enumerationId)
        {
            lock (m_Lock)
            {
                return m_CurrentEnumerations[enumerationId];
            }
        }

        /// <summary>
        /// Removes enumeration with specified enumerationId. If specified
        /// enumerationId does not exists, throws exception.
        /// </summary>
        internal void Remove(IntPtr enumerationId)
        {
            lock (m_Lock)
            {
                m_CurrentEnumerations.Remove(enumerationId);
            }
        }
    }
}
