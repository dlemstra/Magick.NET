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
#include "EightBimValue.h"

using namespace System::Globalization;
using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	EightBimValue::EightBimValue(short ID, array<Byte>^ data)
	{
		_ID = ID;
		_Data = data;
	}
	//==============================================================================================
	short EightBimValue::ID::get()
	{
		return _ID;
	}
	//==============================================================================================
	bool EightBimValue::operator == (EightBimValue^ left, EightBimValue^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool EightBimValue::operator != (EightBimValue^ left, EightBimValue^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool EightBimValue::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<EightBimValue^>(obj));
	}
	//==============================================================================================
	bool EightBimValue::Equals(EightBimValue^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		if (_ID != other->_ID)
			return false;

		if (_Data->Length != other->_Data->Length)
			return false;

		for (int i=0; i<_Data->Length; i++)
		{
			if (_Data[i] != other->_Data[i])
				return false;
		}

		return true;
	}
	//==============================================================================================
	int EightBimValue::GetHashCode()
	{
		return
			_Data->GetHashCode() ^
			_ID.GetHashCode();
	}
	//==============================================================================================
	array<Byte>^ EightBimValue::ToByteArray()
	{
		array<Byte>^ data = gcnew array<Byte>(_Data->Length);
		Array::Copy(_Data, 0, data, 0, _Data->Length);
		return data;
	}
	//==============================================================================================
	String^ EightBimValue::ToString()
	{
		return ToString(Encoding::UTF8);
	}
	//==============================================================================================
	String^ EightBimValue::ToString(System::Text::Encoding^ encoding)
	{
		Throw::IfNull("encoding", encoding);

		return encoding->GetString(_Data);
	}
	//==============================================================================================
}