// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Bmp"/> image is read.
    /// </summary>
    public sealed class BmpReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BmpReadDefines"/> class.
        /// </summary>
        public BmpReadDefines()
          : base(MagickFormat.Bmp)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the file size should be ignored (bmp:ignore-filesize).
        /// </summary>
        public bool IgnoreFileSize { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (IgnoreFileSize)
                    yield return CreateDefine("ignore-filesize", IgnoreFileSize);
            }
        }
    }
}