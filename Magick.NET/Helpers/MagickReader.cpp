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
#include "MagickReader.h"
#include "FileHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickReader::ApplySettingsAfter(Magick::Image* image, MagickReadSettings^ readSettings)
	{
		if (readSettings->ColorSpace.HasValue)
		{
			MagickCore::ColorspaceType colorSpaceType = (MagickCore::ColorspaceType)readSettings->ColorSpace.Value;

			if (image->colorSpace() == colorSpaceType)
				return;

			if (image->colorSpace() == MagickCore::ColorspaceType::CMYKColorspace &&
				(readSettings->ColorSpace.Value == ColorSpace::RGB || readSettings->ColorSpace.Value == ColorSpace::sRGB))
			{
				Magick::Blob blob;
				Marshaller::Marshal(ColorProfile::SRGB, &blob);
				image->profile("ICM", blob);
			}

			image->colorSpace(colorSpaceType);
		}
	}
	//==============================================================================================
	void MagickReader::ApplySettingsBefore(Magick::Image* image, MagickReadSettings^ readSettings)
	{
		if (readSettings->Density != nullptr)
			image->density(readSettings->Density);

		if (readSettings->Width.HasValue && readSettings->Height.HasValue)
		{
			Magick::Geometry geometry = Magick::Geometry(readSettings->Width.Value, readSettings->Height.Value);
			image->size(geometry);
		}
	}
	//==============================================================================================
	void MagickReader::ApplySettingsBefore(MagickCore::ImageInfo *imageInfo, MagickReadSettings^ readSettings)
	{
		if (readSettings->Density != nullptr)
		{
			std::string geometry = (Magick::Geometry)readSettings->Density;
			MagickCore::CloneString(&imageInfo->density, geometry.c_str());
		}
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, Magick::Blob* blob,
		MagickReadSettings^ readSettings)
	{
		MagickWarningException^ result = nullptr;

		if (readSettings != nullptr)
			ApplySettingsBefore(image, readSettings);

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

		if (readSettings != nullptr)
			ApplySettingsAfter(image, readSettings);

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
			if (readSettings != nullptr)
				ApplySettingsBefore(imageInfo, readSettings);

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

		if (readSettings != nullptr)
		{
			for(std::list<Magick::Image>::iterator iter = imageList->begin(); iter != imageList->end(); iter++)
			{
				ApplySettingsAfter(&*(iter), readSettings);
			}
		}

		return result;
	}
	//==============================================================================================
	MagickWarningException^ MagickReader::Read(Magick::Image* image, array<Byte>^ data,
		MagickReadSettings^ readSettings)
	{
		Throw::IfNull("data", data);
		Throw::IfTrue("data", data->Length == 0, "Empty byte array is not permitted.");

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

		if (readSettings != nullptr)
			ApplySettingsBefore(image, readSettings);

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

		if (readSettings != nullptr)
			ApplySettingsAfter(image, readSettings);

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
			if (readSettings != nullptr)
				ApplySettingsBefore(imageInfo, readSettings);

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

		if (readSettings != nullptr)
		{
			for(std::list<Magick::Image>::iterator iter = imageList->begin(); iter != imageList->end(); iter++)
			{
				ApplySettingsAfter(&*(iter), readSettings);
			}
		}

		return nullptr;
	}
	//===========================================================================================
	void MagickReader::Read(Magick::Blob* blob, Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		array<Byte>^ data = nullptr;

		int length = 0;

		if (stream->CanSeek)
		{
			length = (int)stream->Length;
			data = gcnew array<Byte>(length);
			stream->Read(data, 0, length);
		}
		else
		{
			int bufferSize = 8192;
			data = gcnew array<Byte>(bufferSize);

			int readLength;
			while ((readLength = stream->Read(data, length, bufferSize)) > 0)
			{
				Array::Resize(data, data->Length + bufferSize);
				length += readLength;
			}
		}

		Marshaller::Marshal(data, length, blob);
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
}