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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageChannelStatistics object.
    /// </summary>
    public interface IChannelStatistics : IEquatable<IChannelStatistics>
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        PixelChannel Channel { get; }

        /// <summary>
        /// Gets the depth of the channel.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets the entropy.
        /// </summary>
        double Entropy { get; }

        /// <summary>
        /// Gets the kurtosis.
        /// </summary>
        double Kurtosis { get; }

        /// <summary>
        /// Gets the maximum value observed.
        /// </summary>
        double Maximum { get; }

        /// <summary>
        /// Gets the average (mean) value observed.
        /// </summary>
        double Mean { get; }

        /// <summary>
        /// Gets the minimum value observed.
        /// </summary>
        double Minimum { get; }

        /// <summary>
        /// Gets the skewness.
        /// </summary>
        double Skewness { get; }

        /// <summary>
        /// Gets the standard deviation, sqrt(variance).
        /// </summary>
        double StandardDeviation { get; }

        /// <summary>
        /// Gets the sum.
        /// </summary>
        double Sum { get; }

        /// <summary>
        /// Gets the sum cubed.
        /// </summary>
        double SumCubed { get; }

        /// <summary>
        /// Gets the sum fourth power.
        /// </summary>
        double SumFourthPower { get; }

        /// <summary>
        /// Gets the sum squared.
        /// </summary>
        double SumSquared { get; }

        /// <summary>
        /// Gets the variance.
        /// </summary>
        double Variance { get; }
    }
}
