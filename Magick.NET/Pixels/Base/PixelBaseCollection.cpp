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
	Pixel^ PixelBaseCollection::PixelBaseCollectionEnumerator::Current::get()
	{
		if (_X == -1)
			return nullptr;

		return _Collection->CreatePixel(_X, _Y);
	}
	//==============================================================================================
	Object^ PixelBaseCollection::PixelBaseCollectionEnumerator::Current2::get()
	{
		return Current;
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
		return Pixel::Create(x, y, GetValueUnchecked(x, y));
	}
	//==============================================================================================
	array<Magick::Quantum>^ PixelBaseCollection::GetValueUnchecked(int x, int y)
	{
		array<Magick::Quantum>^ value = gcnew array<Magick::Quantum>(_Channels);

		int index = GetIndex(x, y);
		const Magick::PixelPacket* pixelPacket = Pixels + index;

		value[0] = pixelPacket->red;
		value[1] = pixelPacket->green;
		value[2] = pixelPacket->blue;
		value[3] = MaxMap - pixelPacket->opacity;

		if (_Channels == 5)
			value[4] = _Indexes[index];

		return value;
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
	Magick::IndexPacket* PixelBaseCollection::Indexes::get()
	{
		return _Indexes;
	}
	//===========================================================================================
	Magick::Pixels* PixelBaseCollection::View::get()
	{
		return _View;
	}
	//==============================================================================================
	void PixelBaseCollection::CheckIndex(int x, int y)
	{
		Throw::IfFalse("x", x >= 0 && x < _Width, "Invalid X coordinate.");
		Throw::IfFalse("y", y >= 0 && y < _Height, "Invalid Y coordinate.");
	}
	//==============================================================================================
	int PixelBaseCollection::GetIndex(int x, int y)
	{
		return (y * _Width) + x;
	}
	//==============================================================================================
	void PixelBaseCollection::LoadIndexes()
	{
		_Indexes = _View->indexes();
		_Channels = _Indexes == NULL ? 4 : 5;
	}
	//==============================================================================================
	Pixel^ PixelBaseCollection::default::get(int x, int y)
	{
		return GetPixel(x, y);
	}
	//==============================================================================================
	int PixelBaseCollection::Channels::get()
	{
		return _Channels;
	}
	//==============================================================================================
	int PixelBaseCollection::Height::get()
	{
		return _Height;
	}
	//==============================================================================================
	int PixelBaseCollection::Width::get()
	{
		return _Width;
	}
	//==============================================================================================
	IEnumerator<Pixel^>^ PixelBaseCollection::GetEnumerator()
	{
		return gcnew PixelBaseCollectionEnumerator(this);
	}
	//==============================================================================================
	System::Collections::IEnumerator^ PixelBaseCollection::GetEnumerator2()
	{
		return gcnew PixelBaseCollectionEnumerator(this);
	}
	//==============================================================================================
	Pixel^ PixelBaseCollection::GetPixel(int x, int y)
	{
		CheckIndex(x, y);

		return CreatePixel(x, y);
	}
	//==============================================================================================
	array<Magick::Quantum>^ PixelBaseCollection::GetValue(int x, int y)
	{
		CheckIndex(x, y);

		return GetValueUnchecked(x, y);
	}
	//==============================================================================================
	array<Magick::Quantum>^ PixelBaseCollection::GetValues()
	{
		long size = _Width * _Height * _Channels;

		array<Magick::Quantum>^ result = gcnew array<Magick::Quantum>(size);

		int index = 0;
		long i = 0;
		const Magick::PixelPacket* p = Pixels;

		while (i < size)
		{
			result[i++] = p->red;
			result[i++] = p->green;
			result[i++] = p->blue;
			result[i++] = p->opacity;

			if (_Channels == 5)
				result[i++] = _Indexes[index++];

			p++;
		}

		return result;
	}
	//==============================================================================================
}