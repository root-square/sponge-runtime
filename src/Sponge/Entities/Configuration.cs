using Sponge.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities
{
    public class Configuration
    {
        [JsonPropertyName("runtime")]
        public RuntimeConfiguration Runtime { get; set; } = new RuntimeConfiguration();

        [JsonPropertyName("link")]
        public LinkConfiguration Link { get; set; } = new LinkConfiguration();

        [JsonPropertyName("cache")]
        public CacheConfiguration Cache { get; set; } = new CacheConfiguration();

        [JsonPropertyName("task")]
        public TaskConfiguration Task { get; set; } = new TaskConfiguration();
    }
}
