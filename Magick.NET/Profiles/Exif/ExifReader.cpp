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
#include "..\..\Helpers\EnumHelper.h"
#include "ExifReader.h"

using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	void ExifReader::AddValues(List<ExifValue^>^ values, unsigned int index)
	{
		_Index =  _StartIndex +index;
		unsigned short count = GetUInt16();

		for (unsigned short i = 0; i < count; i++)
		{
			ExifValue^ value = CreateValue();

			if (value->Tag == ExifTag::SubIFDOffset)
				_SubIFDoffset = (unsigned int)value->Value;
			else if (value->Tag == ExifTag::GPSIFDOffset)
				_GPSIFDoffset =  (unsigned int)value->Value;
			else
				values->Add(value);
		}
	}
	//==============================================================================================
	Object^ ExifReader::ConvertValue(ExifDataType dataType, array<Byte>^ data, int numberOfComponents)
	{
		switch (dataType)
		{
		case ExifDataType::Unknown:
			return nullptr;
		case ExifDataType::Ascii:
			return ToString(data);
		case ExifDataType::Byte:
			if (numberOfComponents == 1)
				return ToByte(data);
			else 
				return data;
		case ExifDataType::Short:
			if (numberOfComponents == 1)
				return ToUInt16(data);
			else
				return ToArray<unsigned short>(dataType, data, gcnew ConverterMethod<unsigned short>(this, &ExifReader::ToUInt16));
		case ExifDataType::Long:
			if (numberOfComponents == 1)
				return ToUInt32(data);
			else
				return ToArray<unsigned int>(dataType, data, gcnew ConverterMethod<unsigned int>(this, &ExifReader::ToUInt32));
		case ExifDataType::Rational:
			if (numberOfComponents == 1)
				return ToURational(data);
			else
				return ToArray<double>(dataType, data, gcnew ConverterMethod<double>(this, &ExifReader::ToURational));
		case ExifDataType::SignedByte:
			if (numberOfComponents == 1)
				return ToSByte(data);
			else
				return ToArray<SByte>(dataType, data, gcnew ConverterMethod<SByte>(this, &ExifReader::ToSByte));
		case ExifDataType::Undefined:
			if (numberOfComponents == 1)
				return ToByte(data);
			else
				return data;
		case ExifDataType::SignedShort:
			if (numberOfComponents == 1)
				return ToInt16(data);
			else
				return ToArray<short>(dataType, data, gcnew ConverterMethod<short>(this, &ExifReader::ToInt16));
		case ExifDataType::SignedLong:
			if (numberOfComponents == 1)
				return ToInt32(data);
			else
				return ToArray<int>(dataType, data, gcnew ConverterMethod<int>(this, &ExifReader::ToInt32));
		case ExifDataType::SignedRational:
			if (numberOfComponents == 1)
				return ToRational(data);
			else
				return ToArray<double>(dataType, data, gcnew ConverterMethod<double>(this, &ExifReader::ToRational));
		case ExifDataType::SingleFloat:
			if (numberOfComponents == 1)
				return ToSingle(data);
			else
				return ToArray<float>(dataType, data, gcnew ConverterMethod<float>(this, &ExifReader::ToSingle));
		case ExifDataType::DoubleFloat:
			if (numberOfComponents == 1)
				return ToDouble(data);
			else
				return ToArray<double>(dataType, data, gcnew ConverterMethod<double>(this, &ExifReader::ToDouble));
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	ExifValue^ ExifReader::CreateValue()
	{
		ExifTag tag = EnumHelper::Parse(GetUInt16(), ExifTag::Unknown);
		ExifDataType dataType = EnumHelper::Parse(GetUInt16(), ExifDataType::Unknown);
		Object^ value = nullptr;

		if (dataType == ExifDataType::Unknown)
			return gcnew ExifValue(tag, dataType, value, false);

		unsigned int numberOfComponents = GetUInt32();

		int size = (int)(numberOfComponents*GetSize(dataType));
		array<Byte>^ data = GetBytes(4);

		if (size > 4)
		{			
			int oldIndex = _Index;
			_Index = ToUInt16(data) + _StartIndex;
			value = ConvertValue(dataType, GetBytes(size), numberOfComponents);
			_Index = oldIndex;
		}
		else
		{
			value = ConvertValue(dataType, data, numberOfComponents);
		}

		return gcnew ExifValue(tag, dataType, value, numberOfComponents > 1);
	}
	//==============================================================================================
	array<Byte>^ ExifReader::GetBytes(int length)
	{
		array<Byte>^ data = gcnew array<Byte>(length);
		Array::Copy(_Data, _Index, data, 0, length);
		_Index += length;

		return data;
	}
	//==============================================================================================
	int ExifReader::GetSize(ExifDataType dataType)
	{
		switch (dataType)
		{
		case ExifDataType::Byte:
		case ExifDataType::Ascii:
		case ExifDataType::SignedByte:
		case ExifDataType::Undefined:
			return 1;
		case ExifDataType::Short:
		case ExifDataType::SignedShort:
			return 2;
		case ExifDataType::Long:
		case ExifDataType::SignedLong:
		case ExifDataType::SingleFloat:
			return 4;
		case ExifDataType::Rational:
		case ExifDataType::SignedRational:
		case ExifDataType::DoubleFloat:
			return 8;
		default:
			throw gcnew NotImplementedException(dataType.ToString());
		}
	}
	//==============================================================================================
	String^ ExifReader::GetString(int length)
	{
		return ToString(GetBytes(length));
	}
	//==============================================================================================
	void ExifReader::GetThumbnail()
	{
		unsigned int ifd1Offset = GetUInt32();

		List<ExifValue^>^ values = gcnew List<ExifValue^>();
		AddValues(values, ifd1Offset);

		for each(ExifValue^ value in values)
		{
			if (value->Tag == ExifTag::JPEGInterchangeFormat)
				_ThumbnailOffset = (unsigned int)value->Value + _StartIndex;
			else if (value->Tag == ExifTag::JPEGInterchangeFormatLength)
				_ThumbnailLength = (unsigned int)value->Value;
		}
	}
	//==============================================================================================
	unsigned short ExifReader::GetUInt16()
	{
		return ToUInt16(GetBytes(2));
	}
	//==============================================================================================
	unsigned int ExifReader::GetUInt32()
	{
		return ToUInt32(GetBytes(4));
	}
	//==============================================================================================
	generic<typename TDataType>
	where TDataType : value class
		array<TDataType>^ ExifReader::ToArray(ExifDataType dataType, array<Byte>^ data, ConverterMethod<TDataType>^ converter)
	{
		int dataTypeSize =  GetSize(dataType);
		int length = data->Length / dataTypeSize;

		array<TDataType>^ result = gcnew array<TDataType>(length);
		array<Byte>^ buffer = gcnew array<Byte>(dataTypeSize);

		for (int i = 0; i < length; i++)
		{
			Array::Copy(data, i * dataTypeSize, buffer, 0, dataTypeSize);

			result->SetValue(converter(buffer), i);
		}

		return result;
	}
	//==============================================================================================
	Byte ExifReader::ToByte(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return data[0];
	}
	//==============================================================================================
	double ExifReader::ToDouble(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToDouble(data, 0);
	}
	//==============================================================================================
	short ExifReader::ToInt16(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToInt16(data, 0);
	}
	//==============================================================================================
	int ExifReader::ToInt32(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToInt32(data, 0);
	}
	//==============================================================================================
	double ExifReader::ToRational(array<Byte>^ data)
	{
		array<Byte>^ numeratorData = gcnew array<Byte>(4);
		array<Byte>^ denominatorData = gcnew array<Byte>(4);

		Array::Copy(data, numeratorData, 4);
		Array::Copy(data, 4, denominatorData, 0, 4);

		unsigned int numerator = ToInt32(numeratorData);
		unsigned int denominator = ToInt32(denominatorData);

		return numerator/(double) denominator;
	}
	//==============================================================================================
	SByte ExifReader::ToSByte(array<Byte>^ data)
	{
		return (SByte) (data[0] - Byte::MaxValue);
	}
	//==============================================================================================
	float ExifReader::ToSingle(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToSingle(data, 0);
	}
	//==============================================================================================
	String^ ExifReader::ToString(array<Byte>^ data)
	{		
		String^ result = Encoding::UTF8->GetString(data, 0, data->Length);
		int nullCharIndex = result->IndexOf('\0');
		if (nullCharIndex != -1)
			result = result->Substring(0, nullCharIndex);

		return result;
	}
	//==============================================================================================
	unsigned short ExifReader::ToUInt16(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToUInt16(data, 0);
	}
	//==============================================================================================
	unsigned int ExifReader::ToUInt32(array<Byte>^ data)
	{
		if (_IsLittleEndian != BitConverter::IsLittleEndian)
			Array::Reverse(data);

		return BitConverter::ToUInt32(data, 0);
	}
	//==============================================================================================
	double ExifReader::ToURational(array<Byte>^ data)
	{
		array<Byte>^ numeratorData = gcnew array<Byte>(4);
		array<Byte>^ denominatorData = gcnew array<Byte>(4);

		Array::Copy(data, numeratorData, 4);
		Array::Copy(data, 4, denominatorData, 0, 4);

		unsigned int numerator = ToUInt32(numeratorData);
		unsigned int denominator = ToUInt32(denominatorData);

		return numerator/(double) denominator;
	}
	//==============================================================================================
	unsigned int ExifReader::ThumbnailLength::get()
	{
		return _ThumbnailLength;
	}
	//==============================================================================================
	unsigned int ExifReader::ThumbnailOffset::get()
	{
		return _ThumbnailOffset;
	}
	//==============================================================================================
	List<ExifValue^>^ ExifReader::Read(array<Byte>^ data)
	{
		List<ExifValue^>^ result = gcnew List<ExifValue^>();

		_Data = data;

		if (GetString(4) == "Exif")
		{
			if (GetUInt16() != 0)
				return result;

			_StartIndex = 6;
		}
		else
		{
			_Index = 0;
		}

		_IsLittleEndian = GetString(2) == "II";

		if (GetUInt16() != 0x002A)
			return result;

		AddValues(result, GetUInt32());

		GetThumbnail();

		if (_SubIFDoffset == 0)
			return result;

		AddValues(result, _SubIFDoffset);

		if (_GPSIFDoffset == 0)
			return result;

		AddValues(result, _GPSIFDoffset);

		return result;
	}
	//==============================================================================================
}