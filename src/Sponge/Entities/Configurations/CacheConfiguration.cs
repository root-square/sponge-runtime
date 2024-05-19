using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations
{
    public class CacheConfiguration
    {
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("strategy")]
        public string? Strategy { get; set; } = "lfu";

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; } = 1000;

        [JsonPropertyName("duration")]
        public int Duration { get; set; } = 10;
    }
}
