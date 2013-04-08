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
#include "DrawableText.h"

namespace ImageMagick
{
	//==============================================================================================
	void DrawableText::Initialize(double x, double y, String^ text, String^ encoding)
	{
		Throw::IfNullOrEmpty("text", text);

		std::string drawText;
		Marshaller::Marshal(text, drawText);

		if (encoding == nullptr)
		{
			BaseValue = new Magick::DrawableText(x, y, drawText);
		}
		else
		{
			std::string drawEncoding;
			Marshaller::Marshal(encoding, drawEncoding);
			BaseValue = new Magick::DrawableText(x, y, drawText, drawEncoding);
		}
	}
	//==============================================================================================
	DrawableText::DrawableText(double x, double y, String^ text)
	{
		Initialize(x, y, text, nullptr);
	}
	//==============================================================================================
	DrawableText::DrawableText(double x, double y, String^ text, System::Text::Encoding^ encoding)
	{
		Throw::IfFalse("encoding", encoding == System::Text::Encoding::UTF8, "Only UTF-8 seems to be supported for now");

		Initialize(x, y, text, "UTF-8");
	}
	//==============================================================================================
}