// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Bmp"/> image is written.
    /// </summary>
    public sealed class BmpWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BmpWriteDefines"/> class.
        /// </summary>
        public BmpWriteDefines()
          : base(MagickFormat.Bmp)
        {
        }

        /// <summary>
        /// Gets or sets the subtype that will be used (bmp:subtype).
        /// </summary>
        public BmpSubtype? Subtype { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (Subtype.HasValue)
                    yield return CreateDefine("subtype", Subtype.Value);
            }
        }
    }
}