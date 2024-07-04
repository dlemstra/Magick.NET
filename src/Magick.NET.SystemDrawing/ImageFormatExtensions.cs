// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ImageMagick;

internal static class ImageFormatExtensions
{
    private static readonly Dictionary<Guid, MagickFormat> _formats = new()
    {
        [ImageFormat.Bmp.Guid] = MagickFormat.Bmp,
        [ImageFormat.MemoryBmp.Guid] = MagickFormat.Bmp,
        [ImageFormat.Gif.Guid] = MagickFormat.Gif,
        [ImageFormat.Icon.Guid] = MagickFormat.Icon,
        [ImageFormat.Jpeg.Guid] = MagickFormat.Jpeg,
        [ImageFormat.Png.Guid] = MagickFormat.Png,
        [ImageFormat.Tiff.Guid] = MagickFormat.Tiff,
    };

    public static MagickFormat ToMagickFormat(this ImageFormat self)
        => _formats.TryGetValue(self.Guid, out var format)
            ? format
            : throw new NotSupportedException($"Unsupported image format: {self}");
}
