using NetVips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Functions.VIPS
{
    /// <summary>
    /// Provides image functions based on libvips.
    /// </summary>
    public static class Vips
    {
        /// <summary>
        /// Convert the image to AVIF format. <br/>
        /// ▶ This uses such codecs: SVT-AV1(encoder), dav1d(decoder). <br/>
        /// ▶ Recommanded Options: q=60, effort=4, useLossless=False, useSubsampling=True, keepMetadata=True 
        /// </summary>
        /// <param name="stream">An image stream to convert</param>
        /// <param name="q">Numerized image quality(1~100)</param>
        /// <param name="effort">Numerized CPU effort(1~10)</param>
        /// <param name="useLossless">Enable lossless compression</param>
        /// <param name="useSubsampling">Enable chroma sub-sampling</param>
        /// <param name="keepMetadata">Keep the image metadata</param>
        public static void ConvertToAvif(Stream stream, int? q = null, int? effort = null, bool? useLossless = null, bool? useSubsampling = null, bool? keepMetadata = null)
        {
            using (var image = Image.NewFromStream(stream))
            {
                image.HeifsaveStream(stream, q: q, lossless: useLossless, compression: Enums.ForeignHeifCompression.Av1, effort: effort, encoder: Enums.ForeignHeifEncoder.Svt, subsampleMode: useSubsampling == true ? Enums.ForeignSubsample.Auto : Enums.ForeignSubsample.Off, keep: keepMetadata == true ? Enums.ForeignKeep.All : Enums.ForeignKeep.None);
            }
        }

        /// <summary>
        /// Convert the image to GIF format. <br/>
        /// ▶ Recommanded Options: effort=8, useInterlace=False, keepMetadata=True 
        /// </summary>
        /// <param name="stream">An image stream to convert</param>
        /// <param name="effort">Numerized CPU effort(1~10)</param>
        /// <param name="useInterlace">Enable interlace mode</param>
        /// <param name="keepMetadata">Keep the image metadata</param>
        public static void ConvertToGif(Stream stream, int? effort = null, bool? useInterlace = null, bool? keepMetadata = null)
        {
            using (var image = Image.NewFromStream(stream))
            {
                image.GifsaveStream(stream, effort: effort, interlace: useInterlace, keep: keepMetadata == true ? Enums.ForeignKeep.All : Enums.ForeignKeep.None);
            }
        }

        /// <summary>
        /// Convert the image to JPEG format. <br/>
        /// ▶ Recommanded Options: q=100, useInterlace=False, useSubsampling=True, keepMetadata=True 
        /// </summary>
        /// <param name="stream">An image stream to convert</param>
        /// <param name="q">Numerized image quality(1~100)</param>
        /// <param name="useInterlace">Enable interlace mode</param>
        /// <param name="useSubsampling">Enable chroma sub-sampling</param>
        /// <param name="keepMetadata">Keep the image metadata</param>
        public static void ConvertToJpeg(Stream stream, int? q = null, bool? useInterlace = null, bool? useSubsampling = null, bool? keepMetadata = null)
        {
            using (var image = Image.NewFromStream(stream))
            {
                image.JpegsaveStream(stream, q: q, interlace: useInterlace, subsampleMode: useSubsampling == true ? Enums.ForeignSubsample.Auto : Enums.ForeignSubsample.Off, keep: keepMetadata == true ? Enums.ForeignKeep.All : Enums.ForeignKeep.None);
            }
        }

        /// <summary>
        /// Convert the image to PNG format. <br/>
        /// ▶ Recommanded Options: q=80, effort=6, compression=6, useInterlace=False, keepMetadata=True 
        /// </summary>
        /// <param name="stream">An image stream to convert</param>
        /// <param name="q">Numerized image quality(1~100)</param>
        /// <param name="effort">Numerized CPU effort(1~10)</param>
        /// <param name="compression">Numerized compression level(1~10)</param>
        /// <param name="useInterlace">Enable interlace mode</param>
        /// <param name="keepMetadata">Keep the image metadata</param>
        public static void ConvertToPng(Stream stream, int? q = null, int? effort = null, int? compression = null, bool? useInterlace = null, bool? keepMetadata = null)
        {
            using (var image = Image.NewFromStream(stream))
            {
                // Note: When converting an image that is less than 16px from AVIF to PNG, libvips has failed to convert.
                // Note: If this function doesn't work, change such options: bitdepth=8, profile="sRGB", filter=Enums.ForeignPngFilter.None
                image.PngsaveStream(stream, q: q, effort: effort, compression: compression, interlace: useInterlace, keep: keepMetadata == true ? Enums.ForeignKeep.All : Enums.ForeignKeep.None); // An image to convert to PNG must be more than 16*16px.
            }
        }

        /// <summary>
        /// Convert the image to WEBP format. <br/>
        /// ▶ Recommanded Options: q=75, effort=4, useLossless=False, keepMetadata=True 
        /// </summary>
        /// <param name="stream">An image stream to convert</param>
        /// <param name="q">Numerized image quality(1~100)</param>
        /// <param name="effort">Numerized CPU effort(1~10)</param>
        /// <param name="useLossless">Enable lossless compression</param>
        /// <param name="keepMetadata">Keep the image metadata</param>
        public static void ConvertToWebp(Stream stream, int? q = null, int? effort = null, bool? useLossless = null, bool? keepMetadata = null)
        {
            using (var image = Image.NewFromStream(stream))
            {
                image.WebpsaveStream(stream, q: q, effort: effort, lossless: useLossless, keep: keepMetadata == true ? Enums.ForeignKeep.All : Enums.ForeignKeep.None);
            }
        }
    }
}
