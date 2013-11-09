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
#include "..\MagickGeometry.h"
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

		if (type == MagickGeometry::typeid)
			return (T)gcnew MagickGeometry(value);

		if (type == Percentage::typeid)
			return (T)gcnew Percentage((double)Convert::ChangeType(value, double::typeid, CultureInfo::InvariantCulture));

		if (type == bool::typeid)
			return (T)(value == "1" || value == "true");

		return (T)Convert::ChangeType(value, type, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
	XmlElement^ XmlHelper::CreateElement(XmlNode^ node, String^ name)
	{
		XmlDocument^ doc = node->GetType() == XmlDocument::typeid ? (XmlDocument^)node : node->OwnerDocument;
		XmlElement^ element = doc->CreateElement(name);
		node->AppendChild(element);
		return element;
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
	generic <class T>
	void XmlHelper::SetAttribute(XmlElement^ element, String^ name, T value)
	{
		if (element == nullptr)
			return;

		XmlAttribute^ attribute;
		if (element->HasAttribute(name))
			attribute = element->Attributes[name];
		else
			attribute = element->Attributes->Append(element->OwnerDocument->CreateAttribute(name));

		attribute->Value = (String^)Convert::ChangeType(value, String::typeid, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
}
