// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

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
#if !NETSTANDARD2_1
    [Obsolete($"This method will be no longer be available for netstandard20 in the next major release.")]
#endif
    public static IMagickImage<QuantumType>? CreateThumbnail(this IExifProfile self)
    {
        Throw.IfNull(nameof(self), self);

        var thumbnailLength = self.ThumbnailLength;
        var thumbnailOffset = self.ThumbnailOffset;

        if (thumbnailLength == 0 || thumbnailOffset == 0)
            return null;

#if NETSTANDARD2_1
        var data = self.ToReadOnlySpan();

        if (data.Length < (thumbnailOffset + thumbnailLength))
            return null;

        return new MagickImage(data.Slice(thumbnailOffset, thumbnailLength));
#else
        var data = self.ToByteArray();

        if (data is null || data.Length < (thumbnailOffset + thumbnailLength))
            return null;

        return new MagickImage(data, thumbnailOffset, thumbnailLength);
#endif
    }
}
