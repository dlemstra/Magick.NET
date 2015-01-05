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
#include "ChannelMoments.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageMoments object.
	///</summary>
	public ref class Moments sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		Dictionary<PixelChannel, ChannelMoments^>^ _Channels;
		//===========================================================================================
	internal:
		//===========================================================================================
		Moments(const Magick::ImageMoments* moments);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Moments for the all the channels.
		///</summary>
		ChannelMoments^ Composite();
		///==========================================================================================
		///<summary>
		/// Moments for the specified channel.
		///</summary>
		ChannelMoments^ GetChannel(PixelChannel channel);
	};
	//==============================================================================================
}