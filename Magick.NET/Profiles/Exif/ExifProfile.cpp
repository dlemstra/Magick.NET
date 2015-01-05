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
#include "ExifProfile.h"
#include "ExifReader.h"
#include "ExifWriter.h"
#include "..\..\IO\MagickReader.h"
#include "..\..\Helpers\FileHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	void ExifProfile::Initialize()
	{
		if (_Values != nullptr)
			return;

		_Parts = ExifParts::All;

		if (Data == nullptr)
		{
			_Values = gcnew List<ExifValue^>();
			return;
		}

		ExifReader^ reader = gcnew ExifReader();
		_Values = reader->Read(Data);
		_ThumbnailOffset = reader->ThumbnailOffset;
		_ThumbnailLength = reader->ThumbnailLength;
	}
	//==============================================================================================
	array<Byte>^ ExifProfile::GetData()
	{
		if (_Values == nullptr || _Values->Count == 0)
			return nullptr;

		ExifWriter^ writer = gcnew ExifWriter(_Values, _Parts);
		return writer->GetData();
	}
	//==============================================================================================
	ExifParts ExifProfile::Parts::get()
	{
		return _Parts;
	}
	//==============================================================================================
	void ExifProfile::Parts::set(ExifParts value)
	{
		_Parts = value;
	}
	//==============================================================================================
	IEnumerable<ExifValue^>^ ExifProfile::Values::get()
	{
		Initialize();
		return _Values;
	}
	//==============================================================================================
	MagickImage^ ExifProfile::CreateThumbnail()
	{
		Initialize();

		if (_ThumbnailOffset == 0 || _ThumbnailLength == 0)
			return nullptr;

		array<Byte>^ data = gcnew array<Byte>(_ThumbnailLength);
		Array::Copy(Data, _ThumbnailOffset, data, 0, _ThumbnailLength);
		return gcnew MagickImage(data);
	}
	//==============================================================================================
	void ExifProfile::SetValue(ExifTag tag, Object^ value)
	{
		for each (ExifValue^ exifValue in Values)
		{
			if (exifValue->Tag == tag)
			{
				exifValue->Value = value;
				return;
			}
		}

		ExifValue^ exifValue = ExifValue::Create(tag, value);
		_Values->Add(exifValue);
	}
	//==============================================================================================
}