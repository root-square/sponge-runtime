using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Entities.Configurations.Internals
{
    public class ImageParameters
    {
        [JsonPropertyName("q")]
        public int? Q { get; set; }

        [JsonPropertyName("effort")]
        public int? Effort { get; set; }

        [JsonPropertyName("compression")]
        public int? Compression { get; set; }

        [JsonPropertyName("use-lossless")]
        public bool? UseLossless { get; set; }

        [JsonPropertyName("use-interlace")]
        public bool? UseInterlace { get; set; }

        [JsonPropertyName("use-subsampling")]
        public bool? UseSubsampling { get; set; }

        [JsonPropertyName("keep-metadata")]
        public bool? KeepMetadata { get; set; }

        public ImageParameters(int? q = null, int? effort = null, int? compression = null, bool? useLossless = null, bool? useInterlace = null, bool? useSubsampling = null, bool? keepMetadata = null)
        {
            Q = q;
            Effort = effort;
            Compression = compression;
            UseLossless = useLossless;
            UseInterlace = useInterlace;
            UseSubsampling = useSubsampling;
            KeepMetadata = keepMetadata;
        }
    }
}
