// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Heic"/> image is read.
    /// </summary>
    public sealed class HeicReadDefines : IReadDefines
    {
        /// <summary>
        /// Gets or sets a value indicating whether the depth image should be read (heic:depth-image).
        /// </summary>
        public bool? DepthImage { get; set; }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Heic;

        /// <summary>
        /// Gets or sets a value indicating whether the orientation should be preserved (heic:preserve-orientation).
        /// </summary>
        public bool? PreserveOrientation { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (DepthImage == true)
                    yield return new MagickDefine(Format, "depth-image", DepthImage.Value);

                if (PreserveOrientation == true)
                    yield return new MagickDefine(Format, "preserve-orientation", PreserveOrientation.Value);
            }
        }
    }
}
