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
#include "Stdafx.h"
#include "ChannelPerceptualHash.h"

namespace ImageMagick
{
	//==============================================================================================
	ChannelPerceptualHash::ChannelPerceptualHash(const Magick::ChannelPerceptualHash channelPerceptualHash)
	{
		_Channel = (ImageMagick::PixelChannel)channelPerceptualHash.channel();
		_SrgbHuPhash = gcnew array<double>(7);
		_HclpHuPhash = gcnew array<double>(7);
		for (int i=0; i < 7; i++)
		{
			_SrgbHuPhash[i] = channelPerceptualHash.srgbHuPhash(i);
			_HclpHuPhash[i] = channelPerceptualHash.hclpHuPhash(i);
		}
		std::string hash=channelPerceptualHash;
		_Hash=Marshaller::Marshal(hash);
	}
	//==============================================================================================
	PixelChannel ChannelPerceptualHash::Channel::get()
	{
		return _Channel;
	}
	//==============================================================================================
	double ChannelPerceptualHash::SrgbHuPhash(const int index)
	{
		Throw::IfOutOfRange("index", index, 7);

		return _SrgbHuPhash[index];
	}
	//==============================================================================================
	double ChannelPerceptualHash::HclpHuPhash(const int index)
	{
		Throw::IfOutOfRange("index", index, 7);

		return _HclpHuPhash[index];
	}
	//==============================================================================================
	double ChannelPerceptualHash::SumSquaredDistance(ChannelPerceptualHash^ other)
	{
		Throw::IfNull("other", other);

		double ssd = 0.0;

		for (int i=0; i<7; i++)
		{
			ssd += ((_SrgbHuPhash[i] - other->_SrgbHuPhash[i]) * (_SrgbHuPhash[i] - other->_SrgbHuPhash[i]));
			ssd += ((_HclpHuPhash[i] - other->_HclpHuPhash[i]) * (_HclpHuPhash[i] - other->_HclpHuPhash[i]));
		}

		return ssd;
	}
	//==============================================================================================
	String^ ChannelPerceptualHash::ToString()
	{
		return _Hash;
	}
	//==============================================================================================
}