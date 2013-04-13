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
#include "MagickImageCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickImageCollection::MagickImageCollection()
	{
		_Images = new std::list<Magick::Image>();
	}
	//==============================================================================================
	bool MagickImageCollection::MagickImageCollectionEnumerator::MoveNext()
	{
		if (_Index + 1 == (int)_Collection->_Images->size())
			return false;

		_Index++;
		return true;
	}
	//==============================================================================================
	void MagickImageCollection::MagickImageCollectionEnumerator::Reset()
	{
		_Index = -1;
	}
	//==============================================================================================
	void MagickImageCollection::Add(MagickImage^ item)
	{
		Throw::IfNull("image", item);

		_Images->push_back(*item->ReuseImage());
	}
	//==============================================================================================
	void MagickImageCollection::Clear()
	{
		_Images->clear();
	}
	//==============================================================================================
	bool MagickImageCollection::Contains(MagickImage^ item)
	{
		return IndexOf(item) != -1;
	}
	//==============================================================================================
	void MagickImageCollection::CopyTo(array<MagickImage^>^ destination, int arrayIndex)
	{
		Throw::IfNull("destination", destination);
		Throw::IfOutOfRange("arrayIndex", arrayIndex, destination->Length);

		for (std::list<Magick::Image>::const_iterator iter = _Images->begin(), end = _Images->end(); iter != end; ++iter)
		{
			destination[arrayIndex++] = gcnew MagickImage(*iter);
		}
	}
	//==============================================================================================
	IEnumerator<MagickImage^>^ MagickImageCollection::GetEnumerator()
	{
		return gcnew MagickImageCollectionEnumerator(this);
	}
	//==============================================================================================
	System::Collections::IEnumerator^ MagickImageCollection::GetEnumerator2()
	{
		return gcnew MagickImageCollectionEnumerator(this);
	}
	//==============================================================================================
	int MagickImageCollection::IndexOf(MagickImage^ item)
	{
		Throw::IfNull("image", item);

		int index = 0;

		for (std::list<Magick::Image>::const_iterator iter = _Images->begin(), end = _Images->end(); iter != end; ++iter)
		{
			if (item->Equals(*iter))
				return index;

			index++;
		}

		return -1;
	}
	//==============================================================================================
	void MagickImageCollection::Insert(int index, MagickImage^ image)
	{
		Throw::IfNull("image", image);
		Throw::IfOutOfRange("arrayIndex", index, (int)_Images->size());

		std::list<Magick::Image>::iterator iter = _Images->begin();
		std::advance(iter, index);

		_Images->insert(iter, *image->ReuseImage());
	}
	//==============================================================================================
	MagickImageCollection^ MagickImageCollection::Read(String^ fileName)
	{
		MagickImageCollection^ imageList = gcnew MagickImageCollection();
		imageList->_ReadWarning = MagickReader::Read(imageList->_Images, fileName);
		return imageList;
	}
	//==============================================================================================
	MagickImageCollection^ MagickImageCollection::Read(String^ fileName, ColorSpace colorSpace)
	{
		MagickImageCollection^ imageList = gcnew MagickImageCollection();
		imageList->_ReadWarning = MagickReader::Read(imageList->_Images, fileName, colorSpace);
		return imageList;
	}
	//==============================================================================================
	bool MagickImageCollection::Remove(MagickImage^ item)
	{
		Throw::IfNull("image", item);

		int index = IndexOf(item);

		if (index == -1)
			return false;

		RemoveAt(index);

		return true;
	}
	//==============================================================================================
	void MagickImageCollection::RemoveAt(int index)
	{
		Throw::IfOutOfRange("arrayIndex", index, (int)_Images->size());

		std::list<Magick::Image>::iterator iter = _Images->begin();
		std::advance(iter, index);
		_Images->erase(iter);
	}
	//==============================================================================================
}