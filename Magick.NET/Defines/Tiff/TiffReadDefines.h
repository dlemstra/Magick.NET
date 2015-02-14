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

#include "..\Base\DefineCreator.h"
#include "..\Base\IReadDefines.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when a tiff image is read.
	///</summary>
	public ref class TiffReadDefines sealed : DefineCreator, IReadDefines
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the TiffReadDefines class.
		///</summary>
		TiffReadDefines() : DefineCreator(MagickFormat::Tiff) {};
		///==========================================================================================
		///<summary>
		/// Specifies if the exif profile should be ignored (tiff:exif-properties).
		///</summary>
		property Nullable<bool> IgnoreExifPoperties;
		//===========================================================================================
	};
	//==============================================================================================
}