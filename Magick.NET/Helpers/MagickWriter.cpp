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
#include "MagickWriter.h"
#include "FileHelper.h"

using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	//==============================================================================================
	void MagickWriter::WriteUnchecked(Magick::Blob* blob, Stream^ stream)
	{
		int length = (int)blob->length();
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
	//==============================================================================================
	void MagickWriter::Write(Magick::Image* image, Magick::Blob* blob)
	{
		try
		{
			image->write(blob);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickWriter::Write(Magick::Image* image, String^ fileName)
	{
		Throw::IfNullOrEmpty("fileName", fileName);
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);

		std::string imageSpec;
		Marshaller::Marshal(filePath, imageSpec);

		try
		{
			image->write(imageSpec);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickWriter::Write(Magick::Image* image, Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		Magick::Blob blob;
		Write(image, &blob);
		WriteUnchecked(&blob, stream);
	}
	//==============================================================================================
	void MagickWriter::Write(std::list<Magick::Image>* imageList, Magick::Blob* blob)
	{
		try
		{
			Magick::writeImages(imageList->begin(), imageList->end(), blob, true);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickWriter::Write(std::list<Magick::Image>* imageList, String^ fileName)
	{
		Throw::IfNullOrEmpty("fileName", fileName);
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);

		std::string imageSpec;
		Marshaller::Marshal(filePath, imageSpec);

		try
		{
			Magick::writeImages(imageList->begin(), imageList->end(), imageSpec, true);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickWriter::Write(std::list<Magick::Image>* imageList, Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		Magick::Blob blob;
		Write(imageList, &blob);
		WriteUnchecked(&blob, stream);
	}
	//==============================================================================================
}