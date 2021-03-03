// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is written.
    /// </summary>
    public sealed class Jp2WriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jp2WriteDefines"/> class.
        /// </summary>
        public Jp2WriteDefines()
          : base(MagickFormat.Jp2)
        {
        }

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
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (NumberResolutions.HasValue)
                    yield return CreateDefine("number-resolutions", NumberResolutions.Value);

                if (ProgressionOrder.HasValue)
                    yield return CreateDefine("progression-order", ProgressionOrder.Value);

                var quality = CreateDefine("quality", Quality);
                if (quality != null)
                    yield return quality;

                var rate = CreateDefine("rate", Rate);
                if (rate != null)
                    yield return rate;
            }
        }
    }
}