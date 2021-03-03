// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulates the error information.
    /// </summary>
    public interface IMagickErrorInfo
    {
        /// <summary>
        /// Gets the mean error per pixel computed when an image is color reduced.
        /// </summary>
        double MeanErrorPerPixel { get; }

        /// <summary>
        /// Gets the normalized maximum error per pixel computed when an image is color reduced.
        /// </summary>
        double NormalizedMaximumError { get; }

        /// <summary>
        /// Gets the normalized mean error per pixel computed when an image is color reduced.
        /// </summary>
        double NormalizedMeanError { get; }
    }
}
