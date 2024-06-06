using Sponge.Entities.Configurations.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations
{
    public class Configuration
    {
        [JsonPropertyName("runtime")]
        public RuntimeConfiguration Runtime { get; set; } = new RuntimeConfiguration();

        [JsonPropertyName("logging")]
        public LoggingConfiguration Logging { get; set; } = new LoggingConfiguration();

        [JsonPropertyName("diagnostics")]
        public DiagnosticsConfiguration Diagnostics { get; set; } = new DiagnosticsConfiguration();

        [JsonPropertyName("link")]
        public LinkConfiguration Link { get; set; } = new LinkConfiguration();

        [JsonPropertyName("caching")]
        public CachingConfiguration Caching { get; set; } = new CachingConfiguration();

        [JsonPropertyName("audio")]
        public AudioConfiguration Audio { get; set; } = new AudioConfiguration();

        [JsonPropertyName("image")]
        public ImageConfiguration Image { get; set; } = new ImageConfiguration();
    }
}
