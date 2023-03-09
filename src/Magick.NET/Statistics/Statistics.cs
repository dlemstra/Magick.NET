// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    internal sealed partial class Statistics : IStatistics
    {
        private readonly Dictionary<PixelChannel, ChannelStatistics> _channels;

        public Statistics(MagickImage image, IntPtr list, Channels channels)
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

        public IReadOnlyCollection<PixelChannel> Channels
            => _channels.Keys;

        public IChannelStatistics Composite()
            => GetChannel(PixelChannel.Composite)!;

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
