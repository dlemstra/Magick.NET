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
#pragma once

#include "..\Arguments\PointD.h"
#include "..\Enums\PixelChannel.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ChannelMoments object.
	///</summary>
	public ref class ChannelMoments sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		PixelChannel _Channel;
		PointD _Centroid;
		PointD _EllipseAxis;
		double _EllipseAngle;
		double _EllipseEccentricity;
		double _EllipseIntensity;
		array<double>^ _HuInvariants;
		//===========================================================================================
	internal:
		//===========================================================================================
		ChannelMoments(const Magick::ChannelMoments channelMoments);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The centroid.
		///</summary>
		property PointD Centroid
		{
			PointD get();
		}
		///==========================================================================================
		///<summary>
		/// The channel of this moment.
		///</summary>
		property PixelChannel Channel
		{
			PixelChannel get();
		}
		///==========================================================================================
		///<summary>
		/// The ellipse axis.
		///</summary>
		property PointD EllipseAxis
		{
			PointD get();
		}
		///==========================================================================================
		///<summary>
		/// The ellipse angle.
		///</summary>
		property double EllipseAngle
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// The ellipse eccentricity.
		///</summary>
		property double EllipseEccentricity
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// The ellipse intensity.
		///</summary>
		property double EllipseIntensity
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// The Hu invariants.
		///</summary>
		double HuInvariants(const int index);
		//===========================================================================================
	};
	//==============================================================================================
}