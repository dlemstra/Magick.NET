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
#include "ChannelStatistics.h"

namespace ImageMagick
{
	//==============================================================================================
	ChannelStatistics::ChannelStatistics(Magick::Image::ImageChannelStatistics channelStatistics)
	{
		_Maximum = channelStatistics.maximum;
		_Minimum = channelStatistics.minimum;
		_Mean = channelStatistics.mean;
		_Kurtosis = channelStatistics.kurtosis;
		_Skewness = channelStatistics.skewness;
		_StandardDeviation = channelStatistics.standard_deviation;
		_Variance = channelStatistics.variance;
	}
	//==============================================================================================
	double ChannelStatistics::Maximum::get()
	{
		return _Maximum;
	}
	//==============================================================================================
	double ChannelStatistics::Minimum::get()
	{
		return _Minimum;
	}
	//==============================================================================================
	double ChannelStatistics::Mean::get()
	{
		return _Mean;
	}
	//==============================================================================================
	double ChannelStatistics::Kurtosis::get()
	{
		return _Kurtosis;
	}
	//==============================================================================================
	double ChannelStatistics::Skewness::get()
	{
		return _Skewness;
	}
	//==============================================================================================
	double ChannelStatistics::StandardDeviation::get()
	{
		return _StandardDeviation;
	}
	//==============================================================================================
	double ChannelStatistics::Variance::get()
	{
		return _Variance;
	}
	//==============================================================================================
	bool ChannelStatistics::operator == (ChannelStatistics left, ChannelStatistics right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool ChannelStatistics::operator != (ChannelStatistics left, ChannelStatistics right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool ChannelStatistics::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		if (obj->GetType() == ChannelStatistics::typeid)
			return Equals((ChannelStatistics)obj);

		return false;
	}
	//==============================================================================================
	bool ChannelStatistics::Equals(ChannelStatistics channelStatistics)
	{
		return
			_Maximum.Equals(channelStatistics._Maximum) &&
			_Minimum.Equals(channelStatistics._Minimum) &&
			_Mean.Equals(channelStatistics._Mean) &&
			_Kurtosis.Equals(channelStatistics._Kurtosis) &&
			_Skewness.Equals(channelStatistics._Skewness) &&
			_StandardDeviation.Equals(channelStatistics._StandardDeviation) &&
			_Variance.Equals(channelStatistics._Variance);
	}
	//==============================================================================================
	int ChannelStatistics::GetHashCode()
	{
		return
			_Maximum.GetHashCode() ^ _Minimum.GetHashCode() ^ _Mean.GetHashCode() ^
			_Kurtosis.GetHashCode() ^ _Skewness.GetHashCode() ^ _StandardDeviation.GetHashCode() ^
			_Variance.GetHashCode();
	}
	//==============================================================================================
}