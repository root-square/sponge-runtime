using Sponge.Entities.Configurations.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Messages
{
    public class GeneralMessage
    {
        [JsonPropertyName("status")]
        public StatusCode Status { get; set; } = StatusCode.OK;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string? Content { get; set; } = string.Empty;

        public GeneralMessage(StatusCode status, string summary, string? content)
        {
            Status = status;
            Summary = summary;
            Content = content;
        }
    }
}
