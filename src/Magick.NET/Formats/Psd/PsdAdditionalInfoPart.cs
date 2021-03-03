// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies which additional info should be written to the output file.
    /// </summary>
    public enum PsdAdditionalInfoPart
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Only select the info that does not use geometry.
        /// </summary>
        Selective,
    }
}