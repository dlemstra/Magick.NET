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
#include "Stdafx.h"
#include "MagickImageStatistics.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickImageStatistics::MagickImageStatistics(Magick::Image::ImageStatistics statistics)
	{
		_Alpha = ChannelStatistics(statistics.opacity);
		_Blue = ChannelStatistics(statistics.blue);
		_Green = ChannelStatistics(statistics.green);
		_Red = ChannelStatistics(statistics.red);
	}
	//==============================================================================================
	ChannelStatistics MagickImageStatistics::Alpha::get()
	{
		return _Alpha;
	}
	//==============================================================================================
	ChannelStatistics MagickImageStatistics::Blue::get()
	{
		return _Blue;
	}
	//==============================================================================================
	ChannelStatistics MagickImageStatistics::Green::get()
	{
		return _Green;
	}
	//==============================================================================================
	ChannelStatistics MagickImageStatistics::Red::get()
	{
		return _Red;
	}
	//==============================================================================================
	bool MagickImageStatistics::operator == (MagickImageStatistics left, MagickImageStatistics right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickImageStatistics::operator != (MagickImageStatistics left, MagickImageStatistics right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickImageStatistics::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		if (obj->GetType() == MagickImageStatistics::typeid)
			return Equals((MagickImageStatistics)obj);

		return false;
	}
	//==============================================================================================
	bool MagickImageStatistics::Equals(MagickImageStatistics statistics)
	{
		return
			_Alpha.Equals(statistics._Alpha) &&
			_Blue.Equals(statistics._Blue) &&
			_Green.Equals(statistics._Green) &&
			_Red.Equals(statistics._Red);
	}
	//==============================================================================================
	int MagickImageStatistics::GetHashCode()
	{
		return _Alpha.GetHashCode() ^ _Blue.GetHashCode() ^ _Green.GetHashCode() ^_Red.GetHashCode();
	}
	//==============================================================================================
}