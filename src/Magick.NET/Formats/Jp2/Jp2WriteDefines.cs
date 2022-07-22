// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is written.
    /// </summary>
    public sealed class Jp2WriteDefines : IWriteDefines
    {
        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Jp2;

        /// <summary>
        /// Gets or sets the number of resolutions to encode (jp2:number-resolutions).
        /// </summary>
        public int? NumberResolutions { get; set; }

        /// <summary>
        /// Gets or sets the progression order (jp2:progression-order).
        /// </summary>
        public Jp2ProgressionOrder? ProgressionOrder { get; set; }

        /// <summary>
        /// Gets or sets the quality layer PSNR, given in dB. The order is from left to right in ascending order (jp2:quality).
        /// </summary>
        public IEnumerable<float>? Quality { get; set; }

        /// <summary>
        /// Gets or sets the compression ratio values. Each value is a factor of compression, thus 20 means 20 times compressed.
        /// The order is from left to right in descending order. A final lossless quality layer is signified by the value 1 (jp2:rate).
        /// </summary>
        public IEnumerable<float>? Rate { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (NumberResolutions.HasValue)
                    yield return new MagickDefine(Format, "number-resolutions", NumberResolutions.Value);

                if (ProgressionOrder.HasValue)
                    yield return new MagickDefine(Format, "progression-order", ProgressionOrder.Value);

                var quality = MagickDefine.Create(Format, "quality", Quality);
                if (quality is not null)
                    yield return quality;

                var rate = MagickDefine.Create(Format, "rate", Rate);
                if (rate is not null)
                    yield return rate;
            }
        }
    }
}
