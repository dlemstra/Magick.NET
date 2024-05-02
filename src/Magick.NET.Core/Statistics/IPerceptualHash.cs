// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Contains the he perceptual hash of one or more image channels.
/// </summary>
public interface IPerceptualHash
{
    /// <summary>
    /// Returns the perceptual hash for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the has for.</param>
    /// <returns>The perceptual hash for the specified channel.</returns>
    IChannelPerceptualHash? GetChannel(PixelChannel channel);

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="IPerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    double SumSquaredDistance(IPerceptualHash other);

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A <see cref="string"/>.</returns>
    string ToString();
}
