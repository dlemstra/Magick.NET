// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jxl"/> image is written.
    /// </summary>
    public sealed class JxlWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JxlWriteDefines"/> class.
        /// </summary>
        public JxlWriteDefines()
          : base(MagickFormat.Jxl)
        {
        }

        /// <summary>
        /// Gets or sets the jpeg-xl encoding effort. Valid values are in the range of 3 (falcon) to 9 (tortois) (jxl:dct-method).
        /// </summary>
        public int? Effort { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (Effort.HasValue)
                    yield return CreateDefine("effort", Effort.Value);
            }
        }
    }
}