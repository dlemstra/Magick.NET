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
#include "ExifWriter.h"

using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	int ExifWriter::GetIndex(List<int>^ indexes, ExifTag tag)
	{
		for each (int index in indexes)
		{
			if (_Values[index]->Tag == tag)
				return index;
		}

		int index = _Values->Count;
		indexes->Add(index);
		_Values->Add(ExifValue::Create(tag, nullptr));
		return index;
	}
	//==============================================================================================
	List<int>^ ExifWriter::GetIndexes(ExifParts part, array<ExifTag>^ tags)
	{
		if (((int)_AllowedParts & (int)part) == 0)
			return gcnew List<int>(0);

		List<int>^ result = gcnew List<int>();
		for (int i=0; i < _Values->Count; i++)
		{
			ExifValue^ value = _Values[i];

			if (!value->HasValue)
				continue;

			int index = Array::IndexOf(tags, value->Tag);
			if (index > -1)
				result->Add(i);
		}

		return result;
	}
	//==============================================================================================
	int ExifWriter::GetLength(IEnumerable<int>^ indexes)
	{
		int length = 0;

		for each (int index in indexes)
		{
			int valueLength = _Values[index]->Length;

			if (valueLength > 4)
				length += 12 + valueLength;
			else
				length += 12;
		}

		return length;
	}
	//==============================================================================================
	template<typename TDataType>
	int ExifWriter::Write(TDataType value, array<Byte>^ destination, int offset)
	{
		array<Byte>^ bytes = BitConverter::GetBytes(value);
		Buffer::BlockCopy(bytes, 0, destination, offset, bytes->Length);

		return offset + bytes->Length;
	}
	//==============================================================================================
	int ExifWriter::WriteArray(ExifValue^ value, array<Byte>^ destination, int offset)
	{
		if (value->DataType == ExifDataType::Ascii)
			return WriteValue(ExifDataType::Ascii, value->Value, destination, offset);

		int newOffset = offset;
		for each(Object^ obj in (Array^)value->Value)
			newOffset = WriteValue(value->DataType, obj, destination, newOffset);

		return newOffset;
	}
	//==============================================================================================
	int ExifWriter::WriteData(List<int>^ indexes, array<Byte>^ destination, int offset)
	{
		if (_DataOffsets->Count == 0)
			return offset;

		int newOffset = offset;

		int i = 0;
		for each(int index in indexes)
		{
			ExifValue^ value = _Values[index];
			if (value->Length > 4)
			{
				Write(newOffset - _StartIndex, destination, _DataOffsets[i++]);
				newOffset = WriteValue(value, destination, newOffset);
			}
		}

		return newOffset;
	}
	//==============================================================================================
	int ExifWriter::WriteHeaders(List<int>^ indexes, array<Byte>^ destination, int offset)
	{
		_DataOffsets = gcnew List<int>();

		int newOffset = Write((unsigned short)indexes->Count, destination, offset);

		if (indexes->Count == 0)
			return newOffset;

		for each(int index in indexes)
		{
			ExifValue^ value = _Values[index];
			newOffset = Write((unsigned short)value->Tag, destination, newOffset);
			newOffset = Write((unsigned short)value->DataType, destination, newOffset);
			newOffset = Write((unsigned int)value->NumberOfComponents, destination, newOffset);

			if (value->Length > 4)
				_DataOffsets->Add(newOffset);
			else
				WriteValue(value, destination, newOffset);

			newOffset += 4;
		}

		return newOffset;
	}
	//==============================================================================================
	int ExifWriter::WriteRational(double value, array<Byte>^ destination, int offset)
	{
		int numerator = 1;
		int denominator = 1;

		double val = abs(value);
		double df = numerator / denominator;

		while (abs(df - val) > .000001)
		{
			if (df < val)
			{
				numerator++;
			}
			else
			{
				denominator++;
				numerator = (int)(val * denominator);
			}

			df = numerator / (double)denominator;
		}

		Write((numerator) * (value < 0 ? -1 : 1), destination, offset);
		Write(denominator, destination, offset + 4);

		return offset + 8;
	}
	//==============================================================================================
	int ExifWriter::WriteURational(double value, array<Byte>^ destination, int offset)
	{
		unsigned int numerator = 1;
		unsigned int denominator = 1;

		double val = abs(value);
		double df = numerator / denominator;

		while (abs(df - val) > .000001)
		{
			if (df < val)
			{
				numerator++;
			}
			else
			{
				denominator++;
				numerator = (unsigned int)(val * denominator);
			}

			df = numerator / (double)denominator;
		}

		Write(numerator, destination, offset);
		Write(denominator, destination, offset + 4);

		return offset + 8;
	}
	//==============================================================================================
	int ExifWriter::WriteValue(ExifDataType dataType, Object^ value, array<Byte>^ destination, int offset)
	{
		switch(dataType)
		{
		case ExifDataType::Ascii:
			{
				array<Byte>^ bytes = Encoding::UTF8->GetBytes((String^) value);
				Buffer::BlockCopy(bytes, 0, destination, offset, bytes->Length);
				return offset + bytes->Length;
			}
		case ExifDataType::Byte:
		case ExifDataType::Undefined:
			destination[offset] = (Byte)value;
			return offset + 1;
		case ExifDataType::DoubleFloat:
			return Write((double)value, destination, offset);
		case ExifDataType::Short:
			return Write((unsigned short)value, destination, offset);
		case ExifDataType::Long:
			return Write((unsigned int)value, destination, offset);
		case ExifDataType::Rational:
			return WriteURational((double)value, destination, offset);
		case ExifDataType::SignedByte:
			destination[offset] = (SByte)value;
			return offset + 1;
		case ExifDataType::SignedLong:
			return Write((int)value, destination, offset);
		case ExifDataType::SignedShort:
			return Write((short)value, destination, offset);
		case ExifDataType::SignedRational:
			return WriteRational((double)value, destination, offset);
		case ExifDataType::SingleFloat:
			return Write((Single)value, destination, offset);
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	int ExifWriter::WriteValue(ExifValue^ value, array<Byte>^ destination, int offset)
	{
		if (value->IsArray && value->DataType != ExifDataType::Ascii)
			return WriteArray(value, destination, offset);
		else
			return WriteValue(value->DataType, value->Value, destination, offset);
	}
	//==============================================================================================
	ExifWriter::ExifWriter(List<ExifValue^>^ values, ExifParts allowedParts)
	{
		_Values = values;
		_AllowedParts = allowedParts;

		_IfdIndexes = GetIndexes(ExifParts::IfdTags, _IfdTags);
		_ExifIndexes = GetIndexes(ExifParts::ExifTags, _ExifTags);
		_GPSIndexes = GetIndexes(ExifParts::GPSTags, _GPSTags);
	}
	//==============================================================================================
	array<Byte>^ ExifWriter::GetData()
	{
		unsigned int length = 0;
		int exifIndex = -1;
		int gpsIndex = -1;

		if (_ExifIndexes->Count > 0)
			exifIndex = (int)GetIndex(_IfdIndexes, ExifTag::SubIFDOffset);

		if (_GPSIndexes->Count > 0)
			gpsIndex = (int)GetIndex(_IfdIndexes, ExifTag::GPSIFDOffset);

		unsigned int ifdLength = 2 + GetLength(_IfdIndexes) + 4;
		unsigned int exifLength = GetLength(_ExifIndexes);
		unsigned int gpsLength = GetLength(_GPSIndexes);

		if (exifLength > 0)
			exifLength += 2;

		if (gpsLength > 0)
			gpsLength += 2;

		length = ifdLength + exifLength + gpsLength;

		if (length == 6)
			return nullptr;

		length += 10 + 4 + 2;

		array<Byte>^ result = gcnew array<Byte>(length);
		result[0] = 'E';
		result[1] = 'x';
		result[2] = 'i';
		result[3] = 'f';
		result[4] = 0x00;
		result[5] = 0x00;
		result[6] = 'I';
		result[7] = 'I';
		result[8] = 0x2A;
		result[9] = 0x00;

		unsigned int i = 10;
		unsigned int ifdOffset = (i - _StartIndex) + 4;
		unsigned int thumbnailOffset = ifdOffset + ifdLength + exifLength + gpsLength;

		if (exifLength > 0)
			_Values[exifIndex]->Value = (unsigned int)(ifdOffset + ifdLength);

		if (gpsLength > 0)
			_Values[gpsIndex]->Value = (unsigned int)(ifdOffset + ifdLength + exifLength);

		i = Write(ifdOffset, result, i);
		i = WriteHeaders(_IfdIndexes, result, i);
		i = Write(thumbnailOffset, result, i);
		i = WriteData(_IfdIndexes, result, i);

		if (exifLength > 0)
		{
			i = WriteHeaders(_ExifIndexes, result, i);
			i = WriteData(_ExifIndexes, result, i);
		}

		if (gpsLength > 0)
		{
			i = WriteHeaders(_GPSIndexes, result, i);
			i = WriteData(_GPSIndexes, result, i);
		}

		Write((unsigned short)0, result, i);

		return result;
	}
	//==============================================================================================
}