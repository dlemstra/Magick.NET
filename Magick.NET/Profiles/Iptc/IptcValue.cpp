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
#include "IptcValue.h"

using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	IptcValue::IptcValue(IptcTag tag, array<Byte>^ data)
	{
		Throw::IfNull("data", data);

		_Tag = tag;
		_Data = data;
	}
	//==============================================================================================
	bool IptcValue::operator == (IptcValue^ left, IptcValue^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool IptcValue::operator != (IptcValue^ left, IptcValue^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool IptcValue::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<IptcValue^>(obj));
	}
	//==============================================================================================
	bool IptcValue::Equals(IptcValue^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		if (_Tag != other->_Tag)
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
	IptcTag IptcValue::Tag::get()
	{
		return _Tag;
	}
	//==============================================================================================
	String^ IptcValue::Value::get()
	{
		return Encoding::Default->GetString(_Data);
	}
	//==============================================================================================
	int IptcValue::GetHashCode()
	{
		return _Tag.GetHashCode() ^ _Data->GetHashCode();
	}
	//==============================================================================================
	array<Byte>^ IptcValue::ToByteArray()
	{
		array<Byte>^ result = gcnew array<Byte>(_Data->Length);
		if (_Data->Length > 0)
			_Data->CopyTo(result, 0);
		return result;
	}
	//==============================================================================================
	String^ IptcValue::ToString()
	{
		return Value;
	}
	//==============================================================================================
}