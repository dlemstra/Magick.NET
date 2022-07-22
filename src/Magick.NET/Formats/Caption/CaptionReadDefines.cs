// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Caption"/> image is read.
    /// </summary>
    public sealed class CaptionReadDefines : IReadDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptionReadDefines"/> class.
        /// </summary>
        public CaptionReadDefines()
        {
        }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Caption;

        /// <summary>
        /// Gets or sets a the maximum font pointsize (caption:max-pointsize).
        /// </summary>
        public double? MaxFontPointsize { get; set; }

        /// <summary>
        /// Gets or sets a the start font pointsize (caption:start-pointsize).
        /// </summary>
        public double? StartFontPointsize { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (MaxFontPointsize.HasValue)
                    yield return new MagickDefine(Format, "max-pointsize", MaxFontPointsize.Value);

                if (StartFontPointsize.HasValue)
                    yield return new MagickDefine(Format, "start-pointsize", StartFontPointsize.Value);
            }
        }
    }
}
