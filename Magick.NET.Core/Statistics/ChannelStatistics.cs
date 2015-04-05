//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageChannelStatistics object.
	///</summary>
	public sealed class ChannelStatistics : IEquatable<ChannelStatistics>
	{
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the ChannelStatistics class.
		/// </summary>
		/// <param name="channel">The channel.</param>
		/// <param name="depth">Depth of the channel.</param>
		/// <param name="entropy">Entropy.</param>
		/// <param name="kurtosis">Kurtosis</param>
		/// <param name="maximum">Maximum value observed.</param>
		/// <param name="mean">Average (mean) value observed.</param>
		/// <param name="minimum">Minimum value observed.</param>
		/// <param name="skewness">Skewness.</param>
		/// <param name="standardDeviation">Standard deviation, sqrt(variance).</param>
		/// <param name="sum">Sum.</param>
		/// <param name="sumCubed">Sum cubed.</param>
		/// <param name="sumFourthPower">Sum fourth power.</param>
		/// <param name="sumSquared">Sum squared.</param>
		/// <param name="variance">Variance.</param>
		//===========================================================================================
		public ChannelStatistics(PixelChannel channel, int depth, double entropy, double kurtosis,
			double maximum, double mean, double minimum, double skewness, double standardDeviation,
			double sum, double sumCubed, double sumFourthPower, double sumSquared, double variance)
		{
			Channel = channel;
			Depth = depth;
			Entropy = entropy;
			Kurtosis = kurtosis;
			Maximum = maximum;
			Mean = mean;
			Minimum = minimum;
			Skewness = skewness;
			StandardDeviation = standardDeviation;
			Sum = sum;
			SumCubed = sumCubed;
			SumFourthPower = sumFourthPower;
			SumSquared = sumSquared;
			Variance = variance;
		}
		///==========================================================================================
		///<summary>
		/// The channel.
		///</summary>
		public PixelChannel Channel
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Depth of the channel.
		///</summary>
		public int Depth
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Entropy.
		///</summary>
		public double Entropy
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Kurtosis.
		///</summary>
		public double Kurtosis
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Maximum value observed.
		///</summary>
		public double Maximum
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Average (mean) value observed.
		///</summary>
		public double Mean
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Minimum value observed.
		///</summary>
		public double Minimum
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Skewness.
		///</summary>
		public double Skewness
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Standard deviation, sqrt(variance).
		///</summary>
		public double StandardDeviation
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Sum.
		///</summary>
		public double Sum
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Sum cubed.
		///</summary>
		public double SumCubed
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Sum fourth power.
		///</summary>
		public double SumFourthPower
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Sum squared.
		///</summary>
		public double SumSquared
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// Variance.
		///</summary>
		public double Variance
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified ChannelStatistics instances are considered equal.
		/// </summary>
		/// <param name="left">The first ChannelStatistics to compare.</param>
		/// <param name="right"> The second ChannelStatistics to compare.</param>
		/// <returns></returns>
		public static bool operator ==(ChannelStatistics left, ChannelStatistics right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified ChannelStatistics instances are not considered equal.
		/// </summary>
		/// <param name="left">The first ChannelStatistics to compare.</param>
		/// <param name="right"> The second ChannelStatistics to compare.</param>
		/// <returns></returns>
		public static bool operator !=(ChannelStatistics left, ChannelStatistics right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current channel statistics.
		///</summary>
		///<param name="obj">The object to compare this channel statistics with.</param>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return Equals(obj as ChannelStatistics);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified channel statistics is equal to the current channel statistics.
		///</summary>
		///<param name="other">The channel statistics to compare this channel statistics with.</param>
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
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
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
		//===========================================================================================
	}
	//==============================================================================================
}