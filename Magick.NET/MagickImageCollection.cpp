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
	void MagickImageCollection::CopyFrom(std::list<Magick::Image>* images)
	{
		for (std::list<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
		{
			Add(gcnew MagickImage(*iter));
		}
	}//==============================================================================================
	void MagickImageCollection::CopyTo(std::list<Magick::Image>* images)
	{
		for each(MagickImage^ image in _Images)
		{
			images->push_back(*image->ReuseImage());
		}
	}
	//==============================================================================================
	void MagickImageCollection::Merge(LayerMethod layerMethod, Magick::Image* mergedImage)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			std::list<Magick::Image>::iterator first = images->begin();
			std::list<Magick::Image>::iterator last = images->end();

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
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection()
	{
		_Images = gcnew List<MagickImage^>();
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(IEnumerable<MagickImage^>^ images)
	{
		Throw::IfNull("images", images);

		_Images = gcnew List<MagickImage^>();
		for each(MagickImage^ image in images)
		{
			_Images->Add(image);
		}
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(array<Byte>^ data)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(data);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(data, readSettings);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(fileName);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(String^ fileName, MagickReadSettings^ readSettings)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(fileName, readSettings);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(stream);
	}
	//==============================================================================================
	MagickImageCollection::MagickImageCollection(Stream^ stream, MagickReadSettings^ readSettings)
	{
		_Images = gcnew List<MagickImage^>();
		this->Read(stream, readSettings);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::default::get(int index)
	{
		return _Images[index];
	}
	//==============================================================================================
	void MagickImageCollection::default::set(int index, MagickImage^ value)
	{
		_Images[index] = value;
	}
	//==============================================================================================
	int MagickImageCollection::Count::get()
	{
		return _Images->Count;
	}
	//==============================================================================================
	bool MagickImageCollection::IsReadOnly::get()
	{
		return false;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::ReadWarning::get()
	{
		return _ReadWarning;
	}
	//==============================================================================================
	void MagickImageCollection::Add(MagickImage^ item)
	{
		_Images->Add(item);
	}
	//==============================================================================================
	void MagickImageCollection::Add(String^ fileName)
	{
		Add(gcnew MagickImage(fileName));
	}
	//==============================================================================================
	void MagickImageCollection::Clear()
	{
		_Images->Clear();
	}
	//==============================================================================================
	bool MagickImageCollection::Contains(MagickImage^ item)
	{
		return _Images->Contains(item);
	}
	//==============================================================================================
	void MagickImageCollection::CopyTo(array<MagickImage^>^ destination, int arrayIndex)
	{
		Throw::IfNull("destination", destination);
		Throw::IfOutOfRange("arrayIndex", arrayIndex, _Images->Count);
		Throw::IfOutOfRange("arrayIndex", arrayIndex, destination->Length);

		int indexI = 0;
		int length = Math::Min(destination->Length, _Images->Count);
		for (int indexA = arrayIndex; indexA < length; indexA++)
		{
			destination[indexA] = _Images[indexI++]->Copy();
		}
	}
	//==============================================================================================
	IEnumerator<MagickImage^>^ MagickImageCollection::GetEnumerator()
	{
		return _Images->GetEnumerator();
	}
	//==============================================================================================
	System::Collections::IEnumerator^ MagickImageCollection::GetEnumerator2()
	{
		return _Images->GetEnumerator();
	}
	//==============================================================================================
	int MagickImageCollection::IndexOf(MagickImage^ item)
	{
		return _Images->IndexOf(item);
	}
	//==============================================================================================
	void MagickImageCollection::Insert(int index, MagickImage^ item)
	{
		_Images->Insert(index, item);
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
		Merge(layerMethod, mergedImage);

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

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		_ReadWarning = MagickReader::Read(images, data, readSettings);
		CopyFrom(images);

		delete images;
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

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		_ReadWarning = MagickReader::Read(images, fileName, readSettings);
		CopyFrom(images);

		delete images;
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

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		_ReadWarning = MagickReader::Read(images, stream, readSettings);
		CopyFrom(images);

		delete images;
		return _ReadWarning;
	}
	//==============================================================================================
	bool MagickImageCollection::Remove(MagickImage^ item)
	{
		return _Images->Remove(item);
	}
	//==============================================================================================
	void MagickImageCollection::RemoveAt(int index)
	{
		return _Images->RemoveAt(index);
	}
	//==============================================================================================
	void MagickImageCollection::RePage()
	{
		for each(MagickImage^ image in _Images)
		{
			image->Page = gcnew MagickGeometry(0, 0);
		}
	}
	//==============================================================================================
	array<Byte>^ MagickImageCollection::ToByteArray()
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		CopyTo(images);

		Magick::Blob blob;
		MagickWriter::Write(images, &blob);

		delete images;
		return Marshaller::Marshal(&blob);
	}
	//==============================================================================================
	void MagickImageCollection::Write(Stream^ stream)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		CopyTo(images);

		MagickWriter::Write(images, stream);

		delete images;
	}
	//==============================================================================================
	void MagickImageCollection::Write(String^ fileName)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		CopyTo(images);

		MagickWriter::Write(images, fileName);

		delete images;
	}
	//==============================================================================================
}