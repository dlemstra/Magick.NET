// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is read.
    /// </summary>
    public sealed class Jp2ReadDefines : IReadDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jp2ReadDefines"/> class.
        /// </summary>
        public Jp2ReadDefines()
        {
        }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Jp2;

        /// <summary>
        /// Gets or sets the maximum number of quality layers to decode (jp2:quality-layers).
        /// </summary>
        public int? QualityLayers { get; set; }

        /// <summary>
        /// Gets or sets the number of highest resolution levels to be discarded (jp2:reduce-factor).
        /// </summary>
        public int? ReduceFactor { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (QualityLayers.HasValue)
                    yield return new MagickDefine(Format, "quality-layers", QualityLayers.Value);

                if (ReduceFactor.HasValue)
                    yield return new MagickDefine(Format, "reduce-factor", ReduceFactor.Value);
            }
        }
    }
}
