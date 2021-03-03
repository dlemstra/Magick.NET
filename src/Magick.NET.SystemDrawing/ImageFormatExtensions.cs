// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing.Imaging;

namespace ImageMagick
{
    internal static class ImageFormatExtensions
    {
        public static MagickFormat ToFormat(this ImageFormat self)
        {
            if (self == ImageFormat.Bmp || self == ImageFormat.MemoryBmp)
                return MagickFormat.Bmp;
            else if (self == ImageFormat.Gif)
                return MagickFormat.Gif;
            else if (self == ImageFormat.Icon)
                return MagickFormat.Icon;
            else if (self == ImageFormat.Jpeg)
                return MagickFormat.Jpeg;
            else if (self == ImageFormat.Png)
                return MagickFormat.Png;
            else if (self == ImageFormat.Tiff)
                return MagickFormat.Tiff;
            else
                throw new NotSupportedException("Unsupported image format: " + self.ToString());
        }
    }
}