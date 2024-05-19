using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations
{
    public class RuntimeConfiguration
    {
        [JsonPropertyName("mode")]
        public int Mode { get; set; } = 0;

        [JsonPropertyName("port")]
        public ushort Port { get; set; } = 40126;
    }
}
