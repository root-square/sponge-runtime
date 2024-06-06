using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class AudioConfiguration
    {
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("use-multi-threading")]
        public bool UseMultiThreading { get; set; } = true;

        [JsonPropertyName("min-threads")]
        public int MinThreads { get; set; } = 1;

        [JsonPropertyName("max-threads")]
        public int MaxThreads { get; set; } = 4;
    }
}
