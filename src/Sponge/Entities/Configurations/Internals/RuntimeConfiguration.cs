using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class RuntimeConfiguration
    {
        [JsonPropertyName("port")]
        public int Port { get; set; } = 40126;

        [JsonPropertyName("use-dynamic-port")]
        public bool UseDynamicPort { get; set; } = true;

        [JsonPropertyName("use-timeout")]
        public bool UseTimeout { get; set; } = true;

        [JsonPropertyName("timeout")]
        public int Timeout { get; set; } = 360;
    }
}
