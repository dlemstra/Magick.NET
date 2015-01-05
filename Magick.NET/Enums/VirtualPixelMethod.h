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

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Specifies the virtual pixel methods.
	///</summary>
	public enum class VirtualPixelMethod
	{
		Undefined = Magick::UndefinedVirtualPixelMethod,
		Background = Magick::BackgroundVirtualPixelMethod,
		Dither = Magick::DitherVirtualPixelMethod,
		Edge = Magick::EdgeVirtualPixelMethod,
		Mirror = Magick::MirrorVirtualPixelMethod,
		Random = Magick::RandomVirtualPixelMethod,
		Tile = Magick::TileVirtualPixelMethod,
		Transparent = Magick::TransparentVirtualPixelMethod,
		Mask = Magick::MaskVirtualPixelMethod,
		Black = Magick::BlackVirtualPixelMethod,
		Gray = Magick::GrayVirtualPixelMethod,
		White = Magick::WhiteVirtualPixelMethod,
		HorizontalTile = Magick::HorizontalTileVirtualPixelMethod,
		VerticalTile = Magick::VerticalTileVirtualPixelMethod,
		HorizontalTileEdge = Magick::HorizontalTileEdgeVirtualPixelMethod,
		VerticalTileEdge = Magick::VerticalTileEdgeVirtualPixelMethod,
		CheckerTile = Magick::CheckerTileVirtualPixelMethod
	};
	//==============================================================================================
}