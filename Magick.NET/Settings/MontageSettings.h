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

#include "..\Colors\MagickColor.h"
#include "..\Enums\CompositeOperator.h"
#include "..\Enums\Gravity.h"
#include "..\MagickGeometry.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains setting for the montage operation.
	///</summary>
	public ref class MontageSettings sealed
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		void Apply(Magick::MontageFramed *settings);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickReadSettings class.
		///</summary>
		MontageSettings();
		///==========================================================================================
		///<summary>
		/// Color that thumbnails are composed on
		///</summary>
		property MagickColor^ BackgroundColor;
		///==========================================================================================
		///<summary>
		/// Frame border color
		///</summary>
		property MagickColor^ BorderColor;
		///==========================================================================================
		///<summary>
		/// Pixels between thumbnail and surrounding frame
		///</summary>
		property int BorderWidth;
		///==========================================================================================
		///<summary>
		// Composition algorithm to use (e.g. ReplaceCompositeOp)
		///</summary>
		property CompositeOperator Compose;
		///==========================================================================================
		///<summary>
		/// Fill color
		///</summary>
		property MagickColor^ FillColor;
		///==========================================================================================
		///<summary>
		/// Label font
		///</summary>
		property String^ Font;
		///==========================================================================================
		///<summary>
		/// Font point size
		///</summary>
		property int FontPointsize;
		///==========================================================================================
		///<summary>
		/// Frame geometry (width &amp; height frame thickness)
		///</summary>
		property MagickGeometry^ FrameGeometry;
		///==========================================================================================
		///<summary>
		/// Thumbnail width &amp; height plus border width &amp; height
		///</summary>
		property MagickGeometry^ Geometry;
		///==========================================================================================
		///<summary>
		/// Thumbnail position (e.g. SouthWestGravity)
		///</summary>
		property Gravity Gravity;
		///==========================================================================================
		///<summary>
		/// Thumbnail label (applied to image prior to montage)
		///</summary>
		property String^ Label;
		///==========================================================================================
		///<summary>
		/// Enable drop-shadows on thumbnails
		///</summary>
		property bool Shadow;
		///==========================================================================================
		///<summary>
		/// Outline color
		///</summary>
		property MagickColor^ StrokeColor;
		///==========================================================================================
		///<summary>
		/// Background texture image
		///</summary>
		property String^ TextureFileName;
		///==========================================================================================
		///<summary>
		/// Frame geometry (width &amp; height frame thickness)
		///</summary>
		property MagickGeometry^ TileGeometry;
		///==========================================================================================
		///<summary>
		/// Montage title
		///</summary>
		property String^ Title;
		///==========================================================================================
		///<summary>
		/// Transparent color
		///</summary>
		property MagickColor^ TransparentColor;
		//===========================================================================================
	};
	//==============================================================================================
}
