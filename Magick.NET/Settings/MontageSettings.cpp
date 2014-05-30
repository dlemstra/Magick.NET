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
#include "Stdafx.h"
#include "MontageSettings.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void MontageSettings::Apply(Magick::MontageFramed *settings)
	{
		if (BackgroundColor != nullptr)
			settings->backgroundColor(*BackgroundColor->CreateColor());
		else
			settings->backgroundColor(Magick::Color());
		if (BorderColor != nullptr)
			settings->borderColor(*BorderColor->CreateColor());
		else
			settings->borderColor(Magick::Color());
		settings->borderWidth(BorderWidth);
		settings->compose((MagickCore::CompositeOperator) Compose);
		if (FillColor != nullptr)
			settings->fillColor(*FillColor->CreateColor());
		else
			settings->fillColor(Magick::Color());
		std::string font;
		settings->font(Marshaller::Marshal(Font, font));
		if (FrameGeometry != nullptr)
			settings->frameGeometry(*Geometry->CreateGeometry());
		else
			settings->frameGeometry(Magick::Geometry());
		settings->pointSize(FontPointsize);
		if (Geometry != nullptr)
			settings->geometry(*Geometry->CreateGeometry());
		else
			settings->geometry(Magick::Geometry());
		settings->compose((MagickCore::CompositeOperator) Compose);
		settings->gravity((MagickCore::GravityType) Gravity);
		std::string label;
		settings->label(Marshaller::Marshal(Label, label));
		settings->shadow(Shadow);
		if (StrokeColor != nullptr)
			settings->strokeColor(*StrokeColor->CreateColor());
		else
			settings->strokeColor(Magick::Color());
		std::string textureFileName;
		settings->texture(Marshaller::Marshal(TextureFileName, textureFileName));
		if (TileGeometry != nullptr)
			settings->tile(Magick::Geometry(*TileGeometry->CreateGeometry()));
		else
			settings->tile(Magick::Geometry());
		std::string title;
		settings->title(Marshaller::Marshal(Title, title));
		if (TransparentColor != nullptr)
			settings->transparentColor(*TransparentColor->CreateColor());
		else
			settings->transparentColor(Magick::Color());
	}
	//==============================================================================================
	MontageSettings::MontageSettings()
	{
		Magick::Montage settings;
		BackgroundColor = gcnew MagickColor(settings.backgroundColor());
		Compose = (CompositeOperator) settings.compose();
		FillColor = gcnew MagickColor(settings.fillColor());
		FontPointsize = (int)settings.pointSize();
		Geometry = gcnew MagickGeometry(settings.geometry());
		Gravity = (ImageMagick::Gravity) settings.gravity();
		Shadow = settings.shadow();
	}
	//==============================================================================================
}