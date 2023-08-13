using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Worker.Utilities
{
    internal static class ConsoleHelper
    {
        internal static T? Parse<T>(string[] args, string name, char shortName, T defaultValue)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], $"--{name}", StringComparison.OrdinalIgnoreCase) || string.Equals(args[i], $"-{shortName}", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 == args.Length || args[i + 1].StartsWith('-'))
                    {
                        if (typeof(T) == typeof(bool))
                        {
                            return (T)Convert.ChangeType(true, typeof(T));
                        }

                        return defaultValue;
                    }

                    return (T)Convert.ChangeType(args[i + 1], typeof(T));
                }
            }

            return defaultValue;
        }
    }
}
