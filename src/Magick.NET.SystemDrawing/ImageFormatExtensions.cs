// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing.Imaging;

namespace ImageMagick;

internal static class ImageFormatExtensions
{
    public static MagickFormat ToFormat(this ImageFormat self)
    {
        if (self.Guid == ImageFormat.Bmp.Guid || self.Guid == ImageFormat.MemoryBmp.Guid)
            return MagickFormat.Bmp;
        else if (self.Guid == ImageFormat.Gif.Guid)
            return MagickFormat.Gif;
        else if (self.Guid == ImageFormat.Icon.Guid)
            return MagickFormat.Icon;
        else if (self.Guid == ImageFormat.Jpeg.Guid)
            return MagickFormat.Jpeg;
        else if (self.Guid == ImageFormat.Png.Guid)
            return MagickFormat.Png;
        else if (self.Guid == ImageFormat.Tiff.Guid)
            return MagickFormat.Tiff;
        else
            throw new NotSupportedException($"Unsupported image format: {self}");
    }
}
