// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulates the error information.
    /// </summary>
    public sealed class MagickErrorInfo : IMagickErrorInfo
    {
        internal MagickErrorInfo()
          : this(0, 0, 0)
        {
        }

        internal MagickErrorInfo(double meanErrorPerPixel, double normalizedMeanError, double normalizedMaximumError)
        {
            MeanErrorPerPixel = meanErrorPerPixel;
            NormalizedMeanError = normalizedMeanError;
            NormalizedMaximumError = normalizedMaximumError;
        }

        /// <summary>
        /// Gets the mean error per pixel computed when an image is color reduced.
        /// </summary>
        public double MeanErrorPerPixel { get; }

        /// <summary>
        /// Gets the normalized maximum error per pixel computed when an image is color reduced.
        /// </summary>
        public double NormalizedMaximumError { get; }

        /// <summary>
        /// Gets the normalized mean error per pixel computed when an image is color reduced.
        /// </summary>
        public double NormalizedMeanError { get; }
    }
}
