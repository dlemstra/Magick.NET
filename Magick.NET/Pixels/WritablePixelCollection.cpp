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
#include "WritablePixelCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	void WritablePixelCollection::SetPixel(Pixel^ pixel)
	{
		if (pixel->Channels != Channels)
			return;

		int index = GetIndex(pixel->X, pixel->Y);

		Magick::PixelPacket *p = _Pixels + index;

		p->red = pixel[0];
		p->green = pixel[1];
		p->blue = pixel[2];
		p->opacity = pixel[3];

		if (Channels == 5)
			Indexes[index] = pixel[4];
	}
	//==============================================================================================
	WritablePixelCollection::WritablePixelCollection(Magick::Image* image, int x, int y, int width, int height)
		: PixelBaseCollection(image, width, height)
	{
		Throw::IfTrue("width", x + width > (int)image->size().width(), "Invalid X coordinate specified.");
		Throw::IfTrue("height", y + height > (int)image->size().height(), "Invalid Y coordinate specified.");

		try
		{
			_Pixels = View->get(x, y, width, height);
			LoadIndexes();
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void WritablePixelCollection::Set(Pixel^ pixel)
	{
		Throw::IfNull("pixel", pixel);
		Throw::IfFalse("pixel", pixel->Channels == Channels, "Pixel should have the same amount of channels.");

		SetPixel(pixel);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(IEnumerable<Pixel^>^ pixels)
	{
		Throw::IfNull("pixel", pixels);

		IEnumerator<Pixel^>^ enumerator = pixels->GetEnumerator();

		while(enumerator->MoveNext())
		{
			SetPixel(enumerator->Current);
		}
	}
	//==============================================================================================
	void WritablePixelCollection::Write()
	{
		View->sync();
	}
	//==============================================================================================
}