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
#include "..\Arguments\MagickGeometry.h"
#include "..\Arguments\PointD.h"
#include "..\Colors\MagickColor.h"
#include "EnumHelper.h"
#include "MagickConverter.h"

using namespace System::Drawing;
using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	generic <class T>
	T MagickConverter::Convert(String^ value)
	{
		Type^ type = T::typeid;

		if (type == String::typeid)
			return (T)value;

		if (String::IsNullOrEmpty(value))
			return T();

		if (type->IsGenericType && type->GetGenericTypeDefinition()->Name == "Nullable`1")
			type = type->GetGenericArguments()[0];

		if (type->IsEnum)
			return (T)EnumHelper::Parse(type, value);

		if (type == bool::typeid)
			return (T)(value == "1" || value == "true");

		if (type == MagickColor::typeid)
			return (T)gcnew MagickColor(value);

		if (type == MagickGeometry::typeid)
			return (T)gcnew MagickGeometry(value);

		if (type == Percentage::typeid)
			return (T)gcnew Percentage((double)Convert::ChangeType(value, double::typeid, CultureInfo::InvariantCulture));

		if (type == PointD::typeid)
			return (T)gcnew PointD(value);

		return (T)Convert::ChangeType(value, type, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
	generic <class T>
	T MagickConverter::Convert(Object^ value)
	{
		if (value == nullptr)
			return T();

		Type^ type = T::typeid;
		Type^ objectType = value->GetType();

		if (objectType == type)
			return (T)value;

		if (objectType == String::typeid)
			return Convert<T>((String^) value);

		if (type == MagickColor::typeid)
		{
			if (objectType == Color::typeid)
				return (T)gcnew MagickColor((Color)value);
		}
		else if (type == MagickGeometry::typeid)
		{
			if (objectType == Rectangle::typeid)
				return (T)gcnew MagickGeometry((Rectangle)value);
		}
		else if (type == Percentage::typeid)
		{
			if (objectType == int::typeid)
				return (T)gcnew Percentage((int)value);
			
			if (objectType == double::typeid)
				return (T)gcnew Percentage((double)value);
		}

		return (T)Convert::ChangeType(value, T::typeid, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
}