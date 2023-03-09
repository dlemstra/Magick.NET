// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageStatistics object.
    /// </summary>
    public sealed partial class Statistics : IStatistics
    {
        private readonly Dictionary<PixelChannel, ChannelStatistics> _channels;

        internal Statistics(MagickImage image, IntPtr list, Channels channels)
        {
            _channels = new Dictionary<PixelChannel, ChannelStatistics>();

            if (list == IntPtr.Zero)
                return;

            foreach (var channel in image.Channels)
            {
                if ((((int)channels >> (int)channel) & 0x01) != 0)
                    AddChannel(list, channel);
            }

            AddChannel(list, PixelChannel.Composite);
        }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        public IReadOnlyCollection<PixelChannel> Channels
            => _channels.Keys;

        /// <summary>
        /// Returns the statistics for the all the channels.
        /// </summary>
        /// <returns>The statistics for the all the channels.</returns>
        public IChannelStatistics Composite()
            => GetChannel(PixelChannel.Composite)!;

        /// <summary>
        /// Returns the statistics for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the statistics for.</param>
        /// <returns>The statistics for the specified channel.</returns>
        public IChannelStatistics? GetChannel(PixelChannel channel)
        {
            _channels.TryGetValue(channel, out var channelStatistics);
            return channelStatistics;
        }

        internal static void DisposeList(IntPtr list)
        {
            if (list != IntPtr.Zero)
                NativeStatistics.DisposeList(list);
        }

        private void AddChannel(IntPtr list, PixelChannel channel)
        {
            var instance = NativeStatistics.GetInstance(list, channel);

            var result = ChannelStatistics.Create(channel, instance);
            if (result is not null)
                _channels.Add(result.Channel, result);
        }
    }
}
