using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hopsoft.TotalCommander.TotalWfx.VirtualFilesystem
{
    public static class VirtualFilesystemUtils
    {
        internal const char PATH_SEPARATOR = '\\';

        /// <summary>
        /// Checks if name is valid name for filesystem entry. Throws exception if it isn't.
        /// </summary>
        public static void CheckFilesystemEntryName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Filesystem entry name cannot be empty!", nameof(name));

            //ToDo: check on special characters, length-check, etc.
        }

        /// <summary>
        /// Splits filesystem path to it's individual directory (or file) names.
        /// </summary>
        internal static IEnumerable<string> SplitPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var result = path.Split(PATH_SEPARATOR).ToList();

            // Remove empty entry at the beginning representing root directory
            if (String.IsNullOrEmpty(result[0]))
                result.RemoveAt(0);
            else
                throw new Exception($"Invalid path '{path}'");

            // Remove empty entry behind trailing '\', if present
            if (String.IsNullOrEmpty(result[result.Count - 1]))
                result.RemoveAt(result.Count - 1);

            return result;
        }

        /// <summary>
        /// Returns true if both filesystem entry names are equal.
        /// </summary>
        internal static bool FilesystemEntryNamesEquals(string entryName1, string entryName2)
        {
            return String.Compare(entryName1, entryName2, true) == 0;
        }

        /// <summary>
        /// Finds filesystem entry representing given path. If no such entry exists, returns null.
        /// </summary>
        internal static FilesystemEntry TryFindFilesystemEntry(string path, DirectoryEntryBase rootDirectory)
        {
            var pathNames = SplitPath(path);
            var currentDirectory = rootDirectory;
            FileEntryBase file = null;

            foreach (var pathName in pathNames)
            {
                if (currentDirectory == null)
                    return null;

                if (file != null)
                {
                    //File cannot contain children
                    return null;
                }

                var childEntry = TryFindFilesystemEntryInDirectory(currentDirectory, pathName);

                if (childEntry is DirectoryEntryBase childDirectory)
                {
                    currentDirectory = childDirectory;
                }
                else if (childEntry is FileEntryBase f)
                {
                    file = f;
                }
                else
                    return null;
            }

            if (file != null)
                return file;
            else
                return currentDirectory;
        }

        internal static FilesystemEntry TryFindFilesystemEntryInDirectory(DirectoryEntryBase parentDirectory, string childName)
        {
            if (parentDirectory == null)
                throw new ArgumentNullException(nameof(parentDirectory));

            if (String.IsNullOrEmpty(childName))
                throw new ArgumentException();

            return parentDirectory
                .EnumerateChildFilesystemEntries()
                .FirstOrDefault(entry => FilesystemEntryNamesEquals(entry.Name, childName));
        }
    }
}
