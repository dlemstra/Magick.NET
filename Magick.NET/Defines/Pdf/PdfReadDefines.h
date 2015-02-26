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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when a pdf image is read.
	///</summary>
	public ref class PdfReadDefines sealed : DefineCreator, IReadDefines
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PdfReadDefines class.
		///</summary>
		PdfReadDefines() : DefineCreator(MagickFormat::Pdf) {};
		///==========================================================================================
		///<summary>
		/// Scale the image the specified size
		///</summary>
		property MagickGeometry^ FitPage;
		///==========================================================================================
		///<summary>
		/// Force use of the crop box.
		///</summary>
		property Nullable<bool> UseCropBox;
		///==========================================================================================
		///<summary>
		/// Force use of the trim box.
		///</summary>
		property Nullable<bool> UseTrimBox;
		//===========================================================================================
	};
	//==============================================================================================
}