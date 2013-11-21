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
#include "..\Quantum.h"

namespace ImageMagick
{
	//==============================================================================================
	void WritablePixelCollection::SetPixel(int x, int y, array<Magick::Quantum>^ value)
	{
		CheckIndex(x, y);
		Throw::IfTrue("value", value->Length != Channels, "Value should have the same amount of channels.");

		int index = GetIndex(x, y);

		Magick::PixelPacket *p = _Pixels + index;
		SetPixel(p, value, index);

		if (Channels == 5)
			Indexes[index] = Quantum::Convert(value[4]);
	}
	//==============================================================================================
	template<typename TType>
	int WritablePixelCollection::SetPixel(Magick::PixelPacket *pixel, array<TType>^ value, int index)
	{
		pixel->red = Quantum::Convert(value[index++]);
		pixel->green = Quantum::Convert(value[index++]);
		pixel->blue = Quantum::Convert(value[index++]);
		pixel->opacity = Quantum::Convert(value[index++]);

		return index;
	}
	//==============================================================================================
	template<typename TType>
	void WritablePixelCollection::SetPixels(array<TType>^ values)
	{
		Throw::IfNullOrEmpty("values", values);
		Throw::IfFalse("values", values->Length % Channels == 0, "Values should have the same amount of channels.");

		long i = 0;
		int index = 0;
		Magick::PixelPacket* p = _Pixels;

		while (i < values->Length)
		{
			i = SetPixel(p, values, i);

			if (Channels == 5)
				Indexes[index++] = Quantum::Convert((TType)values[i++]);

			p++;
		}
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
			_Pixels = View->set(x, y, width, height);
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
		Throw::IfNullOrEmpty("value", value);

		SetPixel(x, y, value);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<Byte>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<double>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<int>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<long>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<Magick::Quantum>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Set(array<short>^ values)
	{
		SetPixels(values);
	}
	//==============================================================================================
	void WritablePixelCollection::Write()
	{
		View->sync();
	}
	//==============================================================================================
}