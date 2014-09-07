//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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

#include "..\Enums\PixelChannel.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ChannelPerceptualHash object.
	///</summary>
	public ref class ChannelPerceptualHash sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		PixelChannel _Channel;
		array<double>^ _SrgbHuPhash;
		array<double>^ _HclpHuPhash;
		String^ _Hash;
		//===========================================================================================
	internal:
		//===========================================================================================
		ChannelPerceptualHash(const Magick::ChannelPerceptualHash channelPerceptualHash);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The channel.
		///</summary>
		property PixelChannel Channel
		{
			PixelChannel get();
		}
		///==========================================================================================
		///<summary>
		/// SRGB hu perceptual hash.
		///</summary>
		double SrgbHuPhash(const int index);
		///==========================================================================================
		///<summary>
		/// Hclp hu perceptual hash.
		///</summary>
		double HclpHuPhash(const int index);
		///==========================================================================================
		///<summary>
		/// Returns the sum squared difference between this hash and the other hash.
		///</summary>
		///<param name="other">The ChannelPerceptualHash to get the distance of.</param>
		double SumSquaredDistance(ChannelPerceptualHash^ other);
		///==========================================================================================
		///<summary>
		/// Returns a string representation of this hash.
		///</summary>
		virtual String^ ToString() override;
		//===========================================================================================
	};
	//==============================================================================================
}