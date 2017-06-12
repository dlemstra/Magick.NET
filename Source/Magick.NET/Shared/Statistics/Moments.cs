//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// The normalized moments of one or more image channels.
    /// </summary>
    public sealed partial class Moments
    {
        private Dictionary<PixelChannel, ChannelMoments> _Channels;

        private void AddChannel(IntPtr list, PixelChannel channel)
        {
            IntPtr instance = NativeMoments.GetInstance(list, channel);

            ChannelMoments result = ChannelMoments.Create(channel, instance);
            if (result != null)
                _Channels.Add(result.Channel, result);
        }

        internal Moments(MagickImage image, IntPtr list)
        {
            if (list == IntPtr.Zero)
                return;

            _Channels = new Dictionary<PixelChannel, ChannelMoments>();
            foreach (PixelChannel channel in image.Channels)
                AddChannel(list, channel);
        }

        internal static void DisposeList(IntPtr list)
        {
            if (list != IntPtr.Zero)
                NativeMoments.DisposeList(list);
        }

        /// <summary>
        /// Gets the moments for the all the channels.
        /// </summary>
        /// <returns>The moments for the all the channels.</returns>
        public ChannelMoments Composite()
        {
            return GetChannel(PixelChannel.Composite);
        }

        /// <summary>
        /// Gets the moments for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the moments for.</param>
        /// <returns>The moments for the specified channel.</returns>
        public ChannelMoments GetChannel(PixelChannel channel)
        {
            ChannelMoments moments;
            _Channels.TryGetValue(channel, out moments);
            return moments;
        }
    }
}