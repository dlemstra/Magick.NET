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
		Undefined = MagickCore::UndefinedVirtualPixelMethod,
		Background = MagickCore::BackgroundVirtualPixelMethod,
		Dither = MagickCore::DitherVirtualPixelMethod,
		Edge = MagickCore::EdgeVirtualPixelMethod,
		Mirror = MagickCore::MirrorVirtualPixelMethod,
		Random = MagickCore::RandomVirtualPixelMethod,
		Tile = MagickCore::TileVirtualPixelMethod,
		Transparent = MagickCore::TransparentVirtualPixelMethod,
		Mask = MagickCore::MaskVirtualPixelMethod,
		Black = MagickCore::BlackVirtualPixelMethod,
		Gray = MagickCore::GrayVirtualPixelMethod,
		White = MagickCore::WhiteVirtualPixelMethod,
		HorizontalTile = MagickCore::HorizontalTileVirtualPixelMethod,
		VerticalTile = MagickCore::VerticalTileVirtualPixelMethod,
		HorizontalTileEdge = MagickCore::HorizontalTileEdgeVirtualPixelMethod,
		VerticalTileEdge = MagickCore::VerticalTileEdgeVirtualPixelMethod,
		CheckerTileV = MagickCore::CheckerTileVirtualPixelMethod
	};
	//==============================================================================================
}