// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Interface that describes an ICM/ICC color profile.
    /// </summary>
    public interface IColorProfile : IImageProfile
    {
        /// <summary>
        /// Gets the color space of the profile.
        /// </summary>
        ColorSpace ColorSpace { get; }

        /// <summary>
        /// Gets the copyright of the profile.
        /// </summary>
        string? Copyright { get; }

        /// <summary>
        /// Gets the description of the profile.
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// Gets the manufacturer of the profile.
        /// </summary>
        string? Manufacturer { get; }

        /// <summary>
        /// Gets the model of the profile.
        /// </summary>
        string? Model { get; }
    }
}