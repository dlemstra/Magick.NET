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

#include "..\Enums\PixelChannel.h"
#include "ChannelStatistics.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageStatistics object.
	///</summary>
	public ref class MagickImageStatistics : IEquatable<MagickImageStatistics^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		Dictionary<PixelChannel, ChannelStatistics^>^ _Channels;
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImageStatistics(const Magick::Image::ImageStatistics* statistics);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Statistics for the all the channels.
		///</summary>
		//ChannelStatistics^ Composite();
		///==========================================================================================
		///<summary>
		/// Statistics for the specified channel.
		///</summary>
		ChannelStatistics^ GetChannel(PixelChannel channel);
		//===========================================================================================
		static bool operator == (MagickImageStatistics^ left, MagickImageStatistics^ right);
		//===========================================================================================
		static bool operator != (MagickImageStatistics^ left, MagickImageStatistics^ right);
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
		///<param name="other">The image statistics to compare this image statistics with.</param>
		virtual bool Equals(MagickImageStatistics^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}