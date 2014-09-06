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
#include "ChannelMoments.h"

namespace ImageMagick
{
	//==============================================================================================
	ChannelMoments::ChannelMoments(const Magick::ChannelMoments channelMoments)
	{
		_Channel = (ImageMagick::PixelChannel)channelMoments.channel();
		_Centroid = PointD(channelMoments.centroidX(), channelMoments.centroidY());
		_EllipseAxis = PointD(channelMoments.ellipseAxisX(), channelMoments.ellipseAxisY());
		_EllipseAngle = channelMoments.ellipseAngle();
		_EllipseEccentricity = channelMoments.ellipseEccentricity();
		_EllipseIntensity = channelMoments.ellipseIntensity();
		_HuInvariants = gcnew array<double>(8);
		for (int i=0; i < 8; i++)
		{
			_HuInvariants[i] = channelMoments.huInvariants(i);
		}
	}
	//==============================================================================================
	PointD ChannelMoments::Centroid::get()
	{
		return _Centroid;
	}
	//==============================================================================================
	PixelChannel ChannelMoments::Channel::get()
	{
		return _Channel;
	}
	//==============================================================================================
	PointD ChannelMoments::EllipseAxis::get()
	{
		return _EllipseAxis;
	}
	//==============================================================================================
	double ChannelMoments::EllipseAngle::get()
	{
		return _EllipseAngle;
	}
	///=============================================================================================
	double ChannelMoments::EllipseEccentricity::get()
	{
		return _EllipseEccentricity;
	}
	//==============================================================================================
	double ChannelMoments::EllipseIntensity::get()
	{
		return _EllipseIntensity;
	}
	//==============================================================================================
	double ChannelMoments::HuInvariants(const int index)
	{
		Throw::IfOutOfRange("index", index, 8);

		return _HuInvariants[index];
	}
	//==============================================================================================
}