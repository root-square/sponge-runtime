using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Helpers
{
    /// <summary>
    /// Provides functions related to URI(Uniform Resource Identifier).
    /// </summary>
    internal static class UriHelper
    {
        /// <summary>
        /// Opens an URI address.
        /// </summary>
        /// <param name="uri">An URI to open</param>
        internal static void Open(string uri)
        {
            try
            {
                Process.Start(uri);
            }
            catch
            {
                // Hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    uri = uri.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", uri);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", uri);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
