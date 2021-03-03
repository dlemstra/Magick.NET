// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Tiff"/> image is read.
    /// </summary>
    public sealed class TiffReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TiffReadDefines"/> class.
        /// </summary>
        public TiffReadDefines()
          : base(MagickFormat.Tiff)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the exif profile should be ignored (tiff:exif-properties).
        /// </summary>
        public bool? IgnoreExifPoperties { get; set; }

        /// <summary>
        /// Gets or sets the tiff tags that should be ignored (tiff:ignore-tags).
        /// </summary>
        public IEnumerable<string>? IgnoreTags { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (IgnoreExifPoperties.Equals(true))
                    yield return CreateDefine("exif-properties", false);

                var ignoreTags = CreateDefine("ignore-tags", IgnoreTags);
                if (ignoreTags != null)
                    yield return ignoreTags;
            }
        }
    }
}