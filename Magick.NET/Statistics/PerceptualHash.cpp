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
#include "..\Exceptions\Base\MagickException.h"
#include "PerceptualHash.h"

namespace ImageMagick
{
	//==============================================================================================
	void PerceptualHash::Initialize(const Magick::ImagePerceptualHash* perceptualHash)
	{
		_Channels = gcnew Dictionary<PixelChannel, ChannelPerceptualHash^>();

		_Channels->Add(PixelChannel::Red, gcnew ChannelPerceptualHash(perceptualHash->channel(Magick::RedPixelChannel)));
		_Channels->Add(PixelChannel::Green, gcnew ChannelPerceptualHash(perceptualHash->channel(Magick::GreenPixelChannel)));
		_Channels->Add(PixelChannel::Blue, gcnew ChannelPerceptualHash(perceptualHash->channel(Magick::BluePixelChannel)));
	}
	//==============================================================================================
	PerceptualHash::PerceptualHash(const Magick::ImagePerceptualHash* perceptualHash)
	{
		Initialize(perceptualHash);
	}
	//==============================================================================================
	PerceptualHash::PerceptualHash(String^ perceptualHash)
	{
		Throw::IfNullOrEmpty("perceptualHash", perceptualHash);
		Throw::IfFalse("perceptualHash", perceptualHash->Length == 210, "Invalid hash size.");

		std::string hash;
		Marshaller::Marshal(perceptualHash,hash);
		try
		{
			Magick::ImagePerceptualHash magickPerceptualHash(hash);
			Throw::IfFalse("perceptualHash", magickPerceptualHash.isValid(), "Invalid hash.");
			Initialize(&magickPerceptualHash);
		}
		catch(Magick::Exception& exception)
		{
			throw gcnew ArgumentException("Invalid hash.", "perceptualHash", MagickException::Create(exception));
		}
	}
	//==============================================================================================
	ChannelPerceptualHash^ PerceptualHash::GetChannel(PixelChannel channel)
	{
		ChannelPerceptualHash^ perceptualHash;
		_Channels->TryGetValue(channel, perceptualHash);
		return perceptualHash;
	}
	//==============================================================================================
	double PerceptualHash::SumSquaredDistance(PerceptualHash^ other)
	{
		Throw::IfNull("other", other);

		return
			_Channels[PixelChannel::Red]->SumSquaredDistance(other->_Channels[PixelChannel::Red]) +
			_Channels[PixelChannel::Green]->SumSquaredDistance(other->_Channels[PixelChannel::Green]) +
			_Channels[PixelChannel::Blue]->SumSquaredDistance(other->_Channels[PixelChannel::Blue]);
	}
	//==============================================================================================
	String^ PerceptualHash::ToString()
	{
		return
			_Channels[PixelChannel::Red]->ToString() +
			_Channels[PixelChannel::Green]->ToString() +
			_Channels[PixelChannel::Blue]->ToString();
	}
	//==============================================================================================
}