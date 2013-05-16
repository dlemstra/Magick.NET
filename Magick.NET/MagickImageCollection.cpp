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
#include "MagickImageCollection.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickImageCollection::InsertUnchecked(int index, MagickImage^ image)
	{
		std::list<Magick::Image>::iterator iter = Images->begin();
		std::advance(iter, index);

		Images->insert(iter, *image->ReuseImage());
	}
	//==============================================================================================
	bool MagickImageCollection::MagickImageCollectionEnumerator::MoveNext()
	{
		if (_Index + 1 == (int)_Collection->Images->size())
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
			std::list<Magick::Image>::iterator first = Images->begin();
			std::list<Magick::Image>::iterator last = Images->end();

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
	MagickImageCollection::MagickImageCollection(IEnumerable<MagickImage^>^ images)
	{
		Throw::IfNull("images", images);

		IEnumerator<MagickImage^>^ enumerator = images->GetEnumerator();

		_Images = new std::list<Magick::Image>();
		while(enumerator->MoveNext())
		{
			Images->push_back(*enumerator->Current->ReuseImage());
		}
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(array<Byte>^ data)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(data);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(data, readSettings);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(fileName);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName, MagickReadSettings^ readSettings)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(fileName, readSettings);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(stream);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream, MagickReadSettings^ readSettings)
	{
		_Images = new std::list<Magick::Image>();
		this->Read(stream, readSettings);
	}
	//==============================================================================================
	void MagickImageCollection::Add(MagickImage^ item)
	{
		Throw::IfNull("image", item);

		Images->push_back(*item->ReuseImage());
	}
	//==============================================================================================
	void MagickImageCollection::Add(String^ fileName)
	{
		Add(gcnew MagickImage(fileName));
	}
	//==============================================================================================
	void MagickImageCollection::Clear()
	{
		Images->clear();
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

		for (std::list<Magick::Image>::const_iterator iter = Images->begin(), end = Images->end(); iter != end; ++iter)
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

		for (std::list<Magick::Image>::const_iterator iter = Images->begin(), end = Images->end(); iter != end; ++iter)
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
		Throw::IfOutOfRange("arrayIndex", index, (int)Images->size());

		InsertUnchecked(index, image);
	}
	//==============================================================================================
	void MagickImageCollection::Insert(int index, String^ fileName)
	{
		Insert(index, gcnew MagickImage(fileName));
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Merge(LayerMethod layerMethod)
	{
		Magick::Image* mergedImage = new Magick::Image();
		Merge(mergedImage, layerMethod);
		return gcnew MagickImage(*mergedImage);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(array<Byte>^ data)
	{
		return Read(data, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		Clear();
		_ReadWarning = MagickReader::Read(Images, data, readSettings);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(String^ fileName)
	{
		return Read(fileName, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(String^ fileName, MagickReadSettings^ readSettings)
	{
		Clear();
		_ReadWarning = MagickReader::Read(Images, fileName, readSettings);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(Stream^ stream)
	{
		return Read(stream, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(Stream^ stream, MagickReadSettings^ readSettings)
	{
		Clear();
		_ReadWarning = MagickReader::Read(Images, stream, readSettings);
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
		Throw::IfOutOfRange("arrayIndex", index, (int)Images->size());

		std::list<Magick::Image>::iterator iter = Images->begin();
		std::advance(iter, index);

		Images->erase(iter);
	}
	//==============================================================================================
	void MagickImageCollection::RePage()
	{
		for (std::list<Magick::Image>::iterator iter = Images->begin(), end = Images->end(); iter != end; ++iter)
		{
			iter->page(Magick::Geometry(0,0));
		}
	}
	//==============================================================================================
	array<Byte>^ MagickImageCollection::ToByteArray()
	{
		Magick::Blob blob;
		MagickWriter::Write(Images, &blob);
		return Marshaller::Marshal(&blob);
	}
	//==============================================================================================
	void MagickImageCollection::Write(Stream^ stream)
	{
		MagickWriter::Write(Images, stream);
	}
	//==============================================================================================
	void MagickImageCollection::Write(String^ fileName)
	{
		MagickWriter::Write(Images, fileName);
	}
	//==============================================================================================
}