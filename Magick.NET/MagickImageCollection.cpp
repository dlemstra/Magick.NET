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
	MagickImage^ MagickImageCollection::Append(bool vertically)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image appendedImage;
			Magick::appendImages(&appendedImage, images->begin(), images->end(), vertically);

			return gcnew MagickImage(appendedImage);
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
	void MagickImageCollection::CopyFrom(std::list<Magick::Image>* images)
	{
		Clear();

		for (std::list<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
		{
			Add(gcnew MagickImage(*iter));
		}
	}
	//==============================================================================================
	void MagickImageCollection::CopyTo(std::list<Magick::Image>* images)
	{
		for each(MagickImage^ image in _Images)
		{
			images->push_back(image->ReuseValue());
		}
	}
	//==============================================================================================
	void MagickImageCollection::Optimize(LayerMethod optizeMethod)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		std::list<Magick::Image>* optimizedImages = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			if (optizeMethod == LayerMethod::OptimizeImage)
				Magick::optimizeImageLayers(optimizedImages, images->begin(), images->end());
			else
				Magick::optimizePlusImageLayers(optimizedImages, images->begin(), images->end());

			CopyFrom(optimizedImages);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete images;
			delete optimizedImages;
		}
	}
	//==============================================================================================
	List<MagickImage^>^ MagickImageCollection::CreateList(std::list<Magick::Image>* images)
	{
		List<MagickImage^>^ list = gcnew List<MagickImage^>();

		for (std::list<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
		{
			list->Add(gcnew MagickImage(*iter));
		}

		return list;
	}
	//==============================================================================================
	void MagickImageCollection::Merge(Magick::Image* image, LayerMethod method)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::mergeImageLayers(image, images->begin(), images->end(), (Magick::ImageLayerMethod)method);
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
	MagickImage^ MagickImageCollection::AppendHorizontally()
	{
		return Append(false);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::AppendVertically()
	{
		return Append(true);
	}
	//==============================================================================================
	void MagickImageCollection::Clear()
	{
		for each(MagickImage^ image in _Images)
		{
			delete image;
		}

		_Images->Clear();
	}
	//==============================================================================================
	bool MagickImageCollection::Contains(MagickImage^ item)
	{
		return _Images->Contains(item);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Combine()
	{
		return Combine(Channels::All);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Combine(Channels channels)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image combinedImage;
			Magick::combineImages(&combinedImage, images->begin(), images->end(),
				(MagickCore::ChannelType)channels);

			return gcnew MagickImage(combinedImage);
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
	void MagickImageCollection::Coalesce()
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		std::list<Magick::Image>* coalescedImages = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::coalesceImages(coalescedImages, images->begin(), images->end());

			CopyFrom(coalescedImages);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete images;
			delete coalescedImages;
		}
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
			destination[indexA] = _Images[indexI++]->Clone();
		}
	}
	//==============================================================================================
	void MagickImageCollection::Deconstruct()
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		std::list<Magick::Image>* deconstructedImages = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::deconstructImages(deconstructedImages, images->begin(), images->end());

			CopyFrom(deconstructedImages);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete images;
			delete deconstructedImages;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Evaluate(EvaluateOperator evaluateOperator)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image evaluatedImage;
			Magick::evaluateImages(&evaluatedImage, images->begin(), images->end(),
				(MagickCore::MagickEvaluateOperator)evaluateOperator);

			return gcnew MagickImage(evaluatedImage);
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
	MagickImage^ MagickImageCollection::Flatten()
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image flattendImage;
			Magick::flattenImages(&flattendImage, images->begin(), images->end());

			return gcnew MagickImage(flattendImage);
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
	MagickImage^ MagickImageCollection::Merge()
	{
		Magick::Image mergedImage;
		Merge(&mergedImage, LayerMethod::Merge);
		return gcnew MagickImage(mergedImage);
	}
	//==============================================================================================
	MagickImageCollection^ MagickImageCollection::Morph(int frames)
	{
		Throw::IfTrue("frames", frames < 1, "Frames must be at least 1.");

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		std::list<Magick::Image>* morphedImages = new std::list<Magick::Image>();

		MagickImageCollection^ result = gcnew MagickImageCollection();

		try
		{
			CopyTo(images);

			Magick::morphImages(morphedImages, images->begin(), images->end(), frames);

			result->CopyFrom(morphedImages);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete images;
			delete morphedImages;
		}

		return result;
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Mosaic()
	{
		Magick::Image mosaicImage;
		Merge(&mosaicImage, LayerMethod::Mosaic);
		return gcnew MagickImage(mosaicImage);
	}
	//==============================================================================================
	void MagickImageCollection::Optimize()
	{
		Optimize(LayerMethod::OptimizeImage);
	}
	//==============================================================================================
	void MagickImageCollection::OptimizePlus()
	{
		Optimize(LayerMethod::OptimizePlus);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(array<Byte>^ data)
	{
		return Read(data, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::Read(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
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
	void MagickImageCollection::Reverse()
	{
		_Images->Reverse();
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
	MagickImage^ MagickImageCollection::TrimBounds()
	{
		Magick::Image trimBoundsImage;
		Merge(&trimBoundsImage, LayerMethod::Trimbounds);
		return gcnew MagickImage(trimBoundsImage);
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