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
	/// Specifies the placement gravity.
	///</summary>
	public enum class Gravity
	{
		Undefined = Magick::UndefinedGravity,
		Forget = Magick::ForgetGravity,
		Northwest = Magick::NorthWestGravity,
		North = Magick::NorthGravity,
		Northeast = Magick::NorthEastGravity,
		West = Magick::WestGravity, 
		Center = Magick::CenterGravity,
		East = Magick::EastGravity,
		Southwest = Magick::SouthWestGravity,
		South = Magick::SouthGravity,
		Southeast = Magick::SouthEastGravity,
		Static = Magick::StaticGravity
	};
	//==============================================================================================
}