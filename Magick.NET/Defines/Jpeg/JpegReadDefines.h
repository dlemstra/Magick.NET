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
#pragma once

#include "..\..\Arguments\MagickGeometry.h"
#include "..\Base\DefineCreator.h"
#include "..\Base\IReadDefines.h"
#include "..\Base\ProfileTypes.h"
#include "DctMethod.h"

using namespace ImageMagick::Defines;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when an jpeg image is read.
	///</summary>
	public ref class JpegReadDefines sealed : DefineCreator, IReadDefines
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the JpegReadDefines class.
		///</summary>
		JpegReadDefines() : DefineCreator(MagickFormat::Jpeg) {};
		///==========================================================================================
		///<summary>
		/// Enables or disables block smoothing (jpeg:block-smoothing).
		///</summary>
		property Nullable<bool> BlockSmoothing;
		///==========================================================================================
		///<summary>
		/// Specifies the size desired number of colors (jpeg:colors).
		///</summary>
		property Nullable<int> Colors;
		///==========================================================================================
		///<summary>
		/// Specifies the dtc method that will be used (jpeg:dct-method).
		///</summary>
		property Nullable<DctMethod> DctMethod;
		///==========================================================================================
		///<summary>
		/// Enables or disables fancy upsampling (jpeg:fancy-upsampling).
		///</summary>
		property Nullable<bool> FancyUpsampling;
		///==========================================================================================
		///<summary>
		/// Specifies the size the scale the image to (jpeg:size). The output image won't be exactly
		/// the specified size. More information can be found here: http://jpegclub.org/djpeg/.
		///</summary>
		property MagickGeometry^ Size;
		///==========================================================================================
		///<summary>
		/// Specifies the profile that should be skipped when the image is read (profile:skip).
		///</summary>
		property Nullable<ProfileTypes> SkipProfiles;
		//===========================================================================================
	};
	//==============================================================================================
}