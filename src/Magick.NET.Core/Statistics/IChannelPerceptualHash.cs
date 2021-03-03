// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Contains the he perceptual hash of one image channel.
    /// </summary>
    public interface IChannelPerceptualHash
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        PixelChannel Channel { get; }

        /// <summary>
        /// SRGB hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The SRGB hu perceptual hash.</returns>
        double SrgbHuPhash(int index);

        /// <summary>
        /// Hclp hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The Hclp hu perceptual hash.</returns>
        double HclpHuPhash(int index);

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
}
