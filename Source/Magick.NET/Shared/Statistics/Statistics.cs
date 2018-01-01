// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
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
    public sealed partial class Statistics : IEquatable<Statistics>
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
        /// Determines whether the specified <see cref="Statistics"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Statistics"/> to compare.</param>
        /// <param name="right"> The second <see cref="Statistics"/> to compare.</param>
        public static bool operator ==(Statistics left, Statistics right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Statistics"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Statistics"/> to compare.</param>
        /// <param name="right"> The second <see cref="Statistics"/> to compare.</param>
        public static bool operator !=(Statistics left, Statistics right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns the statistics for the all the channels.
        /// </summary>
        /// <returns>The statistics for the all the channels.</returns>
        public ChannelStatistics Composite()
        {
            return GetChannel(PixelChannel.Composite);
        }

        /// <summary>
        /// Returns the statistics for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the statistics for.</param>
        /// <returns>The statistics for the specified channel.</returns>
        public ChannelStatistics GetChannel(PixelChannel channel)
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

            return Equals(obj as Statistics);
        }

        /// <summary>
        /// Determines whether the specified image statistics is equal to the current <see cref="Statistics"/>.
        /// </summary>
        /// <param name="other">The image statistics to compare this <see cref="Statistics"/> with.</param>
        /// <returns>True when the specified image statistics is equal to the current <see cref="Statistics"/>.</returns>
        public bool Equals(Statistics other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (_channels.Count != other._channels.Count)
                return false;

            foreach (PixelChannel channel in _channels.Keys)
            {
                if (!other._channels.ContainsKey(channel))
                    return false;

                if (!_channels[channel].Equals(other._channels[channel]))
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
            int hashCode = _channels.GetHashCode();

            foreach (PixelChannel channel in _channels.Keys)
            {
                hashCode = hashCode ^ _channels[channel].GetHashCode();
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
            IntPtr instance = NativeStatistics.GetInstance(list, channel);

            ChannelStatistics result = ChannelStatistics.Create(channel, instance);
            if (result != null)
                _channels.Add(result.Channel, result);
        }
    }
}