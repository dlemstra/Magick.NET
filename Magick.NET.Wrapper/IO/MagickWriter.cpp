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
#include "MagickWriter.h"
#include "..\Helpers\ExceptionHelper.h"

using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		void MagickWriter::WriteUnchecked(Magick::Blob* blob, Stream^ stream)
		{
			int length = (int)blob->length();
			if (length == 0)
				return;

			int bufferSize = Math::Min(length, 8192);
			array<Byte>^ buffer = gcnew array<Byte>(bufferSize);

			int offset = 0;
			IntPtr ptr = IntPtr((void*)blob->data());
			while(offset < length)
			{
				int count = (offset + bufferSize > length) ? length - offset : bufferSize;

				Marshal::Copy(ptr, buffer, 0, count);

				stream->Write(buffer, 0, count);

				offset += bufferSize;
				ptr = IntPtr(ptr.ToInt64() + count);
			}
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(Magick::Image* image, Magick::Blob* blob)
		{
			try
			{
				image->write(blob);
				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(Magick::Image* image, String^ fileName)
		{
			Throw::IfNullOrEmpty("fileName", fileName);

			std::string imageSpec;
			Marshaller::Marshal(fileName, imageSpec);

			try
			{
				image->write(imageSpec);
				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(Magick::Image* image, Stream^ stream)
		{
			Throw::IfNull("stream", stream);

			Magick::Blob blob;
			MagickException^ exception = Write(image, &blob);
			WriteUnchecked(&blob, stream);
			return exception;
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(std::vector<Magick::Image>* imageList, Magick::Blob* blob)
		{
			try
			{
				Magick::writeImages(imageList->begin(), imageList->end(), blob, true);
				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(std::vector<Magick::Image>* imageList, Stream^ stream)
		{
			Throw::IfNull("stream", stream);

			Magick::Blob blob;
			MagickException^ exception = Write(imageList, &blob);
			WriteUnchecked(&blob, stream);
			return exception;
		}
		//===========================================================================================
		MagickException^ MagickWriter::Write(std::vector<Magick::Image>* imageList, String^ fileName)
		{
			Throw::IfNullOrEmpty("fileName", fileName);

			std::string imageSpec;
			Marshaller::Marshal(fileName, imageSpec);

			try
			{
				Magick::writeImages(imageList->begin(), imageList->end(), imageSpec, true);
				return nullptr;
			}
			catch (Magick::Exception& exception)
			{
				return ExceptionHelper::Create(exception);
			}
		}
		//===========================================================================================
	}
}