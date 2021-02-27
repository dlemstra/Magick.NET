// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

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

        internal Statistics(MagickImage image, IntPtr list)
        {
            if (list == IntPtr.Zero)
                return;

            _channels = new Dictionary<PixelChannel, ChannelStatistics>();
            foreach (PixelChannel channel in image.Channels)
                AddChannel(list, channel);

            AddChannel(list, PixelChannel.Composite);
        }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        public IEnumerable<PixelChannel> Channels
            => _channels.Keys;

        /// <summary>
        /// Returns the statistics for the all the channels.
        /// </summary>
        /// <returns>The statistics for the all the channels.</returns>
        public IChannelStatistics Composite()
            => GetChannel(PixelChannel.Composite);

        /// <summary>
        /// Returns the statistics for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the statistics for.</param>
        /// <returns>The statistics for the specified channel.</returns>
        public IChannelStatistics GetChannel(PixelChannel channel)
        {
            _channels.TryGetValue(channel, out ChannelStatistics channelStatistics);
            return channelStatistics;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Statistics"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="Statistics"/> with.</param>
        /// <returns>Truw when the specified object is equal to the current <see cref="Statistics"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as IStatistics);
        }

        /// <summary>
        /// Determines whether the specified image statistics is equal to the current <see cref="Statistics"/>.
        /// </summary>
        /// <param name="other">The image statistics to compare this <see cref="Statistics"/> with.</param>
        /// <returns>True when the specified image statistics is equal to the current <see cref="Statistics"/>.</returns>
        public bool Equals(IStatistics other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var otherChannels = new List<PixelChannel>(other.Channels);

            if (_channels.Count != otherChannels.Count)
                return false;

            foreach (PixelChannel channel in _channels.Keys)
            {
                if (!otherChannels.Contains(channel))
                    return false;

                var otherChannel = other.GetChannel(channel);

                if (!_channels[channel].Equals(otherChannel))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            var hashCode = _channels.GetHashCode();

            foreach (PixelChannel channel in _channels.Keys)
            {
                hashCode ^= _channels[channel].GetHashCode();
            }

            return hashCode;
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
            if (result != null)
                _channels.Add(result.Channel, result);
        }
    }
}
