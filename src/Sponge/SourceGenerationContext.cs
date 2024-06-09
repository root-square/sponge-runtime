using Sponge.Entities.Configurations;
using Sponge.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge
{
    /// <summary>
    /// A source generation context for configurations.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false, PropertyNamingPolicy = JsonKnownNamingPolicy.KebabCaseLower, AllowTrailingCommas = true)]
    [JsonSerializable(typeof(Configuration))]
    [JsonSerializable(typeof(Response))]
    [JsonSerializable(typeof(ConfigurationResponse))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }
}
