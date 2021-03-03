// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for the deskew operation.
    /// </summary>
    public interface IDeskewSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the image should be auto cropped after deskewing.
        /// </summary>
        bool AutoCrop { get; set; }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        Percentage Threshold { get; set; }
    }
}
