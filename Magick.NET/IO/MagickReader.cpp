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
#include "..\Helpers\FileHelper.h"
#include "MagickReader.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickReader::ApplySettings(Magick::Image* image, MagickReadSettings^ readSettings)
	{
		if (readSettings == nullptr)
			return;

		if (readSettings->ColorSpace.HasValue)
		{
			// For some reason I cannot access the methods of image->options(). This trick makes sure
			// that calling image->colorspaceType() will not raise an exception.
			Magick::Geometry size = image->size();
			image->size(Magick::Geometry(1, 1));
			image->read("xc:white");

			image->colorspaceType((MagickCore::ColorspaceType)readSettings->ColorSpace.Value);
			image->size(size);
		}

		if (readSettings->Density != nullptr)
		{
			const Magick::Geometry* geometry = readSettings->Density->CreateGeometry();
			image->density(*geometry);
			delete geometry;
		}

		if (readSettings->Width.HasValue && readSettings->Height.HasValue)
		{
			Magick::Geometry geometry = Magick::Geometry(readSettings->Width.Value, readSettings->Height.Value);
			image->size(geometry);
		}
	}
	//==============================================================================================
	void MagickReader::ApplySettings(MagickCore::ImageInfo *imageInfo, MagickReadSettings^ readSettings)
	{
		if (readSettings == nullptr)
			return;

		if (readSettings->Density != nullptr)
		{
			const Magick::Geometry* geometry = readSettings->Density->CreateGeometry();
			std::string geometryStr = *geometry;
			MagickCore::CloneString(&imageInfo->density, geometryStr.c_str());
			delete geometry;
		}

		if (readSettings->ColorSpace.HasValue)
			imageInfo->colorspace = (MagickCore::ColorspaceType)readSettings->ColorSpace.Value;
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, Magick::Blob* blob,
		MagickReadSettings^ readSettings)
	{
		MagickWarningException^ result = nullptr;

		ApplySettings(image, readSettings);

		try
		{
			if (readSettings != nullptr && readSettings->Ping)
				image->ping(*blob);
			else
				image->read(*blob);
		}
		catch (Magick::Warning& exception)
		{
			result = MagickWarningException::Create(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return result;
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(std::list<Magick::Image>* imageList, Magick::Blob* blob,
		MagickReadSettings^ readSettings)
	{
		MagickWarningException^ result = nullptr;

		try
		{
			MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(0);
			ApplySettings(imageInfo, readSettings);

			MagickCore::ExceptionInfo exceptionInfo;
			MagickCore::GetExceptionInfo(&exceptionInfo);
			MagickCore::Image *images = MagickCore::BlobToImage(imageInfo, blob->data(), blob->length(), &exceptionInfo);
			MagickCore::DestroyImageInfo(imageInfo);
			Magick::insertImages(imageList, images);
			Magick::throwException(exceptionInfo);
			MagickCore::DestroyExceptionInfo(&exceptionInfo);
		}
		catch (Magick::Warning& exception)
		{
			result = MagickWarningException::Create(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return result;
	}
	//===========================================================================================
	void MagickReader::Read(Magick::Blob* blob, Stream^ stream)
	{
		Marshaller::Marshal(Read(stream), blob);
	}
	//===========================================================================================
	void MagickReader::Read(Magick::Blob* blob, String^ fileName)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		FileStream^ stream = File::OpenRead(filePath);
		Read(blob, stream);
		delete stream;
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, array<Byte>^ data,
		MagickReadSettings^ readSettings)
	{
		Throw::IfNullOrEmpty("data", data);

		Magick::Blob blob;
		Marshaller::Marshal(data, &blob);
		return Read(image, &blob, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, Stream^ stream,
		MagickReadSettings^ readSettings)
	{
		Magick::Blob blob;
		Read(&blob, stream);
		return Read(image, &blob, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, String^ fileName,
		MagickReadSettings^ readSettings)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		MagickWarningException^ result = nullptr;

		ApplySettings(image, readSettings);

		try
		{
			std::string imageSpec;
			Marshaller::Marshal(filePath, imageSpec);

			if (readSettings != nullptr && readSettings->Ping)
				image->ping(imageSpec);
			else
				image->read(imageSpec);
		}
		catch (Magick::Warning& exception)
		{
			result = MagickWarningException::Create(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return result;
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(std::list<Magick::Image>* imageList, array<Byte>^ data,
		MagickReadSettings^ readSettings)
	{
		Throw::IfNull("data", data);
		Throw::IfTrue("data", data->Length == 0, "Empty byte array is not permitted.");

		Magick::Blob blob;
		Marshaller::Marshal(data, &blob);
		return Read(imageList, &blob, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(std::list<Magick::Image>* imageList, Stream^ stream,
		MagickReadSettings^ readSettings)
	{
		Magick::Blob blob;
		Read(&blob, stream);
		return Read(imageList, &blob, readSettings);
	}	
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(std::list<Magick::Image>* imageList, String^ fileName,
		MagickReadSettings^ readSettings)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		try
		{
			std::string imageSpec;
			Marshaller::Marshal(filePath, imageSpec);

			MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(0);
			ApplySettings(imageInfo, readSettings);

			imageSpec.copy(imageInfo->filename, MaxTextExtent-1);
			imageInfo->filename[imageSpec.length()] = 0;

			MagickCore::ExceptionInfo exceptionInfo;
			MagickCore::GetExceptionInfo(&exceptionInfo);
			MagickCore::Image* images = MagickCore::ReadImage(imageInfo, &exceptionInfo);
			MagickCore::DestroyImageInfo(imageInfo);
			Magick::insertImages(imageList, images);
			Magick::throwException(exceptionInfo);
			MagickCore::DestroyExceptionInfo(&exceptionInfo);
		}
		catch (Magick::Warning& exception)
		{
			return MagickWarningException::Create(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return nullptr;
	}
	//==============================================================================================
	array<Byte>^ MagickReader::Read(Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		if (stream->CanSeek)
		{
			int length = (int)stream->Length;
			if (length == 0)
				return gcnew array<Byte>(0);

			array<Byte>^ result = gcnew array<Byte>(length);
			stream->Read(result, 0, length);
			return result;
		}
		else
		{
			const int bufferSize = 8192;
			MemoryStream^ memStream = gcnew MemoryStream();

			array<Byte>^ buffer =  gcnew array<Byte>(bufferSize);
			int length;
			while ((length = stream->Read(buffer, 0, bufferSize)) != 0)
			{
				memStream->Write(buffer, 0, length);
			}

			array<Byte>^ result = memStream->ToArray();
			delete memStream;

			return result;
		}
	}
	//==============================================================================================
	array<Byte>^ MagickReader::Read(String^ fileName)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		FileStream^ stream = File::OpenRead(filePath);
		array<Byte>^ result = Read(stream);
		delete stream;

		return result;
	}
	//==============================================================================================
}