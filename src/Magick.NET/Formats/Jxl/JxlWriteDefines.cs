// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jxl"/> image is written.
    /// </summary>
    public sealed class JxlWriteDefines : IWriteDefines
    {
        /// <summary>
        /// Gets or sets the jpeg-xl encoding effort. Valid values are in the range of 3 (falcon) to 9 (tortois) (jxl:effort).
        /// </summary>
        public int? Effort { get; set; }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Jxl;

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (Effort.HasValue)
                    yield return new MagickDefine(Format, "effort", Effort.Value);
            }
        }
    }
}
