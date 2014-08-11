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
#include "PixelCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	PixelCollection::PixelCollection(Magick::Image* image, int x, int y, int width, int height)
		: PixelBaseCollection(image, width, height)
	{
		Throw::IfTrue("width", x + width > (int)image->size().width(), "Invalid X coordinate specified: {0}.", x);
		Throw::IfTrue("height", y + height > (int)image->size().height(), "Invalid Y coordinate specified: {0}.", y);

		try
		{
			_Pixels = View->getConst(x, y, width, height);
			CheckPixels();
		}
		catch(Magick::Exception& exception)
		{
			MagickException::Throw(exception);
		}
	}
	//==============================================================================================
}