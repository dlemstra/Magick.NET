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

using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	void MagickBlob::Initialize(Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		array<Byte>^ data = gcnew array<Byte>((int)stream->Length);
		stream->Read(data, 0, (int)stream->Length);
		Value = Marshaller::Marshal(data);
	}
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
		Throw::IfNull("data", data);

		Value = Marshaller::Marshal(data);
	}
	//==============================================================================================
	MagickBlob^ MagickBlob::Read(String^ fileName)
	{
		Throw::IfInvalidFileName(fileName);

		FileStream^ stream = File::OpenRead(fileName);
		MagickBlob^ blob = gcnew MagickBlob();
		blob->Initialize(stream);
		delete stream;

		return blob;
	}
	//==============================================================================================
	MagickBlob^ MagickBlob::Read(Stream^ stream)
	{
		MagickBlob^ blob = gcnew MagickBlob();
		blob->Initialize(stream);
		return blob;
	}	
	//==============================================================================================
	void MagickBlob::Write(String^ fileName)
	{
		Throw::IfNullOrEmpty("fileName", fileName);

		FileStream^ stream = File::OpenWrite(fileName);
		Write(stream);
		stream->Close();
	}
	//==============================================================================================
	void MagickBlob::Write(Stream^ stream)
	{
		MagickWriter::Write(Value, stream);
	}
	//==============================================================================================
}