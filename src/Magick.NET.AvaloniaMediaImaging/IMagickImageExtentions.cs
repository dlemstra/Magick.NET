// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ImageMagick;

/// <content>
/// Contains code that is not compatible with .NET Core.
/// </content>
public static partial class IMagickImageExtentions
{
    /// <summary>
    /// Converts this instance to a <see cref="WriteableBitmap"/> with a default DPI of 96x96.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="WriteableBitmap"/>.</returns>
    public static unsafe WriteableBitmap ToWriteableBitmap<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
    {
        var size = new PixelSize((int)self.Width, (int)self.Height);
        var dpi = new Vector(96, 96);
        var bitmap = new WriteableBitmap(size, dpi, PixelFormats.Rgba8888, AlphaFormat.Unpremul);

        using var framebuffer = bitmap.Lock();
        using var pixels = self.GetPixelsUnsafe();

        var destination = framebuffer.Address;
        for (var y = 0; y < self.Height; y++)
        {
            var bytes = pixels.ToByteArray(0, y, self.Width, 1, "RGBA");
            if (bytes != null)
                Marshal.Copy(bytes, 0, destination, bytes.Length);

            destination += framebuffer.RowBytes;
        }

        return bitmap;
    }
    
    /// <summary>
    /// Converts this instance to a <see cref="WriteableBitmap"/>.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="WriteableBitmap"/>.</returns>
    public static unsafe WriteableBitmap ToWriteableBitmapWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
    {
        var size = new PixelSize((int)self.Width, (int)self.Height);
        var density = new Vector(self.Density.X, self.Density.Y);
        var bitmap = new WriteableBitmap(size, density, PixelFormats.Rgba8888, AlphaFormat.Unpremul);

        using var framebuffer = bitmap.Lock();
        using var pixels = self.GetPixelsUnsafe();

        var destination = framebuffer.Address;
        for (var y = 0; y < self.Height; y++)
        {
            var bytes = pixels.ToByteArray(0, y, self.Width, 1, "RGBA");
            if (bytes != null)
                Marshal.Copy(bytes, 0, destination, bytes.Length);

            destination += framebuffer.RowBytes;
        }

        return bitmap;
    }
}
