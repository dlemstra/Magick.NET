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
	ChannelStatistics::ChannelStatistics(const Magick::Image::ImageChannelStatistics channelStatistics)
	{
		//_Depth = channelStatistics.depth();
		_Kurtosis = channelStatistics.kurtosis;
		_Maximum = channelStatistics.maximum;
		_Mean = channelStatistics.mean;
		_Minimum = channelStatistics.minimum;
		_Skewness = channelStatistics.skewness;
		_StandardDeviation = channelStatistics.standard_deviation;
		//_Sum = channelStatistics.sum();
		//_SumCubed = channelStatistics.sumCubed();
		//_SumFourthPower = channelStatistics.sumFourthPower();
		//_SumSquared = channelStatistics.sumSquared();
		_Variance = channelStatistics.variance;
	}
	//==============================================================================================
	//int ChannelStatistics::Depth::get()
	//{
	//	return _Depth;
	//}
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
	//double ChannelStatistics::Sum::get()
	//{
	//	return _Sum;
	//}
	//==============================================================================================
	//double ChannelStatistics::SumCubed::get()
	//{
	//	return _SumCubed;
	//}
	//==============================================================================================
	//double ChannelStatistics::SumFourthPower::get()
	//{
	//	return _SumFourthPower;
	//}
	//==============================================================================================
	//double ChannelStatistics::SumSquared::get()
	//{
	//	return _SumSquared;
	//}
	//==============================================================================================
	double ChannelStatistics::Variance::get()
	{
		return _Variance;
	}
	//==============================================================================================
	bool ChannelStatistics::operator == (ChannelStatistics^ left, ChannelStatistics^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool ChannelStatistics::operator != (ChannelStatistics^ left, ChannelStatistics^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool ChannelStatistics::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		return Equals(dynamic_cast<ChannelStatistics^>(obj));
	}
	//==============================================================================================
	bool ChannelStatistics::Equals(ChannelStatistics^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return
			//_Depth.Equals(other->Depth) &&
			_Kurtosis.Equals(other->_Kurtosis) &&
			_Maximum.Equals(other->_Maximum) &&
			_Mean.Equals(other->_Mean) &&
			_Minimum.Equals(other->_Minimum) &&
			_Skewness.Equals(other->_Skewness) &&
			_StandardDeviation.Equals(other->_StandardDeviation) &&
			//_Sum.Equals(other->_Sum) &&
			//_SumCubed.Equals(other->_SumCubed) &&
			//_SumFourthPower.Equals(other->_SumFourthPower) &&
			//_SumSquared.Equals(other->_SumSquared) &&
			_Variance.Equals(other->_Variance);
	}
	//==============================================================================================
	int ChannelStatistics::GetHashCode()
	{
		return
			/*_Depth.GetHashCode() ^*/ _Maximum.GetHashCode() ^ _Mean.GetHashCode() ^ _Minimum.GetHashCode() ^
			_Kurtosis.GetHashCode() ^ _Skewness.GetHashCode() ^ _StandardDeviation.GetHashCode() ^
			/*_Sum.GetHashCode() ^ _SumCubed.GetHashCode() ^ _SumFourthPower.GetHashCode() ^
			_SumSquared.GetHashCode() ^*/ _Variance.GetHashCode();
	}
	//==============================================================================================
}