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
#include "Statistics.h"

namespace ImageMagick
{
	//==============================================================================================
	Statistics::Statistics(const Magick::ImageStatistics* statistics)
	{
		_Channels = gcnew Dictionary<PixelChannel, ChannelStatistics^>();
		for each (PixelChannel channel in Enum::GetValues(PixelChannel::typeid))
		{
			if (_Channels->ContainsKey(channel))
				continue;

			Magick::ChannelStatistics channelStatistics = statistics->channel((Magick::PixelChannel)channel);
			if (!channelStatistics.isValid())
				continue;

			_Channels->Add(channel, gcnew ChannelStatistics(channelStatistics));
		}
	}
	//==============================================================================================
	ChannelStatistics^ Statistics::Composite()
	{
		return _Channels[PixelChannel::Composite];
	}
	//==============================================================================================
	ChannelStatistics^ Statistics::GetChannel(PixelChannel channel)
	{
		if (!_Channels->ContainsKey(channel))
			return nullptr;

		return _Channels[channel];
	}
	//==============================================================================================
	bool Statistics::operator == (Statistics^ left, Statistics^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool Statistics::operator != (Statistics^ left, Statistics^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool Statistics::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		return Equals(dynamic_cast<Statistics^>(obj));
	}
	//==============================================================================================
	bool Statistics::Equals(Statistics^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		if (_Channels->Count != other->_Channels->Count)
			return false;

		for each(PixelChannel channel in _Channels->Keys)
		{
			if (!other->_Channels->ContainsKey(channel))
				return false;

			if (! _Channels[channel]->Equals( other->_Channels[channel]))
				return false;
		}

		return true;
	}
	//==============================================================================================
	int Statistics::GetHashCode()
	{
		int hashCode = _Channels->GetHashCode();

		for each(PixelChannel channel in _Channels->Keys)
		{
			hashCode = hashCode ^ _Channels[channel]->GetHashCode();
		}

		return hashCode;
	}
	//==============================================================================================
}