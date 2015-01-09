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
#include "..\Quantum.h"
#include "Percentage.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	Percentage::operator Magick::Quantum(Percentage percentage)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		return Convert::ToByte(percentage._Value);
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		return Convert::ToUInt16(percentage._Value);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
	Percentage::operator double(Percentage percentage)
	{
		return percentage._Value;
	}
	//==============================================================================================
	Percentage::operator int(Percentage percentage)
	{
		return (int)percentage._Value;
	}
	//==============================================================================================
	Percentage Percentage::FromQuantum(double value)
	{
		return Percentage((value / (double)Quantum::Max) * 100);
	}
	//==============================================================================================
	Magick::Quantum Percentage::ToQuantum()
	{
		return (Magick::Quantum)((double)Quantum::Max * (_Value / 100));
	}
	//==============================================================================================
	Percentage::Percentage(double value)
	{
		_Value = value;
	}
	//==============================================================================================
	Percentage::Percentage(int value)
	{
		_Value = (double)value;
	}
	//==============================================================================================
	bool Percentage::operator == (Percentage left, Percentage right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool Percentage::operator != (Percentage left, Percentage right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	Percentage::operator Percentage(double value)
	{
		return Percentage(value);
	}
	//==============================================================================================
	Percentage::operator Percentage(int value)
	{
		return Percentage(value);
	}
	//==============================================================================================
	bool Percentage::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		if (obj->GetType() == Percentage::typeid)
			return Equals((Percentage)obj);

		if (obj->GetType() == double::typeid)
			return _Value.Equals(obj);

		if (obj->GetType() == int::typeid)
			return ((int)_Value).Equals((int)obj);

		return false;
	}
	//==============================================================================================
	bool Percentage::Equals(Percentage other)
	{
		return _Value.Equals(other._Value);
	}
	//==============================================================================================
	int Percentage::GetHashCode()
	{
		return _Value.GetHashCode();
	}
	//==============================================================================================
	String^ Percentage::ToString()
	{
		return String::Format(CultureInfo::InvariantCulture, "{0:0.##}%", _Value);
	}
	//==============================================================================================
}