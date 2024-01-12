using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Helpers
{
    /// <summary>
    /// Provides functions related to localization.
    /// </summary>
    internal static class LocalizationHelper
    {
        /// <summary>
        /// Gets a text from the specified key. If the key doesn't exist, returns <value>"NULL"</value>.
        /// </summary>
        /// <param name="key">A key of localization text</param>
        /// <returns>A localization text</returns>
        internal static string GetText(string key)
        {
            return (string?)App.Current.Resources[key] ?? "NULL";
        }
    }
}
