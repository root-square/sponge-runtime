using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class LoggingConfiguration
    {
        [JsonPropertyName("use-verbose-mode")]
        public bool UseVerboseMode { get; set; } = false;
    }
}
