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

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageStatistics object.
    /// </summary>
    public interface IStatistics : IEquatable<IStatistics>
    {
        /// <summary>
        /// Gets the channels.
        /// </summary>
        IEnumerable<PixelChannel> Channels { get; }

        /// <summary>
        /// Returns the statistics for the all the channels.
        /// </summary>
        /// <returns>The statistics for the all the channels.</returns>
        IChannelStatistics Composite();

        /// <summary>
        /// Returns the statistics for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the statistics for.</param>
        /// <returns>The statistics for the specified channel.</returns>
        IChannelStatistics GetChannel(PixelChannel channel);
    }
}
