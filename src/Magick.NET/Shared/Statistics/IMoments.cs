// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// The normalized moments of one or more image channels.
    /// </summary>
    public interface IMoments
    {
        /// <summary>
        /// Gets the moments for the all the channels.
        /// </summary>
        /// <returns>The moments for the all the channels.</returns>
        IChannelMoments Composite();

        /// <summary>
        /// Gets the moments for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the moments for.</param>
        /// <returns>The moments for the specified channel.</returns>
        IChannelMoments GetChannel(PixelChannel channel);
    }
}