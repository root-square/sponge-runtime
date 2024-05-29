using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services.Abstractions
{
    public class Route
    {
        public string Name { get; set; } = string.Empty;

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public Route(string name)
        {
            Name = name;
        }

        public Route(string name, string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public string[] ToArray()
        {
            string[] result = new string[Aliases.Length + 1];
            result[0] = Name;
            Array.Copy(Aliases, 0, result, 1, Aliases.Length);
            return result;
        }

        public List<string> ToList()
        {
            var result = new List<string>() { Name };
            result.AddRange(Aliases);
            return result;
        }
    }
}
