// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageMagick;

/// <content>
/// Contains code that is not compatible with .NET Core.
/// </content>
public static partial class IMagickImageExtentions
{
    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <param name="bitmap">The bitmap to read the image from.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static void Read<TQuantumType>(this IMagickImage<TQuantumType> self, Bitmap bitmap)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(self);
        Throw.IfNull(bitmap);

        using var memStream = new MemoryStream();
        if (IsSupportedImageFormat(bitmap.RawFormat))
            bitmap.Save(memStream, bitmap.RawFormat);
        else
            bitmap.Save(memStream, ImageFormat.Bmp);

        memStream.Position = 0;
        self.Read(memStream);
    }

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
    public static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
        => ToBitmap(self, withDensity: false);

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
    public static Bitmap ToBitmapWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self)
        where TQuantumType : struct, IConvertible
        => ToBitmap(self, withDensity: true);

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
    /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
    public static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self, ImageFormat imageFormat)
        where TQuantumType : struct, IConvertible
        => ToBitmap(self, imageFormat, withDensity: false);

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
    /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
    /// </summary>
    /// <param name="self">The image.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
    public static Bitmap ToBitmapWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self, ImageFormat imageFormat)
        where TQuantumType : struct, IConvertible
        => ToBitmap(self, imageFormat, withDensity: true);

    private static void CopyPixels<TQuantumType>(IUnsafePixelCollection<TQuantumType> pixels, BitmapData data, IMagickImage<TQuantumType> image, string mapping)
        where TQuantumType : struct, IConvertible
    {
        var destination = data.Scan0;
        for (var y = 0; y < image.Height; y++)
        {
            var bytes = pixels.ToByteArray(0, y, image.Width, 1, mapping);
            if (bytes is not null)
                Marshal.Copy(bytes, 0, destination, bytes.Length);

            destination += data.Stride;
        }
    }

    private static unsafe void CopyPixelsOptimized<TQuantumType>(IUnsafePixelCollection<TQuantumType> pixels, BitmapData data, IMagickImage<TQuantumType> image, uint blueIndex, uint greenIndex, uint redIndex, int alphaIndex)
        where TQuantumType : struct, IConvertible
    {
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        var source = (TQuantumType*)pixels.GetAreaPointer(0, 0, image.Width, image.Height);
#pragma warning restore CS8500
        var destination = (byte*)data.Scan0;
        var increment = alphaIndex == -1 ? 3 : 4;

        var quantum = QuantumScaler.Create<TQuantumType>();
        for (var y = 0; y < image.Height; y++)
        {
            var startOfRow = destination;

            for (var x = 0; x < image.Width; x++)
            {
                *destination = quantum.ScaleToByte(*(source + blueIndex));
                *(destination + 1) = quantum.ScaleToByte(*(source + greenIndex));
                *(destination + 2) = quantum.ScaleToByte(*(source + redIndex));
                if (alphaIndex != -1)
                    *(destination + 3) = quantum.ScaleToByte(*(source + alphaIndex));

                source += pixels.Channels;
                destination += increment;
            }

            destination = startOfRow + data.Stride;
        }
    }

    private static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self, bool withDensity)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(self);

        var image = self;

        var format = PixelFormat.Format24bppRgb;
        var mapping = "BGR";
        if (image.HasAlpha)
        {
            format = PixelFormat.Format32bppArgb;
            mapping = "BGRA";
        }

        try
        {
            if (!IssRGBCompatibleColorspace(image.ColorSpace))
            {
                image = self.Clone();
                image.ColorSpace = ColorSpace.sRGB;
            }

            using var pixels = image.GetPixelsUnsafe();
            var blueIndex = pixels.GetChannelIndex(PixelChannel.Blue);
            var greenIndex = pixels.GetChannelIndex(PixelChannel.Green);
            var redIndex = pixels.GetChannelIndex(PixelChannel.Red);
            var alphaIndex = pixels.GetChannelIndex(PixelChannel.Alpha);

            var bitmap = new Bitmap((int)image.Width, (int)image.Height, format);
            var data = bitmap.LockBits(new Rectangle(0, 0, (int)image.Width, (int)image.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            try
            {
                if (format == PixelFormat.Format32bppArgb)
                {
                    if (blueIndex is not null && greenIndex is not null && redIndex is not null && alphaIndex is not null)
                        CopyPixelsOptimized(pixels, data, image, blueIndex.Value, greenIndex.Value, redIndex.Value, (int)alphaIndex.Value);
                    else
                        CopyPixels(pixels, data, image, mapping);
                }
                else
                {
                    if (blueIndex is not null && greenIndex is not null && redIndex is not null)
                        CopyPixelsOptimized(pixels, data, image, blueIndex.Value, greenIndex.Value, redIndex.Value, -1);
                    else
                        CopyPixels(pixels, data, image, mapping);
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            if (withDensity)
                SetBitmapDensity(self, bitmap);

            return bitmap;
        }
        finally
        {
            if (!ReferenceEquals(self, image))
                image.Dispose();
        }
    }

    private static Bitmap ToBitmap<TQuantumType>(IMagickImage<TQuantumType> self, ImageFormat imageFormat, bool withDensity)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(self);
        Throw.IfNull(imageFormat);

        var format = imageFormat.ToMagickFormat();

        var memStream = new MemoryStream();
        self.Write(memStream, format);
        memStream.Position = 0;

        /* Do not dispose the memStream, the bitmap owns it. */
        var bitmap = new Bitmap(memStream);
        if (withDensity)
            SetBitmapDensity(self, bitmap);

        return bitmap;
    }

    private static void SetBitmapDensity<TQuantumType>(IMagickImage<TQuantumType> image, Bitmap bitmap)
        where TQuantumType : struct, IConvertible
    {
        var dpi = GetDefaultDensity(image);
        bitmap.SetResolution((float)dpi.X, (float)dpi.Y);
    }

    private static Density GetDefaultDensity(IMagickImage image)
    {
        if (image.Density.X <= 0 || image.Density.Y <= 0)
            return new Density(96);

        return image.Density.ChangeUnits(DensityUnit.PixelsPerInch);
    }

    private static bool IsSupportedImageFormat(ImageFormat format)
        => format.Guid.Equals(ImageFormat.Bmp.Guid) ||
           format.Guid.Equals(ImageFormat.Gif.Guid) ||
           format.Guid.Equals(ImageFormat.Icon.Guid) ||
           format.Guid.Equals(ImageFormat.Jpeg.Guid) ||
           format.Guid.Equals(ImageFormat.Png.Guid) ||
           format.Guid.Equals(ImageFormat.Tiff.Guid);

    private static bool IssRGBCompatibleColorspace(ColorSpace colorSpace)
        => colorSpace == ColorSpace.sRGB ||
           colorSpace == ColorSpace.RGB ||
           colorSpace == ColorSpace.scRGB ||
           colorSpace == ColorSpace.Transparent ||
           colorSpace == ColorSpace.Gray ||
           colorSpace == ColorSpace.LinearGray;
}
