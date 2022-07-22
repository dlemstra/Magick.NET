// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Bmp"/> image is written.
    /// </summary>
    public sealed class BmpWriteDefines : IWriteDefines
    {
        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Bmp;

        /// <summary>
        /// Gets or sets the subtype that will be used (bmp:subtype).
        /// </summary>
        public BmpSubtype? Subtype { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (Subtype.HasValue)
                    yield return new MagickDefine(Format, "subtype", Subtype.Value);
            }
        }
    }
}
