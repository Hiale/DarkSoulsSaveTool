using System;
using System.Globalization;
using System.IO;

namespace Hiale.DarkSoulsSaveTool
{
    public static class Helper
    {
        /// <summary>
        /// Checks whether a path contains a separator "\" at the end. If not, it got added.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CheckDirectorySeparator(string path)
        {
            if (path == null)
                return null;
            return !path.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) ? path.Insert(path.Length, Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) : path;
        }

        public static bool HasWriteAccess(string directory)
        {
            try
            {
                Directory.GetAccessControl(directory);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
