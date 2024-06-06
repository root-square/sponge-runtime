using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class LinkConfiguration
    {
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("priority")]
        public int Priority { get; set; } = 1; // Note: 0 = BEFORE RUNTIME START-UP / 1 = AFTER RUNTIME START-UP

        [JsonPropertyName("target")]
        public string? Target { get; set; } = "base.bin";

        [JsonPropertyName("sync-io")]
        public bool SyncIO { get; set; } = true;

        [JsonPropertyName("sync-lifecycle")]
        public bool SyncLifecycle { get; set; } = true;
    }
}
