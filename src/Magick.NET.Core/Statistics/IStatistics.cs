// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick statistics object.
/// </summary>
public interface IStatistics
{
    /// <summary>
    /// Gets the channels.
    /// </summary>
    IReadOnlyCollection<PixelChannel> Channels { get; }

    /// <summary>
    /// Returns the statistics for the all the channels.
    /// </summary>
    /// <returns>The statistics for the all the channels.</returns>
    IChannelStatistics Composite();

    /// <summary>
    /// Returns the statistics for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the statistics for.</param>
    /// <returns>The statistics for the specified channel.</returns>
    IChannelStatistics? GetChannel(PixelChannel channel);
}
