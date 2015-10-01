//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
#include "..\Helpers\ExceptionHelper.h"

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		int MagickReader::GetExpectedLength(MagickReaderSettings^ settings)
		{
			int length = settings->Width.Value * settings->Height.Value * settings->PixelStorage->Mapping->Length;
			switch (settings->PixelStorage->StorageType)
			{
			case StorageType::Char:
				return length;
			case StorageType::Double:
				return length * sizeof(double);
			case  StorageType::Float:
				return length * sizeof(float);
			case StorageType::Long:
				return length * sizeof(int);
			case StorageType::LongLong:
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
		//===========================================================================================
		void MagickReader::ReadPixels(Magick::Image* image, MagickReaderSettings^ settings,
			array<Byte>^ pixels)
		{
			Throw::IfTrue("settings", settings->PixelStorage->StorageType == StorageType::Undefined, "Storage type should not be undefined.");
			Throw::IfNull("settings", settings->Width, "Width should be defined when pixel storage is set.");
			Throw::IfNull("settings", settings->Height, "Height should be defined when pixel storage is set.");
			Throw::IfNullOrEmpty("settings", settings->PixelStorage->Mapping, "Pixel storage mapping should be defined.");

			int length = GetExpectedLength(settings);
			Throw::IfTrue("pixels", pixels->Length != length, "The array length is " + pixels->Length + " but should be " + length + ".");

			void* convertedPixels = NULL;
			try
			{
				convertedPixels = Marshaller::Marshal(pixels, settings->PixelStorage->StorageType);

				std::string map;
				Marshaller::Marshal(settings->PixelStorage->Mapping, map);

				Magick::Geometry size = Magick::Geometry();
				image->read(settings->Width.Value, settings->Height.Value, map,
					(MagickCore::StorageType)settings->PixelStorage->StorageType, convertedPixels);
			}
			finally
			{
				delete[] convertedPixels;
			}
		}
		//===========================================================================================
		array<Byte>^ MagickReader::ReadUnchecked(String^ filePath)
		{
			FileStream^ stream = File::OpenRead(filePath);
			array<Byte>^ result = Read(stream);
			delete stream;

			return result;
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(Magick::Image* image, array<Byte>^ bytes,
			MagickReaderSettings^ readSettings)
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
				return ExceptionHelper::Create(exception);
			}
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(Magick::Image* image, MagickColor^ color, int width, int height)
		{
			Throw::IfNull("color", color);

			const Magick::Geometry* geometry = new Magick::Geometry(width, height);
			const Magick::Color* background = color->CreateColor();
			try
			{
				image->read(*geometry, "xc:" + (std::string)*background);

				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
			finally
			{
				delete geometry;
				delete background;
			}
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(Magick::Image* image, Stream^ stream,
			MagickReaderSettings^ readSettings)
		{
			return Read(image, Read(stream), readSettings);
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(Magick::Image* image, String^ fileName, int width, int height)
		{
			Throw::IfInvalidFileName(fileName);

			const Magick::Geometry* geometry = new Magick::Geometry(width, height);
			try
			{
				std::string imageSpec;
				Marshaller::Marshal(fileName, imageSpec);

				image->read(*geometry, imageSpec);

				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(Magick::Image* image, String^ fileName,
			MagickReaderSettings^ readSettings)
		{
			Throw::IfInvalidFileName(fileName);

			unsigned char *pixels = NULL;

			try
			{
				std::string imageSpec;
				Marshaller::Marshal(fileName, imageSpec);

				if (readSettings != nullptr)
				{
					if (readSettings->Ping)
					{
						image->ping(imageSpec);
						return nullptr;
					}
					else if (readSettings->PixelStorage != nullptr)
					{
						array<Byte>^ bytes = ReadUnchecked(fileName);
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
				return ExceptionHelper::Create(exception);
			}
			finally
			{
				if (pixels != NULL)
					delete[] pixels;
			}
		}
		//===========================================================================================
		MagickException^ MagickReader::Read(std::vector<Magick::Image>* imageList, array<Byte>^ bytes,
			MagickReaderSettings^ readSettings)
		{
			Throw::IfNull("readSettings", readSettings);
			Throw::IfNullOrEmpty("bytes", bytes);

			MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(NULL);
			MagickCore::ExceptionInfo *exceptionInfo = MagickCore::AcquireExceptionInfo();
			unsigned char* data = NULL;

			try
			{
				Throw::IfTrue("readSettings", readSettings->PixelStorage != nullptr,
					"PixelStorage is not supported for images with multiple frames/layers.");

				if (!readSettings->Ping)
					readSettings->Apply(imageInfo);

				data = Marshaller::Marshal(bytes);
				MagickCore::Image *images;

				if (readSettings->Ping)
					images = MagickCore::PingBlob(imageInfo, data, bytes->Length, exceptionInfo);
				else
					images = MagickCore::BlobToImage(imageInfo, data, bytes->Length, exceptionInfo);

				Magick::insertImages(imageList, images);
				Magick::throwException(exceptionInfo, readSettings->IgnoreWarnings);
				MagickCore::DestroyExceptionInfo(exceptionInfo);

				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
			finally
			{
				MagickCore::DestroyImageInfo(imageInfo);
				if (data != NULL)
					delete[] data;
			}
		}
		//==============================================================================================
		MagickException^ MagickReader::Read(std::vector<Magick::Image>* imageList, Stream^ stream,
			MagickReaderSettings^ readSettings)
		{
			return Read(imageList, Read(stream), readSettings);
		}	
		//==============================================================================================
		MagickException^ MagickReader::Read(std::vector<Magick::Image>* imageList, String^ fileName,
			MagickReaderSettings^ readSettings)
		{
			Throw::IfNull("readSettings", readSettings);
			Throw::IfInvalidFileName(fileName);

			MagickCore::ImageInfo *imageInfo = MagickCore::CloneImageInfo(NULL);
			MagickCore::ExceptionInfo *exceptionInfo = MagickCore::AcquireExceptionInfo();

			try
			{
				Throw::IfTrue("readSettings", readSettings->PixelStorage != nullptr,
					"PixelStorage is not supported for images with multiple frames/layers.");

				if (!readSettings->Ping)
					readSettings->Apply(imageInfo);

				std::string imageSpec;
				Marshaller::Marshal(fileName, imageSpec);

				MagickCore::CopyMagickString(imageInfo->filename, imageSpec.c_str(), MagickPathExtent - 1);

				MagickCore::Image* images;
				if (readSettings->Ping)
					images = MagickCore::PingImage(imageInfo, exceptionInfo);
				else
					images = MagickCore::ReadImage(imageInfo, exceptionInfo);
				Magick::insertImages(imageList, images);
				Magick::throwException(exceptionInfo, readSettings->IgnoreWarnings);
				MagickCore::DestroyExceptionInfo(exceptionInfo);

				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
			finally
			{
				MagickCore::DestroyImageInfo(imageInfo);
			}
		}
		//===========================================================================================
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
		//===========================================================================================
		array<Byte>^ MagickReader::Read(String^ fileName)
		{
			Throw::IfInvalidFileName(fileName);

			return ReadUnchecked(fileName);
		}
		//===========================================================================================
	}
}