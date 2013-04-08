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
#include "DrawableCompositeImage.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableCompositeImage::DrawableCompositeImage(double x, double y, MagickImage^ image)
	{
		Throw::IfNull("image", image);

		BaseValue = new Magick::DrawableCompositeImage(x, y, (Magick::Image&)image);
	}
	//==============================================================================================
	DrawableCompositeImage::DrawableCompositeImage(double x, double y, CompositeOperator compose, 
		MagickImage^ image)
	{
		Throw::IfNull("image", image);

		BaseValue = new Magick::DrawableCompositeImage(x, y, image->Width, image->Height,
			(Magick::Image&)image, (Magick::CompositeOperator)compose);
	}
	//==============================================================================================
	DrawableCompositeImage::DrawableCompositeImage(MagickGeometry^ offset, MagickImage^ image)
	{
		Throw::IfNull("offset", offset);
		Throw::IfNull("image", image);

		BaseValue = new Magick::DrawableCompositeImage(offset->X, offset->Y, offset->Width,
			offset->Height, (Magick::Image&)image);
	}
	//==============================================================================================
	DrawableCompositeImage::DrawableCompositeImage(MagickGeometry^ offset, CompositeOperator compose,
		MagickImage^ image)
	{
		Throw::IfNull("offset", offset);
		Throw::IfNull("image", image);

		BaseValue = new Magick::DrawableCompositeImage(offset->X, offset->Y, offset->Width,
			offset->Height, (Magick::Image&)image, (Magick::CompositeOperator)compose);
	}
	//==============================================================================================
}