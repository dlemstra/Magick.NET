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
#include "Stdafx.h"
#include "Moments.h"

namespace ImageMagick
{
	//==============================================================================================
	Moments::Moments(const Magick::ImageMoments* moments)
	{
		_Channels = gcnew Dictionary<PixelChannel, ChannelMoments^>();
		Array^ values = Enum::GetValues(PixelChannel::typeid);
		for (int i = 0; i < values->Length; i++)
		{
			PixelChannel channel = (PixelChannel)values->GetValue(i);
			if (_Channels->ContainsKey(channel))
				continue;

			Magick::ChannelMoments magickMoments = moments->channel((Magick::PixelChannel)channel);
			if (!magickMoments.isValid())
				continue;

			_Channels->Add(channel,gcnew ChannelMoments(magickMoments));
		}
	}
	//==============================================================================================
	ChannelMoments^ Moments::Composite()
	{
		return GetChannel(PixelChannel::Composite);
	}
	//==============================================================================================
	ChannelMoments^ Moments::GetChannel(PixelChannel channel)
	{
		ChannelMoments^ moments;
		_Channels->TryGetValue(channel, moments);
		return moments;
	}
	//==============================================================================================
}