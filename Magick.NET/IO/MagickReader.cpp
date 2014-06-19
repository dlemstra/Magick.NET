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
#include "..\Helpers\FileHelper.h"
#include "MagickReader.h"

namespace ImageMagick
{
	//==============================================================================================
	int MagickReader::GetExpectedLength(MagickReadSettings^ readSettings)
	{
		int length = readSettings->Width.Value * readSettings->Width.Value * readSettings->PixelStorage->Mapping->Length;
		switch (readSettings->PixelStorage->StorageType)
		{
		case StorageType::Char:
			return length;
		case StorageType::Double:
			return length * sizeof(double);
		case  StorageType::Float:
			return length * sizeof(float);
		case StorageType::Integer:
			return length * sizeof(int);
		case StorageType::Long:
			return length * sizeof(long);
		case StorageType::Quantum:
			return length * sizeof(Magick::Quantum);
		case StorageType::Short:
			return length * sizeof(short);
		case StorageType::Undefined:
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	void MagickReader::ReadPixels(Magick::Image* image, MagickReadSettings^ readSettings,
		array<Byte>^ pixels)
	{
		Throw::IfTrue("readSettings", readSettings->PixelStorage->StorageType == StorageType::Undefined, "Storage type should not be undefined.");
		Throw::IfNull("readSettings", readSettings->Width, "Width should be defined when pixel storage is set.");
		Throw::IfNull("readSettings", readSettings->Height, "Height should be defined when pixel storage is set.");
		Throw::IfNullOrEmpty("readSettings", readSettings->PixelStorage->Mapping, "Pixel storage mapping should be defined.");

		int length = GetExpectedLength(readSettings);
		Throw::IfTrue("pixels", pixels->Length != length, "The array length is " + pixels->Length + " but should be " + length + ".");

		void* convertedPixels = NULL;
		try
		{
			convertedPixels = Marshaller::Marshal(pixels, readSettings->PixelStorage->StorageType);

			std::string map;
			Marshaller::Marshal(readSettings->PixelStorage->Mapping, map);

			Magick::Geometry size = Magick::Geometry();
			image->read(readSettings->Width.Value, readSettings->Height.Value, map,
				(MagickCore::StorageType)readSettings->PixelStorage->StorageType, convertedPixels);
		}
		finally
		{
			delete[] convertedPixels;
		}
	}
	//==============================================================================================
	array<Byte>^ MagickReader::ReadUnChecked(String^ filePath)
	{
		FileStream^ stream = File::OpenRead(filePath);
		array<Byte>^ result = Read(stream);
		delete stream;

		return result;
	}
	//==============================================================================================
	MagickException^ MagickReader::Read(Magick::Image* image, array<Byte>^ bytes,
		MagickReadSettings^ readSettings)
	{
		Throw::IfNullOrEmpty("bytes", bytes);

		Magick::Blob blob;

		try
		{
			if (readSettings != nullptr)
			{
				if (readSettings->Ping)
				{
					Marshaller::Marshal(bytes, &blob);
					image->ping(blob);
					return nullptr;
				}
				else if (readSettings->PixelStorage != nullptr)
				{
					ReadPixels(image, readSettings, bytes);
					return nullptr;
				}

				readSettings->Apply(image);
			}

			Marshaller::Marshal(bytes, &blob);
			image->read(blob);

			return nullptr;
		}
		catch (Magick::Exception& exception)
		{
			return MagickException::Create(exception);
		}
	}
	//==============================================================================================
	MagickException^ MagickReader::Read(Magick::Image* image, Stream^ stream,
		MagickReadSettings^ readSettings)
	{
		return Read(image, Read(stream), readSettings);
	}
	//==============================================================================================
	MagickException^ MagickReader::Read(Magick::Image* image, String^ fileName,
		MagickReadSettings^ readSettings)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		unsigned char *pixels = NULL;

		try
		{
			std::string imageSpec;
			Marshaller::Marshal(filePath, imageSpec);

			if (readSettings != nullptr)
			{
				if (readSettings->Ping)
				{
					image->ping(imageSpec);
					return nullptr;
				}
				else if (readSettings->PixelStorage != nullptr)
				{
					array<Byte>^ bytes = ReadUnChecked(filePath);
					ReadPixels(image, readSettings, bytes);
					return nullptr;
				}

				readSettings->Apply(image);
			}

			image->read(imageSpec);

			return nullptr;
		}
		catch (Magick::Exception& exception)
		{
			return MagickException::Create(exception);
		}
		finally
		{
			if (pixels != NULL)
				delete[] pixels;
		}
	}
	//==============================================================================================
	MagickException^ MagickReader::Read(std::list<Magick::Image>* imageList, array<Byte>^ bytes,
		MagickReadSettings^ readSettings)
	{
		Throw::IfNullOrEmpty("bytes", bytes);

		MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(NULL);
		MagickCore::ExceptionInfo *exceptionInfo = MagickCore::AcquireExceptionInfo();
		unsigned char* data;

		try
		{
			if (readSettings != nullptr)
			{
				Throw::IfFalse("readSettings", readSettings->PixelStorage == nullptr,
					"PixelStorage is not supported for images with multiple frames/layers.");

				if (!readSettings->Ping)
					readSettings->Apply(imageInfo);
			}

			data = Marshaller::Marshal(bytes);
			MagickCore::Image *images;

			if (readSettings != nullptr && readSettings->Ping)
				images = MagickCore::PingBlob(imageInfo, data, bytes->Length, exceptionInfo);
			else
				images = MagickCore::BlobToImage(imageInfo, data, bytes->Length, exceptionInfo);

			Magick::insertImages(imageList, images);
			Magick::throwException(exceptionInfo);
			MagickCore::DestroyExceptionInfo(exceptionInfo);

			return nullptr;
		}
		catch (Magick::Exception& exception)
		{
			return MagickException::Create(exception);
		}
		finally
		{
			MagickCore::DestroyImageInfo(imageInfo);
			if (data != NULL)
				delete[] data;
		}
	}
	//==============================================================================================
	MagickException^ MagickReader::Read(std::list<Magick::Image>* imageList, Stream^ stream,
		MagickReadSettings^ readSettings)
	{
		return Read(imageList, Read(stream), readSettings);
	}	
	//==============================================================================================
	MagickException^ MagickReader::Read(std::list<Magick::Image>* imageList, String^ fileName,
		MagickReadSettings^ readSettings)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(NULL);
		MagickCore::ExceptionInfo *exceptionInfo = MagickCore::AcquireExceptionInfo();

		try
		{
			if (readSettings != nullptr)
			{
				Throw::IfFalse("readSettings", readSettings->PixelStorage == nullptr,
					"PixelStorage is not supported for images with multiple frames/layers.");

				if (!readSettings->Ping)
					readSettings->Apply(imageInfo);
			}

			std::string imageSpec;
			Marshaller::Marshal(filePath, imageSpec);

			MagickCore::CopyMagickString(imageInfo->filename, imageSpec.c_str(), MaxTextExtent - 1);

			MagickCore::Image* images;
			if (readSettings != nullptr && readSettings->Ping)
				images = MagickCore::PingImage(imageInfo, exceptionInfo);
			else
				images = MagickCore::ReadImage(imageInfo, exceptionInfo);
			Magick::insertImages(imageList, images);
			Magick::throwException(exceptionInfo);
			MagickCore::DestroyExceptionInfo(exceptionInfo);

			return nullptr;
		}
		catch (Magick::Exception& exception)
		{
			return MagickException::Create(exception);
		}
		finally
		{
			MagickCore::DestroyImageInfo(imageInfo);
		}
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

		return ReadUnChecked(fileName);
	}
	//==============================================================================================
}