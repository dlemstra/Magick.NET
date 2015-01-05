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
#include "..\Helpers\MagickConverter.h"
#include "XmlHelper.h"

using namespace System::Globalization;

namespace ImageMagick
{
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

		return MagickConverter::Convert<T>(element->GetAttribute(name));
	}
	//==============================================================================================
	generic <class T>
	T XmlHelper::GetValue(XmlAttribute^ attribute)
	{
		if (attribute == nullptr)
			return T();

		return MagickConverter::Convert<T>(attribute->Value);
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

		if (T::typeid == String::typeid)
			attribute->Value = (String^)value;
		else
			attribute->Value = (String^)Convert::ChangeType(value, String::typeid, CultureInfo::InvariantCulture);
	}
	//==============================================================================================
}
