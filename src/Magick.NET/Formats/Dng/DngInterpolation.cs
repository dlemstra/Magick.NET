// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Defines the dng interpolation quality.
    /// </summary>
    public enum DngInterpolation
    {
        /// <summary>
        /// Interpolation will be disabled.
        /// </summary>
        Disabled = -1,

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        Linear = 0,

        /// <summary>
        /// VNG interpolation.
        /// </summary>
        Vng = 1,

        /// <summary>
        /// PPG interpolation.
        /// </summary>
        Ppg = 2,

        /// <summary>
        /// AHD interpolation.
        /// </summary>
        Ahd = 3,

        /// <summary>
        /// DCB interpolation.
        /// </summary>
        Dcb = 4,

        /// <summary>
        /// DHT interpolation.
        /// </summary>
        Dht = 11,

        /// <summary>
        /// Modified AHD interpolation (by Anton Petrusevich).
        /// </summary>
        ModifiedAhd = 12,
    }
}
