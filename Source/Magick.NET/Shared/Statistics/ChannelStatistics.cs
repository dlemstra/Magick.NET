// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public sealed partial class ChannelStatistics : IEquatable<ChannelStatistics>
    {
        private ChannelStatistics(PixelChannel channel, IntPtr instance)
        {
            Channel = channel;

            NativeChannelStatistics nativeInstance = new NativeChannelStatistics(instance);
            Depth = nativeInstance.Depth;
            Entropy = nativeInstance.Entropy;
            Kurtosis = nativeInstance.Kurtosis;
            Maximum = nativeInstance.Maximum;
            Mean = nativeInstance.Mean;
            Minimum = nativeInstance.Minimum;
            Skewness = nativeInstance.Skewness;
            StandardDeviation = nativeInstance.StandardDeviation;
            Sum = nativeInstance.Sum;
            SumCubed = nativeInstance.SumCubed;
            SumFourthPower = nativeInstance.SumFourthPower;
            SumSquared = nativeInstance.SumSquared;
            Variance = nativeInstance.Variance;
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        public PixelChannel Channel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the depth of the channel.
        /// </summary>
        public int Depth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entropy.
        /// </summary>
        public double Entropy
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the kurtosis.
        /// </summary>
        public double Kurtosis
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the maximum value observed.
        /// </summary>
        public double Maximum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the average (mean) value observed.
        /// </summary>
        public double Mean
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the minimum value observed.
        /// </summary>
        public double Minimum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the skewness.
        /// </summary>
        public double Skewness
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the standard deviation, sqrt(variance).
        /// </summary>
        public double StandardDeviation
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sum.
        /// </summary>
        public double Sum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sum cubed.
        /// </summary>
        public double SumCubed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sum fourth power.
        /// </summary>
        public double SumFourthPower
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sum squared.
        /// </summary>
        public double SumSquared
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the variance.
        /// </summary>
        public double Variance
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ChannelStatistics"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ChannelStatistics"/> to compare.</param>
        /// <param name="right"> The second <see cref="ChannelStatistics"/> to compare.</param>
        public static bool operator ==(ChannelStatistics left, ChannelStatistics right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ChannelStatistics"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ChannelStatistics"/> to compare.</param>
        /// <param name="right"> The second <see cref="ChannelStatistics"/> to compare.</param>
        public static bool operator !=(ChannelStatistics left, ChannelStatistics right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ChannelStatistics"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ChannelStatistics"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ChannelStatistics"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as ChannelStatistics);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ChannelStatistics"/> is equal to the current <see cref="ChannelStatistics"/>.
        /// </summary>
        /// <param name="other">The channel statistics to compare this <see cref="ChannelStatistics"/> with.</param>
        /// <returns>True when the specified <see cref="ChannelStatistics"/> is equal to the current <see cref="ChannelStatistics"/>.</returns>
        public bool Equals(ChannelStatistics other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              Depth.Equals(other.Depth) &&
              Entropy.Equals(other.Entropy) &&
              Kurtosis.Equals(other.Kurtosis) &&
              Maximum.Equals(other.Maximum) &&
              Mean.Equals(other.Mean) &&
              Minimum.Equals(other.Minimum) &&
              Skewness.Equals(other.Skewness) &&
              StandardDeviation.Equals(other.StandardDeviation) &&
              Sum.Equals(other.Sum) &&
              SumCubed.Equals(other.SumCubed) &&
              SumFourthPower.Equals(other.SumFourthPower) &&
              SumSquared.Equals(other.SumSquared) &&
              Variance.Equals(other.Variance);
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              Depth.GetHashCode() ^
              Entropy.GetHashCode() ^
              Kurtosis.GetHashCode() ^
              Maximum.GetHashCode() ^
              Mean.GetHashCode() ^
              Minimum.GetHashCode() ^
              Skewness.GetHashCode() ^
              StandardDeviation.GetHashCode() ^
              Sum.GetHashCode() ^
              SumCubed.GetHashCode() ^
              SumFourthPower.GetHashCode() ^
              SumSquared.GetHashCode() ^
              Variance.GetHashCode();
        }

        internal static ChannelStatistics Create(PixelChannel channel, IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            return new ChannelStatistics(channel, instance);
        }
    }
}