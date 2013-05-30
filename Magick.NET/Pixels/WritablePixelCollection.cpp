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
#include "Stdafx.h"
#include "WritablePixelCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	void WritablePixelCollection::SetPixel(int x, int y, array<Magick::Quantum>^ value)
	{
		CheckIndex(x, y);
		Throw::IfTrue("pixel", value->Length != Channels, "Pixel should have the same amount of channels.");

		int index = GetIndex(x, y);

		Magick::PixelPacket *p = _Pixels + index;

		p->red = value[0];
		p->green = value[1];
		p->blue = value[2];
		p->opacity = value[3];

		if (Channels == 5)
			Indexes[index] = value[4];
	}
	//==============================================================================================
	const Magick::PixelPacket* WritablePixelCollection::Pixels::get()
	{
		return _Pixels;
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
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void WritablePixelCollection::Set(Pixel^ pixel)
	{
		Throw::IfNull("pixel", pixel);

		SetPixel(pixel->X, pixel->Y, pixel->Value);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(IEnumerable<Pixel^>^ pixels)
	{
		Throw::IfNull("pixels", pixels);

		IEnumerator<Pixel^>^ enumerator = pixels->GetEnumerator();

		while(enumerator->MoveNext())
		{
			Set(enumerator->Current);
		}
	}
	//==============================================================================================
	void WritablePixelCollection::Set(int x, int y, array<Magick::Quantum>^ value)
	{
		Throw::IfNull("value", value);

		SetPixel(x, y, value);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<Magick::Quantum>^ values)
	{
		Throw::IfNull("values", values);

		long size = values->Length - Channels;
		long i = 0;
		int index = 0;
		Magick::PixelPacket* p = _Pixels;

		while (i < size)
		{
			p->red = values[i++];
			p->green = values[i++];
			p->blue = values[i++];
			p->opacity = values[i++];

			if (Channels == 5)
				Indexes[index++] = values[i++];

			p++;
		}
	}
	//==============================================================================================
	void WritablePixelCollection::Write()
	{
		View->sync();
	}
	//==============================================================================================
}