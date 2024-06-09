using Sponge.Entities.Configurations.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Responses
{
    public class Response
    {
        [JsonPropertyName("status")]
        public virtual ResponseCode Status { get; set; } = ResponseCode.OK;

        [JsonPropertyName("summary")]
        public virtual string Summary { get; set; } = string.Empty;

        public Response(ResponseCode status, string summary)
        {
            Status = status;
            Summary = summary;
        }
    }
}
