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
#include "DctMethod.h"

using namespace System::Collections::Generic;
using namespace ImageMagick::Defines;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when a jpeg image is written.
	///</summary>
	public ref class JpegWriteDefines sealed : DefineCreator
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the JpegWriteDefines class.
		///</summary>
		JpegWriteDefines() : DefineCreator(MagickFormat::Jpeg) {};
		///==========================================================================================
		///<summary>
		/// Specifies the dtc method that will be used (jpeg:dct-method).
		///</summary>
		property Nullable<DctMethod> DctMethod;
		///==========================================================================================
		///<summary>
		/// Search for compression quality that does not exceed the specified extent in kilobytes. (jpeg:extent).
		///</summary>
		property Nullable<int> Extent;
		///==========================================================================================
		///<summary>
		/// Enables or disables optimize coding (jpeg:optimize-coding).
		///</summary>
		property Nullable<bool> OptimizeCoding;
		///==========================================================================================
		///<summary>
		/// Set quality scaling for luminance and chrominance separately (jpeg:quality).
		///</summary>
		property MagickGeometry^ Quality;
		///==========================================================================================
		///<summary>
		/// File name that contains custom quantization tables (jpeg:q-table).
		///</summary>
		property String^ QuantizationTables;
		///==========================================================================================
		///<summary>
		/// Set jpeg sampling factor (jpeg:sampling-factor).
		///</summary>
		property IEnumerable<MagickGeometry^>^ SamplingFactors;
		//===========================================================================================
	};
	//==============================================================================================
}