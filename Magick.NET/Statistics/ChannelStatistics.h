//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
#pragma once

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageChannelStatistics object.
	///</summary>
	public value struct ChannelStatistics sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _Maximum;
		double _Minimum;
		double _Mean;
		double _Kurtosis;
		double _Skewness;
		double _StandardDeviation;
		double _Variance;
		//===========================================================================================
	internal:
		//===========================================================================================
		ChannelStatistics(Magick::Image::ImageChannelStatistics channelStatistics);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Maximum value observed.
		///</summary>
		property double Maximum
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Minimum value observed.
		///</summary>
		property double Minimum
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Average (mean) value observed.
		///</summary>
		property double Mean
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Kurtosis.
		///</summary>
		property double Kurtosis
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Skewness.
		///</summary>
		property double Skewness
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Standard deviation, sqrt(variance).
		///</summary>
		property double StandardDeviation
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// Variance.
		///</summary>
		property double Variance
		{
			double get();
		}
		//===========================================================================================
		static bool operator == (ChannelStatistics left, ChannelStatistics right);
		//===========================================================================================
		static bool operator != (ChannelStatistics left, ChannelStatistics right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current channel statistics.
		///</summary>
		///<param name="obj">The object to compare this channel statistics with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified channel statistics is equal to the current channel statistics.
		///</summary>
		///<param name="channelStatistics">The channel statistics to compare this channel statistics with.</param>
		bool Equals(ChannelStatistics channelStatistics);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}