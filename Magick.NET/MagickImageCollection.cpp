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
	void MagickImageCollection::Merge(Magick::Image* mergedImage, LayerMethod layerMethod)
	{
		try
		{
			std::list<Magick::Image>::iterator first = _Images->begin();
			std::list<Magick::Image>::iterator last = _Images->end();

			MagickCore::ExceptionInfo exceptionInfo;
			MagickCore::GetExceptionInfo(&exceptionInfo);
			Magick::linkImages(first, last);
			MagickCore::Image* image = MagickCore::MergeImageLayers(first->image(),
				(MagickCore::ImageLayerMethod)layerMethod, &exceptionInfo);
			Magick::unlinkImages(first, last);
			mergedImage->replaceImage( image );
			Magick::throwException(exceptionInfo);
			(void)MagickCore::DestroyExceptionInfo(&exceptionInfo);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection()
	{
		_Images = new std::list<Magick::Image>();
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(MagickBlob^ blob)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(blob);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(MagickBlob^ blob, ImageMagick::ColorSpace colorSpace)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(blob, colorSpace);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(fileName);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName, ImageMagick::ColorSpace colorSpace)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(fileName, colorSpace);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(stream);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream, ImageMagick::ColorSpace colorSpace)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(stream, colorSpace);
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
	MagickImage^ MagickImageCollection::Merge(LayerMethod layerMethod)
	{
		Magick::Image* mergedImage = new Magick::Image();
		Merge(mergedImage, layerMethod);
		return gcnew MagickImage(*mergedImage);
	}

	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(MagickBlob^ blob)
	{
		_ReadWarning = MagickReader::Read(_Images, blob);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(MagickBlob^ blob, ImageMagick::ColorSpace colorSpace)
	{
		_ReadWarning = MagickReader::Read(_Images, blob, colorSpace);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(String^ fileName)
	{
		_ReadWarning = MagickReader::Read(_Images, fileName);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(String^ fileName, ImageMagick::ColorSpace colorSpace)
	{
		_ReadWarning = MagickReader::Read(_Images, fileName, colorSpace);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(Stream^ stream)
	{
		_ReadWarning = MagickReader::Read(_Images, stream);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(Stream^ stream, ImageMagick::ColorSpace colorSpace)
	{
		_ReadWarning = MagickReader::Read(_Images, stream, colorSpace);
		return _ReadWarning;
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
	void MagickImageCollection::RePage()
	{
		for (std::list<Magick::Image>::iterator iter = _Images->begin(), end = _Images->end(); iter != end; ++iter)
		{
			iter->page(Magick::Geometry(0,0));
		}
	}
	//==============================================================================================
}