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
#include "DrawableAffine.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableAffine::DrawableAffine(double scaleX, double scaleY, double shearX, double shearY,
		double translateX, double translateY)
	{
		BaseValue = new Magick::DrawableAffine(scaleX, scaleY, shearX, shearY, translateX, translateY);
	}
	//==============================================================================================
	DrawableAffine::DrawableAffine(Matrix^ matrix)
	{
		Throw::IfNull("matrix", matrix);

		BaseValue = new Magick::DrawableAffine((double)matrix->Elements[0], (double)matrix->Elements[1],
			(double)matrix->Elements[2], (double)matrix->Elements[3], (double)matrix->Elements[4],
			(double)matrix->Elements[5]);
	}
	//==============================================================================================
	double DrawableAffine::ScaleX::get()
	{
		return Value->sx();
	}
	//==============================================================================================
	void DrawableAffine::ScaleX::set(double value)
	{
		Value->sx(value);
	}
	//==============================================================================================
	double DrawableAffine::ScaleY::get()
	{
		return Value->sy();
	}
	//==============================================================================================
	void DrawableAffine::ScaleY::set(double value)
	{
		Value->sy(value);
	}
	//==============================================================================================
	double DrawableAffine::ShearX::get()
	{
		return Value->rx();
	}
	//==============================================================================================
	void DrawableAffine::ShearX::set(double value)
	{
		Value->rx(value);
	}
	//==============================================================================================
	double DrawableAffine::ShearY::get()
	{
		return Value->ry();
	}
	//==============================================================================================
	void DrawableAffine::ShearY::set(double value)
	{
		Value->ry(value);
	}
	//==============================================================================================
	double DrawableAffine::TranslateX::get()
	{
		return Value->tx();
	}
	//==============================================================================================
	void DrawableAffine::TranslateX::set(double value)
	{
		Value->tx(value);
	}
	//==============================================================================================
	double DrawableAffine::TranslateY::get()
	{
		return Value->ty();
	}
	//==============================================================================================
	void DrawableAffine::TranslateY::set(double value)
	{
		Value->ty(value);
	}
	//==============================================================================================
}