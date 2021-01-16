using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTreeView.Data;
using System.IO;
using WpfTreeViews.Data;

namespace WpfTreeView
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // get every logical drive on the machine
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get folders

            //Try and get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }
            #endregion

            #region get files

            //Create a blank list for files
            var files = new List<string>();

            //Try and get files from the folder
            //ignoring any issues doing so
            try
            {
                var f = System.IO.Directory.GetFiles(fullPath);

                if (f.Length > 0)
                    items.AddRange(f.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File}));
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers
        /// <summary>
        /// Find the file or folder name form a full path
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // if we have no path, return empty
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            //make all slashes back slashase
            var normalizedPath = path.Replace('/', '\\');

            // find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //if we don't find a backshash, return the path itself
            if (lastIndex <= 0)
                return path;

            //return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }

}
