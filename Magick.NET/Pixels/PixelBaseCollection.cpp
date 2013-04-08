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
#include "PixelBaseCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	PixelBaseCollection::PixelBaseCollectionEnumerator::PixelBaseCollectionEnumerator(PixelBaseCollection^ collection)
	{
		_Collection = collection;

		Reset();
	}
	//==============================================================================================
	bool PixelBaseCollection::PixelBaseCollectionEnumerator::MoveNext()
	{
		if (++_X == _Collection->_Width)
		{
			_X = 0;
			_Y++;
		}

		if (_Y < _Collection->_Height)
			return true;

		_X = _Collection->_Width - 1;
		_Y = _Collection->_Height - 1;
		return false;
	}
	//==============================================================================================
	void PixelBaseCollection::PixelBaseCollectionEnumerator::Reset()
	{
		_X = -1;
		_Y = 0;
	}
	//==============================================================================================
	Pixel^ PixelBaseCollection::CreatePixel(int x, int y)
	{
		int index = GetIndex(x, y);

		const Magick::PixelPacket* pixelPacket = Pixels + index;

		Pixel^ pixel = gcnew Pixel(x, y, _Channels);
		pixel[0] = pixelPacket->red;
		pixel[1] = pixelPacket->green;
		pixel[2] = pixelPacket->blue;
		pixel[3] = pixelPacket->opacity;

		if (_Channels == 5)
			pixel[4] = _Indexes[index];

		return pixel;
	}
	//==============================================================================================
	PixelBaseCollection::PixelBaseCollection(Magick::Image* image, int width, int height)
	{
		Throw::IfTrue("width", width > (int)image->size().width(), "Invalid width specified.");
		Throw::IfTrue("height", height > (int)image->size().height(), "Invalid height specified.");

		_View = new Magick::Pixels(*image);
		_Width = width;
		_Height = height;
	}
	//==============================================================================================
	int PixelBaseCollection::GetIndex(int x, int y)
	{
		Throw::IfFalse("x", x >= 0 && x < _Width, "Invalid X coordinate.");
		Throw::IfFalse("y", y >= 0 && y < _Height, "Invalid Y coordinate.");

		return (y * _Width) + x;
	}
	//==============================================================================================
	void PixelBaseCollection::LoadIndexes()
	{
		_Indexes = _View->indexes();
		_Channels = _Indexes == NULL ? 4 : 5;
	}
	//==============================================================================================
	Pixel^ PixelBaseCollection::Get(int x, int y)
	{
		return CreatePixel(x, y);
	}
	//==============================================================================================
	array<array<Magick::Quantum>^>^ PixelBaseCollection::GetValues()
	{
		array<array<Magick::Quantum>^>^ result = gcnew array<array<Magick::Quantum>^>(_Width);

		const Magick::PixelPacket* pixel = Pixels;
		long size = _Height * _Channels;

		for (int x = 0; x < _Width; x++)
		{
			result[x] = gcnew array<Magick::Quantum>(size);

			int q = 0;
			for (int y = 0; y < _Height; y++)
			{ 
				result[x][q] = pixel->red;
				result[x][++q] = pixel->green;
				result[x][++q] = pixel->blue;
				result[x][++q] = pixel->opacity;

				if (_Channels == 5)
					result[x][++q] = _Indexes[y];

				pixel++;
			}
		}

		return result;
	}
	//==============================================================================================
}