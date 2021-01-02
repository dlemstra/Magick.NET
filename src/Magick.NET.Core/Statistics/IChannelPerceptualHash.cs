// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Contains the he perceptual hash of one image channel.
    /// </summary>
    public interface IChannelPerceptualHash
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        PixelChannel Channel { get; }

        /// <summary>
        /// SRGB hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The SRGB hu perceptual hash.</returns>
        double SrgbHuPhash(int index);

        /// <summary>
        /// Hclp hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The Hclp hu perceptual hash.</returns>
        double HclpHuPhash(int index);

        /// <summary>
        /// Returns the sum squared difference between this hash and the other hash.
        /// </summary>
        /// <param name="other">The <see cref="IChannelPerceptualHash"/> to get the distance of.</param>
        /// <returns>The sum squared difference between this hash and the other hash.</returns>
        double SumSquaredDistance(IChannelPerceptualHash other);

        /// <summary>
        /// Returns a string representation of this hash.
        /// </summary>
        /// <returns>A string representation of this hash.</returns>
        string ToString();
    }
}
