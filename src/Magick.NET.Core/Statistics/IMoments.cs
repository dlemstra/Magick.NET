// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// The normalized moments of one or more image channels.
/// </summary>
public interface IMoments
{
    /// <summary>
    /// Gets the moments for the all the channels.
    /// </summary>
    /// <returns>The moments for the all the channels.</returns>
    IChannelMoments? Composite();

    /// <summary>
    /// Gets the moments for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the moments for.</param>
    /// <returns>The moments for the specified channel.</returns>
    IChannelMoments? GetChannel(PixelChannel channel);
}
