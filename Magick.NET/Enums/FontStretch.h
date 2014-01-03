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

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Specifies font stretch type.
	///</summary>
	public enum class FontStretch
	{
		Undefined = Magick::UndefinedStretch,
		Normal = Magick::NormalStretch,
		UltraCondensed = Magick::UltraCondensedStretch,
		ExtraCondensed = Magick::ExtraCondensedStretch,
		Condensed = Magick::CondensedStretch,
		SemiCondensed = Magick::SemiCondensedStretch,
		SemiExpanded = Magick::SemiExpandedStretch,
		Expanded = Magick::ExpandedStretch,
		ExtraExpanded = Magick::ExtraExpandedStretch,
		UltraExpanded = Magick::UltraExpandedStretch,
		Any = Magick::AnyStretch
	};
	//==============================================================================================
}