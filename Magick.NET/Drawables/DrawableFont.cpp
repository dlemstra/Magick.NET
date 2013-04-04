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
#include "stdafx.h"
#include "DrawableFont.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableFont::DrawableFont(String^ family)
	{
		Throw::IfNullOrEmpty("family", family);

		std::string font;
		Marshaller::Marshal(family, font);
		Value = new Magick::DrawableFont(font);
	}
	//==============================================================================================
	DrawableFont::DrawableFont(String^ family, FontStyleType style, FontWeight weight, FontStretch stretch)
	{
		Throw::IfNullOrEmpty("family", family);

		std::string font;
		Marshaller::Marshal(family, font);
		Value = new Magick::DrawableFont(font, (MagickCore::StyleType)style, (int)weight,
			(MagickCore::StretchType)stretch);
	}
	//==============================================================================================
}