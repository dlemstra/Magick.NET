// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Contains the perceptual hash of one image channel.
/// </summary>
public interface IChannelPerceptualHash
{
    /// <summary>
    /// Gets the channel.
    /// </summary>
    PixelChannel Channel { get; }

    /// <summary>
    /// Returns the hu perceptual hash for the specified colorspace.
    /// </summary>
    /// <param name="colorSpace">The colorspace to use.</param>
    /// <param name="index">The index to use.</param>
    /// <returns>The hu perceptual hash for the specified colorspace.</returns>
    double HuPhash(ColorSpace colorSpace, int index);

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="IChannelPerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    double SumSquaredDistance(IChannelPerceptualHash other);

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A string representation of this hash.</returns>
    string ToString();
}
