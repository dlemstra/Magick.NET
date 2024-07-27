// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;

namespace ImageMagick;

/// <summary>
/// Extension methods for the <see cref="IMagickGeometry"/> interface.
/// </summary>
public static class IMagickGeometryExtensions
{
    /// <summary>
    /// Sets the values of this class using the specified <see cref="Rectangle"/>.
    /// </summary>
    /// /// <param name="self">The geometry.</param>
    /// <param name="rectangle">The <see cref="Rectangle"/> to convert.</param>
    public static void SetFromRectangle(this IMagickGeometry self, Rectangle rectangle)
        => self?.Initialize(rectangle.X, rectangle.Y, (uint)rectangle.Width, (uint)rectangle.Height);

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="self">The geometry.</param>
    /// <returns>A <see cref="Color"/> instance.</returns>
    public static Rectangle ToRectangle(this IMagickGeometry self)
    {
        if (self is null)
            return default;

        return new Rectangle(self.X, self.Y, (int)self.Width, (int)self.Height);
    }
}
