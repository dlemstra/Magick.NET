// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Psd"/> image is written.
    /// </summary>
    public sealed class PsdWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PsdWriteDefines"/> class.
        /// </summary>
        public PsdWriteDefines()
          : base(MagickFormat.Psd)
        {
        }

        /// <summary>
        /// Gets or sets which additional info should be written to the output file.
        /// </summary>
        public PsdAdditionalInfoPart AdditionalInfo { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                yield return CreateDefine("additional-info", AdditionalInfo);
            }
        }
    }
}