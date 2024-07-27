// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Extension methods for the <see cref="Density"/> class.
/// </summary>
public static class DensityExtensions
{
    /// <summary>
    /// Returns a <see cref="MagickGeometry"/> based on the specified width and height.
    /// </summary>
    /// <param name="self">The density.</param>
    /// <param name="width">The width in cm or inches.</param>
    /// <param name="height">The height in cm or inches.</param>
    /// <returns>A <see cref="MagickGeometry"/> based on the specified width and height in cm or inches.</returns>
    public static IMagickGeometry? ToGeometry(this Density self, double width, double height)
    {
        if (self is null)
            return null;

        var pixelWidth = (uint)(width * self.X);
        var pixelHeight = (uint)(height * self.Y);

        return new MagickGeometry(pixelWidth, pixelHeight);
    }
}
