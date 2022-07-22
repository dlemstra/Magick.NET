// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Psd"/> image is written.
    /// </summary>
    public sealed class PsdWriteDefines : IWriteDefines
    {
        /// <summary>
        /// Gets or sets which additional info should be written to the output file.
        /// </summary>
        public PsdAdditionalInfoPart AdditionalInfo { get; set; }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Psd;

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                yield return new MagickDefine(Format, "additional-info", AdditionalInfo);
            }
        }
    }
}
