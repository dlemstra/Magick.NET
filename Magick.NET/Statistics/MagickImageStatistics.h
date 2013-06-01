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

#include "ChannelStatistics.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageStatistics object.
	///</summary>
	public value struct MagickImageStatistics sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ChannelStatistics _Alpha;
		ChannelStatistics _Blue;
		ChannelStatistics _Green;
		ChannelStatistics _Red;
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImageStatistics(Magick::Image::ImageStatistics statistics);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Statistics for the alpha channel.
		///</summary>
		property ChannelStatistics Alpha
		{
			ChannelStatistics get();
		}
		///==========================================================================================
		///<summary>
		/// Statistics for the blue channel.
		///</summary>
		property ChannelStatistics Blue
		{
			ChannelStatistics get();
		}
		///==========================================================================================
		///<summary>
		/// Statistics for the green channel.
		///</summary>
		property ChannelStatistics Green
		{
			ChannelStatistics get();
		}
		///==========================================================================================
		///<summary>
		/// Statistics for the red channel.
		///</summary>
		property ChannelStatistics Red
		{
			ChannelStatistics get();
		}
		//===========================================================================================
		static bool operator == (MagickImageStatistics left, MagickImageStatistics right)
		{
			return Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator != (MagickImageStatistics left, MagickImageStatistics right)
		{
			return !Object::Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image statistics.
		///</summary>
		///<param name="obj">The object to compare this image statistics with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified image statistics is equal to the current image statistics.
		///</summary>
		///<param name="statistics">The image statistics to compare this image statistics with.</param>
		bool Equals(MagickImageStatistics statistics);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}