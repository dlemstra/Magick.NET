// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Bmp"/> image is read.
    /// </summary>
    public sealed class BmpReadDefines : IReadDefines
    {
        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Bmp;

        /// <summary>
        /// Gets or sets a value indicating whether the file size should be ignored (bmp:ignore-filesize).
        /// </summary>
        public bool IgnoreFileSize { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (IgnoreFileSize)
                    yield return new MagickDefine(Format, "ignore-filesize", IgnoreFileSize);
            }
        }
    }
}
