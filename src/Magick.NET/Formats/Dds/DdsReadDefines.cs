// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Dds"/> image is read.
    /// </summary>
    public sealed class DdsReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DdsReadDefines"/> class.
        /// </summary>
        public DdsReadDefines()
          : base(MagickFormat.Dds)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating wether mipmaps should be skipped (dds:skip-mipmaps).
        /// </summary>
        public bool? SkipMipmaps { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (SkipMipmaps.HasValue)
                    yield return CreateDefine("skip-mipmaps", SkipMipmaps.Value);
            }
        }
    }
}