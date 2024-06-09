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

        [JsonPropertyName("message")]
        public virtual string Message { get; set; } = string.Empty;

        public Response(ResponseCode status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
