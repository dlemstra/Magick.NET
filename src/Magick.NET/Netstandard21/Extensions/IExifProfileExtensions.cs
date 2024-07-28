// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Extension methods for the <see cref="IExifProfile"/> interface.
/// </summary>
public static class IExifProfileExtensions
{
    /// <summary>
    /// Returns the thumbnail in the exif profile when available.
    /// </summary>
    /// <param name="self">The exif profile.</param>
    /// <returns>The thumbnail in the exif profile when available.</returns>
    public static IMagickImage<QuantumType>? CreateThumbnail(this IExifProfile self)
    {
        Throw.IfNull(nameof(self), self);

        var thumbnailLength = (int)self.ThumbnailLength;
        var thumbnailOffset = (int)self.ThumbnailOffset;

        if (thumbnailLength == 0 || thumbnailOffset == 0)
            return null;

        var data = self.ToReadOnlySpan();

        if (data.Length < (thumbnailOffset + thumbnailLength))
            return null;

        return new MagickImage(data.Slice(thumbnailOffset, thumbnailLength));
    }
}

#endif
