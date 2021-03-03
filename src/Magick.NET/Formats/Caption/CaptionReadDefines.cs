// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Caption"/> image is read.
    /// </summary>
    public sealed class CaptionReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptionReadDefines"/> class.
        /// </summary>
        public CaptionReadDefines()
          : base(MagickFormat.Caption)
        {
        }

        /// <summary>
        /// Gets or sets a the maximum font pointsize (caption:max-pointsize).
        /// </summary>
        public double? MaxFontPointsize { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (MaxFontPointsize.HasValue)
                    yield return CreateDefine("max-pointsize", MaxFontPointsize.Value);
            }
        }
    }
}