// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// The normalized moments of one or more image channels.
/// </summary>
public sealed partial class Moments : IMoments
{
    private readonly Dictionary<PixelChannel, ChannelMoments> _channels = new();

    internal Moments(MagickImage image, IntPtr list)
    {
        if (list == IntPtr.Zero)
            return;

        foreach (var channel in image.Channels)
            AddChannel(list, channel);
    }

    /// <summary>
    /// Gets the moments for the all the channels.
    /// </summary>
    /// <returns>The moments for the all the channels.</returns>
    public IChannelMoments Composite()
        => _channels[PixelChannel.Composite];

    /// <summary>
    /// Gets the moments for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the moments for.</param>
    /// <returns>The moments for the specified channel.</returns>
    public IChannelMoments? GetChannel(PixelChannel channel)
    {
        _channels.TryGetValue(channel, out var moments);
        return moments;
    }

    internal static void DisposeList(IntPtr list)
    {
        if (list != IntPtr.Zero)
            NativeMoments.DisposeList(list);
    }

    private void AddChannel(IntPtr list, PixelChannel channel)
    {
        var instance = NativeMoments.GetInstance(list, channel);

        var result = ChannelMoments.Create(channel, instance);
        if (result is not null)
            _channels.Add(result.Channel, result);
    }
}
