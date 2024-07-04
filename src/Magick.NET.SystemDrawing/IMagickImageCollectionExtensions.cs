// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Extension methods for the <see cref="IMagickImageCollection{QuantumType}"/> interface.
/// </summary>
public static class IMagickImageCollectionExtensions
{
    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Tiff"/>.
    /// </summary>
    /// <param name="self">The image collection.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Tiff"/>.</returns>
    public static Bitmap ToBitmap<TQuantumType>(this IMagickImageCollection<TQuantumType> self)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(nameof(self), self);

        if (self.Count == 1)
            return self[0].ToBitmap();

        return ToBitmap(self, ImageFormat.Tiff);
    }

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
    /// Supported formats are: Gif, Icon, Tiff.
    /// </summary>
    /// <param name="self">The image collection.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
    public static Bitmap ToBitmap<TQuantumType>(this IMagickImageCollection<TQuantumType> self, ImageFormat imageFormat)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(nameof(self), self);
        Throw.IfNull(nameof(imageFormat), imageFormat);

        var format = imageFormat.ToMagickFormat();

        foreach (var image in self)
        {
            image.Settings.Format = format;
        }

        var memStream = new MemoryStream();
        self.Write(memStream);
        memStream.Position = 0;

        /* Do not dispose the memStream, the bitmap owns it. */
        return new Bitmap(memStream);
    }
}
