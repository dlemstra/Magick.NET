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
#pragma once

#include "..\Enums\PixelChannel.h"
#include "ChannelPerceptualHash.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImagePerceptualHash object.
	///</summary>
	public ref class PerceptualHash sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		Dictionary<PixelChannel, ChannelPerceptualHash^>^ _Channels;
		//===========================================================================================
		void Initialize(const Magick::ImagePerceptualHash* perceptualHash);
		//===========================================================================================
	internal:
		//===========================================================================================
		PerceptualHash(const Magick::ImagePerceptualHash* perceptualHash);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PerceptualHash class with the specified hash.
		///</summary>
		PerceptualHash(String^ perceptualHash);
		///==========================================================================================
		///<summary>
		/// Perceptual hash for the specified channel.
		///</summary>
		ChannelPerceptualHash^ GetChannel(PixelChannel channel);
		///==========================================================================================
		///<summary>
		/// Returns the sum squared difference between this hash and the other hash.
		///</summary>
		///<param name="other">The PerceptualHash to get the distance of.</param>
		double SumSquaredDistance(PerceptualHash^ other);
		///==========================================================================================
		///<summary>
		/// Returns a string representation of this hash.
		///</summary>
		virtual String^ ToString() override;
		//===========================================================================================
	};
	//==============================================================================================
}