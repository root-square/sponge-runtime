using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Utilities
{
    public static class PathEnumerator
    {
        public static bool IsDirectory(string path)
        {
            FileAttributes attrributes = File.GetAttributes(path);

            return (attrributes & FileAttributes.Directory) == FileAttributes.Directory ? true : false;
        }

        public static bool IsExists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        public static IEnumerable<string> EnumerateFiles(string path, string filter = "*", bool recursive = false)
        {
            IEnumerator<string> enumerator;

            try
            {
                enumerator = Directory.EnumerateFiles(path, filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).GetEnumerator();
            }
            catch (UnauthorizedAccessException)
            {
                yield break;
            }

            while (true)
            {
                try { if (!enumerator.MoveNext()) break; }
                catch (UnauthorizedAccessException) { continue; }
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<string> EnumerateDirectories(string path, string filter = "*", bool recursive = false)
        {
            IEnumerator<string> enumerator;

            try
            {
                enumerator = Directory.EnumerateDirectories(path, filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).GetEnumerator();
            }
            catch (UnauthorizedAccessException)
            {
                yield break;
            }

            while (true)
            {
                try { if (!enumerator.MoveNext()) break; }
                catch (UnauthorizedAccessException) { continue; }
                yield return enumerator.Current;
            }
        }
    }
}
