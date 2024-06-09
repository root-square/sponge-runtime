using Sponge.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Responses
{
    internal class ConfigurationResponse : Response
    {
        [JsonPropertyName("content")]
        public Configuration Content { get; set; } = new Configuration();

        public ConfigurationResponse(ResponseCode status, string message, Configuration content) : base(status, message)
        {
            Status = status;
            Message = message;
            Content = content;
        }
    }
}
