using Sponge.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Messages
{
    internal class ConfigurationMessage
    {
        [JsonPropertyName("status")]
        public StatusCode Status { get; set; } = StatusCode.OK;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public Configuration Content { get; set; } = new Configuration();

        public ConfigurationMessage(StatusCode status, string summary, Configuration content)
        {
            Status = status;
            Summary = summary;
            Content = content;
        }
    }
}
