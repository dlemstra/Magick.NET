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
#include "ExifValue.h"

using namespace System::Globalization;
using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	String^ ExifValue::ToString(Object^ value)
	{
		switch(_DataType)
		{
		case ExifDataType::Byte:
			return ((Byte)value).ToString("X2", CultureInfo::InvariantCulture);
		case ExifDataType::Ascii:
			return (String^)value;
		case ExifDataType::Short:
			return ((unsigned short)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Long:
			return ((unsigned int)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Rational:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedByte:
			return ((SByte)value).ToString("X2", CultureInfo::InvariantCulture);
		case ExifDataType::Undefined:
			return ((Byte)value).ToString("X2", CultureInfo::InvariantCulture);
		case ExifDataType::SignedShort:
			return ((short)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedLong:
			return ((int)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedRational:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SingleFloat:
			return ((float)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::DoubleFloat:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	ExifValue::ExifValue(ExifTag tag, ExifDataType dataType, Object^ value, bool isArray)
	{
		_Tag = tag;
		_DataType = dataType;
		_Value = value;
		_IsArray = isArray;

		if (_DataType == ExifDataType::Ascii)
			_IsArray = false;
	}
	//==============================================================================================
	bool ExifValue::operator == (ExifValue^ left, ExifValue^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool ExifValue::operator != (ExifValue^ left, ExifValue^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool ExifValue::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<ExifValue^>(obj));
	}
	//==============================================================================================
	bool ExifValue::Equals(ExifValue^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return
			_Tag == other->_Tag &&
			_DataType == other->_DataType &&
			Object::Equals(_Value, other->_Value);
	}
	//==============================================================================================
	ExifDataType ExifValue::DataType::get()
	{
		return _DataType;
	}
	//==============================================================================================
	bool ExifValue::IsArray::get()
	{
		return _IsArray;
	}
	//==============================================================================================
	ExifTag ExifValue::Tag::get()
	{
		return _Tag;
	}
	//==============================================================================================
	Object^ ExifValue::Value::get()
	{
		return _Value;
	}
	//==============================================================================================
	int ExifValue::GetHashCode()
	{
		int hashCode = _Tag.GetHashCode() ^ _DataType.GetHashCode();
		return _Value != nullptr ? hashCode ^ _Value->GetHashCode() : hashCode;
	}
	//==============================================================================================
	String^ ExifValue::ToString()
	{
		if (_Value == nullptr)
			return nullptr;

		if (!_IsArray)
			return ToString(_Value);

		StringBuilder^ sb = gcnew StringBuilder();
		for each(Object^ value in (Array^)_Value)
		{
			sb->Append(ToString(value));
			sb->Append(" ");
		}

		return sb->ToString();
	}
	//==============================================================================================
}