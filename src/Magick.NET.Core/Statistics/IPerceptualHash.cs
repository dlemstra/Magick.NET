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
    /// Contains the he perceptual hash of one or more image channels.
    /// </summary>
    public interface IPerceptualHash
    {
        /// <summary>
        /// Returns the perceptual hash for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the has for.</param>
        /// <returns>The perceptual hash for the specified channel.</returns>
        IChannelPerceptualHash GetChannel(PixelChannel channel);

        /// <summary>
        /// Returns the sum squared difference between this hash and the other hash.
        /// </summary>
        /// <param name="other">The <see cref="IPerceptualHash"/> to get the distance of.</param>
        /// <returns>The sum squared difference between this hash and the other hash.</returns>
        double SumSquaredDistance(IPerceptualHash other);

        /// <summary>
        /// Returns a string representation of this hash.
        /// </summary>
        /// <returns>A <see cref="string"/>.</returns>
        string ToString();
    }
}