// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Windows.Media.Imaging;
using MediaPixelFormats = System.Windows.Media.PixelFormats;

namespace ImageMagick;

/// <content>
/// Contains code that is not compatible with .NET Core.
/// </content>
public static partial class IMagickImageExtentions
{
    /// <summary>
    /// Converts this instance to a <see cref="BitmapSource"/>.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="BitmapSource"/>.</returns>
    public static BitmapSource ToBitmapSource<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
        => ToBitmapSource(self, false);

    /// <summary>
    /// Converts this instance to a <see cref="BitmapSource"/>.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="BitmapSource"/>.</returns>
    public static BitmapSource ToBitmapSourceWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
        => ToBitmapSource(self, true);

    private static BitmapSource ToBitmapSource<TQuantumType>(this IMagickImage<TQuantumType> self, bool useDensity)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(nameof(self), self);

        var image = self;

        var mapping = "RGB";
        var format = MediaPixelFormats.Rgb24;

        try
        {
            if (self.ColorSpace == ColorSpace.CMYK && !image.HasAlpha)
            {
                mapping = "CMYK";
                format = MediaPixelFormats.Cmyk32;
            }
            else
            {
                if (image.ColorSpace != ColorSpace.sRGB)
                {
                    image = self.Clone();
                    image.ColorSpace = ColorSpace.sRGB;
                }

                if (image.HasAlpha)
                {
                    mapping = "BGRA";
                    format = MediaPixelFormats.Bgra32;
                }
            }

            var step = format.BitsPerPixel / 8;
            var stride = (int)image.Width * step;

            using var pixels = image.GetPixelsUnsafe();
            var bytes = pixels.ToByteArray(mapping);
            var dpi = GetDefaultDensity(image, useDensity ? DensityUnit.PixelsPerInch : DensityUnit.Undefined);
            return BitmapSource.Create((int)image.Width, (int)image.Height, dpi.X, dpi.Y, format, null, bytes, stride);
        }
        finally
        {
            if (!ReferenceEquals(self, image))
                image.Dispose();
        }
    }

    private static Density GetDefaultDensity(IMagickImage image, DensityUnit units)
    {
        if (units == DensityUnit.Undefined || (image.Density.X <= 0 || image.Density.Y <= 0))
            return new Density(96);

        return image.Density.ChangeUnits(units);
    }
}
