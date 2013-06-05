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
#include "..\Colors\MagickColor.h"
#include "..\Percentage.h"
#include "EnumHelper.h"
#include "XmlHelper.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	generic <class T>
	T XmlHelper::GetValue(String^ value)
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

		if (type == MagickColor::typeid)
			return (T)gcnew MagickColor(value);

		if (type == Percentage::typeid)
		{
			return (T)gcnew Percentage((double)Convert::ChangeType(value, double::typeid, CultureInfo::InvariantCulture));
		}

		return (T)Convert::ChangeType(value, type, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
	generic <class T>
	T XmlHelper::GetAttribute(XmlElement^ element, String^ name)
	{
		if (element == nullptr || !element->HasAttribute(name))
			return T();

		return GetValue<T>(element->GetAttribute(name));
	}
	//==============================================================================================
	generic <class T>
	T XmlHelper::GetValue(XmlAttribute^ attribute)
	{
		if (attribute == nullptr)
			return T();

		return GetValue<T>(attribute->Value);
	}
	//==============================================================================================
}
