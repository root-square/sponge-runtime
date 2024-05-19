using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations
{
    public class TaskConfiguration
    {
        [JsonPropertyName("use-multi-threading")]
        public bool UseMultiThreading { get; set; } = true;

        [JsonPropertyName("min-threads")]
        public int MinThreads { get; set; } = 1;

        [JsonPropertyName("max-threads")]
        public int MaxThreads { get; set; } = 4;

        [JsonPropertyName("use-timeout")]
        public bool UseTimeout { get; set; } = true;

        [JsonPropertyName("timeout")]
        public int Timeout { get; set; } = 360;

        [JsonPropertyName("params")]
        public Dictionary<string, Parameters> Parameters { get; set; } = new Dictionary<string, Parameters>();

        public TaskConfiguration()
        {
            Parameters.Add("avif", new Parameters(q: 60, effort: 4, useLossless: false, useSubsampling: true, keepMetadata: true));
            Parameters.Add("gif", new Parameters(effort: 8, useInterlace: false, keepMetadata: true));
            Parameters.Add("jpeg", new Parameters(q: 100, useInterlace: false, useSubsampling: true, keepMetadata: true));
            Parameters.Add("png", new Parameters(q: 80, effort: 6, compression: 6, useInterlace: false, keepMetadata: true));
            Parameters.Add("webp", new Parameters(q: 75, effort: 4, useLossless: false, keepMetadata: true));
        }
    }
}
