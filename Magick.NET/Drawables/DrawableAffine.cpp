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
}