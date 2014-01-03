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
#include "Percentage.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	Percentage::operator Magick::Quantum(Percentage percentage)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		return Convert::ToByte(percentage._Value * 100);
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		return Convert::ToUInt16(percentage._Value * 100);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
	Magick::Quantum Percentage::ToQuantum()
	{
		return (Magick::Quantum)(MaxMap * _Value);
	}
	//==============================================================================================
	Percentage::Percentage(double value)
	{
		_Value = value;
	}
	//==============================================================================================
	Percentage::Percentage(int value)
	{
		_Value = (double)value / 100;
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
	Percentage::operator double(Percentage percentage)
	{
		return percentage.ToDouble();
	}
	//==============================================================================================
	Percentage::operator int(Percentage percentage)
	{
		return percentage.ToInt32();
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
			return this->ToInt32().Equals((int)obj);

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
	double Percentage::ToDouble()
	{
		return _Value;
	}
	//==============================================================================================
	int Percentage::ToInt32()
	{
		return Convert::ToInt32(_Value * 100);
	}
	//==============================================================================================
	String^ Percentage::ToString()
	{
		return String::Format(CultureInfo::InvariantCulture, "{0}%", ToInt32());
	}
	//==============================================================================================
}