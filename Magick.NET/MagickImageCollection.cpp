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
#include "MagickImageCollection.h"

namespace ImageMagick
{

	//==============================================================================================
	void MagickImageCollection::AddFrom(std::list<Magick::Image>* images)
	{
		for (std::list<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
		{
			Add(gcnew MagickImage(*iter));
		}
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Append(bool vertically)
	{
		if (Count == 0)
			return nullptr;

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
			HandleException(exception);
			return nullptr;
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
		AddFrom(images);
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
	void MagickImageCollection::HandleException(const Magick::Exception& exception)
	{
		HandleException(MagickException::Create(exception));
	}
	//==============================================================================================
	void MagickImageCollection::HandleException(MagickException^ exception)
	{
		if (exception == nullptr)
			return;

		MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
		if (warning == nullptr)
			throw exception;

		if (_WarningEvent != nullptr)
			_WarningEvent->Invoke(this, gcnew WarningEventArgs(warning));
	}
	//==============================================================================================
	void MagickImageCollection::HandleReadException(MagickException^ exception)
	{
		if (exception == nullptr)
			return;

		MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
		if (warning == nullptr)
			throw exception;

		_ReadWarning = warning;
		if (_WarningEvent != nullptr)
			_WarningEvent->Invoke(this, gcnew WarningEventArgs(warning));
	}
	//==============================================================================================
	void MagickImageCollection::Optimize(LayerMethod optizeMethod)
	{
		if (Count == 0)
			return;

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
			HandleException(exception);
		}
		finally
		{
			delete images;
			delete optimizedImages;
		}
	}
	//==============================================================================================
	void MagickImageCollection::SetFormat(ImageFormat^ format)
	{
		MagickFormat magickFormat = MagickFormat::Unknown;

		if (format == ImageFormat::Gif)
			magickFormat = MagickFormat::Gif;
		else if (format == ImageFormat::Icon)
			magickFormat = MagickFormat::Icon;
		else if (format == ImageFormat::Tiff)
			magickFormat = MagickFormat::Tiff;
		else
			throw gcnew NotSupportedException("Unsupported image format: " + format->ToString());

		for each (MagickImage^ image in _Images)
		{
			image->Format = magickFormat;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Smush(bool vertically, int offset)
	{
		if (Count == 0)
			return nullptr;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image smushedImage;
			Magick::smushImages(&smushedImage, images->begin(), images->end(), offset, vertically);

			return gcnew MagickImage(smushedImage);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete images;
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
			HandleException(exception);
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
	void MagickImageCollection::Warning::add(EventHandler<WarningEventArgs^>^ handler)
	{
		_WarningEvent += handler;
	}
	//==============================================================================================
	void MagickImageCollection::Warning::remove(EventHandler<WarningEventArgs^>^ handler)
	{
		_WarningEvent -= handler;
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
	MagickWarningException^ MagickImageCollection::AddRange(array<Byte>^ data)
	{
		return AddRange(data, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::AddRange(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		HandleReadException(MagickReader::Read(images, data, readSettings));
		AddFrom(images);

		delete images;
		return _ReadWarning;
	}
	//==============================================================================================
	void MagickImageCollection::AddRange(MagickImageCollection^ images)
	{
		Throw::IfNull("images", images);

		int count = images->Count;
		for (int i=0; i < count; i++)
		{
			Add(images[i]->Clone());
		}
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::AddRange(Stream^ stream)
	{
		return AddRange(stream, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::AddRange(Stream^ stream, MagickReadSettings^ readSettings)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		HandleReadException(MagickReader::Read(images, stream, readSettings));
		AddFrom(images);

		delete images;
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::AddRange(String^ fileName)
	{
		return AddRange(fileName, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImageCollection::AddRange(String^ fileName, MagickReadSettings^ readSettings)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		HandleReadException(MagickReader::Read(images, fileName, readSettings));
		AddFrom(images);

		delete images;
		return _ReadWarning;
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
		if (Count == 0)
			nullptr;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image combinedImage;
			Magick::combineImages(&combinedImage, images->begin(), images->end(),
				(Magick::ChannelType)channels);

			return gcnew MagickImage(combinedImage);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	void MagickImageCollection::Coalesce()
	{
		if (Count == 0)
			return;

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
			HandleException(exception);
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
		if (Count == 0)
			return;

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
		if (Count == 0)
			return;

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
			HandleException(exception);
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
		if (Count == 0)
			return nullptr;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			Magick::Image evaluatedImage;
			Magick::evaluateImages(&evaluatedImage, images->begin(), images->end(),
				(Magick::MagickEvaluateOperator)evaluateOperator);

			return gcnew MagickImage(evaluatedImage);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Flatten()
	{
		if (Count == 0)
			return nullptr;

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
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Fx(String^ expression)
	{
		if (Count == 0)
			return nullptr;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();

		try
		{
			CopyTo(images);

			std::string fxExpression;
			Marshaller::Marshal(expression, fxExpression);

			Magick::Image fxImage;
			Magick::fxImages(&fxImage, images->begin(), images->end(), fxExpression);

			return gcnew MagickImage(fxImage);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
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
		if (Count == 0)
			return nullptr;

		Magick::Image mergedImage;
		Merge(&mergedImage, LayerMethod::Merge);
		return gcnew MagickImage(mergedImage);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::Montage(MontageSettings^ settings)
	{
		if (Count == 0)
			return nullptr;

		Throw::IfNull("settings", settings);

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		MagickImageCollection^ collection = gcnew MagickImageCollection();

		try
		{
			CopyTo(images);

			Magick::MontageFramed options;
			settings->Apply(&options);
			Magick::montageImages(images, images->begin(), images->end(), options);
			collection->CopyFrom(images);
			return collection->Merge();
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete images;
			delete collection;
		}

	}
	//==============================================================================================
	MagickImageCollection^ MagickImageCollection::Morph(int frames)
	{
		if (Count == 0)
			return nullptr;

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
			HandleException(exception);
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
		if (Count == 0)
			return nullptr;

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
	MagickErrorInfo^ MagickImageCollection::Quantize(QuantizeSettings^ settings)
	{
		if (Count == 0)
			return nullptr;

		Throw::IfNull("settings", settings);

		MagickImage^ colorMap = nullptr;

		try 
		{
			colorMap=AppendHorizontally();
			MagickErrorInfo^ result = colorMap->Quantize(settings);

			for each (MagickImage^ image in _Images)
			{
				image->Map(colorMap);
			}

			return result;
		}
		finally
		{
			if(colorMap != nullptr)
				delete colorMap;
		}
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
		HandleReadException(MagickReader::Read(images, data, readSettings));
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
		HandleReadException(MagickReader::Read(images, fileName, readSettings));
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
		HandleReadException(MagickReader::Read(images, stream, readSettings));
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
	MagickImage^ MagickImageCollection::SmushHorizontal(int offset)
	{
		return Smush(false, offset);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::SmushVertical(int offset)
	{
		return Smush(true, offset);
	}
	//==============================================================================================
	array<Byte>^ MagickImageCollection::ToByteArray()
	{
		if (Count == 0)
			return nullptr;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			CopyTo(images);

			Magick::Blob blob;
			HandleException(MagickWriter::Write(images, &blob));
			return Marshaller::Marshal(&blob);
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	Bitmap^ MagickImageCollection::ToBitmap()
	{
		return ToBitmap(ImageFormat::Tiff);;
	}
	//==============================================================================================
	Bitmap^ MagickImageCollection::ToBitmap(ImageFormat^ imageFormat)
	{
		SetFormat(imageFormat);

		MemoryStream^ memStream = gcnew MemoryStream();
		Write(memStream);
		memStream->Position = 0;
		// Do not dispose the memStream, the bitmap owns it.
		return gcnew Bitmap(memStream);
	}
	//==============================================================================================
	MagickImage^ MagickImageCollection::TrimBounds()
	{
		if (Count == 0)
			return nullptr;

		Magick::Image trimBoundsImage;
		Merge(&trimBoundsImage, LayerMethod::Trimbounds);
		return gcnew MagickImage(trimBoundsImage);
	}
	//==============================================================================================
	void MagickImageCollection::Write(Stream^ stream)
	{
		if (Count == 0)
			return;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			CopyTo(images);
			HandleException(MagickWriter::Write(images, stream));
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	void MagickImageCollection::Write(String^ fileName)
	{
		if (Count == 0)
			return;

		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			CopyTo(images);
			HandleException(MagickWriter::Write(images, fileName));
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
}