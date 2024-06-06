using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class ImageConfiguration
    {
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("use-multi-threading")]
        public bool UseMultiThreading { get; set; } = true;

        [JsonPropertyName("min-threads")]
        public int MinThreads { get; set; } = 1;

        [JsonPropertyName("max-threads")]
        public int MaxThreads { get; set; } = 4;

        [JsonPropertyName("params")]
        public Dictionary<string, ImageParameters> Parameters { get; set; } = new Dictionary<string, ImageParameters>();

        public ImageConfiguration()
        {
            Parameters.Add("avif", new ImageParameters(q: 60, effort: 4, useLossless: false, useSubsampling: true, keepMetadata: true));
            Parameters.Add("gif", new ImageParameters(effort: 8, useInterlace: false, keepMetadata: true));
            Parameters.Add("jpeg", new ImageParameters(q: 100, useInterlace: false, useSubsampling: true, keepMetadata: true));
            Parameters.Add("png", new ImageParameters(q: 80, effort: 6, compression: 6, useInterlace: false, keepMetadata: true));
            Parameters.Add("webp", new ImageParameters(q: 75, effort: 4, useLossless: false, keepMetadata: true));
        }
    }
}
