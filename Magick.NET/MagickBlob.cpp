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
#include "MagickBlob.h"
#include "Helpers\MagickReader.h"

using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	MagickBlob^ MagickBlob::Create()
	{
		MagickBlob^ blob = gcnew MagickBlob();
		blob->Value = new Magick::Blob();
		return blob;
	}
	//==============================================================================================
	MagickBlob::MagickBlob(Magick::Blob& blob)
	{
		Value = new Magick::Blob(blob);
	}
	//==============================================================================================
	MagickBlob::MagickBlob(array<Byte>^ data)
	{
		Value = new Magick::Blob();
		MagickReader::Read(Value, data);
	}
	//==============================================================================================
	MagickBlob^ MagickBlob::Read(String^ fileName)
	{
		MagickBlob^ blob = gcnew MagickBlob();
		MagickReader::Read(blob->Value, fileName);
		return blob;
	}
	//==============================================================================================
	MagickBlob^ MagickBlob::Read(Stream^ stream)
	{
		MagickBlob^ blob = gcnew MagickBlob();
		MagickReader::Read(blob->Value, stream);
		return blob;
	}
	//==============================================================================================
	array<Byte>^ MagickBlob::ToByteArray()
	{
		array<Byte>^ data = gcnew array<Byte>(Value->length());
		MagickWriter::Write(Value, data);
		return data;
	}
	//==============================================================================================
	void MagickBlob::Write(String^ fileName)
	{
		MagickWriter::Write(Value, fileName);
	}
	//==============================================================================================
	void MagickBlob::Write(Stream^ stream)
	{
		MagickWriter::Write(Value, stream);
	}
	//==============================================================================================
}