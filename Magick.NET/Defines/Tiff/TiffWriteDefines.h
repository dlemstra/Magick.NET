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
#include "..\..\Enums\Endian.h"
#include "..\Base\DefineCreator.h"
#include "TiffAlpha.h"

using namespace ImageMagick::Defines;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when a tiff image is written.
	///</summary>
	public ref class TiffWriteDefines sealed : DefineCreator
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the TiffWriteDefines class.
		///</summary>
		TiffWriteDefines() : DefineCreator(MagickFormat::Tiff) {};
		///==========================================================================================
		///<summary>
		/// Specifies the tiff alpha (tiff:alpha).
		///</summary>
		property Nullable<TiffAlpha> Alpha;
		///==========================================================================================
		///<summary>
		/// Specifies the endianness of the tiff file (tiff:endian).
		///</summary>
		property Nullable<ImageMagick::Endian> Endian;
		///==========================================================================================
		///<summary>
		/// Specifies the endianness of the tiff file (tiff:fill-order).
		///</summary>
		property Nullable<ImageMagick::Endian> FillOrder;
		///==========================================================================================
		///<summary>
		/// Specifies the rows per strip (tiff:rows-per-strip).
		///</summary>
		property Nullable<int> RowsPerStrip;
		///==========================================================================================
		///<summary>
		/// Specifies the tile geometry (tiff:tile-geometry).
		///</summary>
		property MagickGeometry^ TileGeometry;
		//===========================================================================================
	};
	//==============================================================================================
}