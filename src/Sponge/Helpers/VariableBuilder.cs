using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Helpers
{
    /// <summary>
    /// Provides pre-formatted variables.
    /// </summary>
    internal class VariableBuilder
    {
        /// <summary>
        /// Returns the product version.
        /// </summary>
        /// <returns>The product version</returns>
        internal static string GetProductVersion()
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return attribute != null ? attribute!.InformationalVersion : "dev";
        }

        /// <summary>
        /// Returns the file version.
        /// </summary>
        /// <returns>The file version</returns>
        internal static string GetFileVersion()
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyVersionAttribute>();
            return attribute != null ? attribute!.Version : "dev";
        }

        /// <summary>
        /// Returns current application location.
        /// </summary>
        /// <returns>The current application location</returns>
        internal static string GetApplicationLocation()
        {
            return Path.Combine(GetBaseDirectory(), AppDomain.CurrentDomain.FriendlyName + ".exe");
        }

        /// <summary>
        /// Returns the base directory.
        /// </summary>
        /// <returns>The base directory</returns>
        internal static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
