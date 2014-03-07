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
#include "ScriptVariables.h"
#include "..\Colors\MagickColor.h"
#include "..\Helpers\XmlHelper.h"
#include "..\MagickConverter.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void ScriptVariables::GetNames(XmlElement^ element)
	{
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			array<String^>^ names = GetNames(attribute->Value);
			if (names == nullptr)
				continue;

			for each(String^ name in names)
			{
				_Variables[name] = nullptr;
			}
		}

		for each(XmlElement^ child in element->ChildNodes)
		{
			GetNames(child);
		}
	}
	//==============================================================================================
	array<String^>^ ScriptVariables::GetNames(String^ value)
	{
		if (value->Length < 3)
			return nullptr;

		MatchCollection^ matches = _Names->Matches(value);
		if (matches->Count == 0)
			return nullptr;

		array<String^>^ result = gcnew array<String^>(matches->Count);
		for (int i=0; i < matches->Count; i++)
		{
			result[i] = matches[i]->Groups["name"]->Value;
		}

		return result;
	}
	//==============================================================================================
	ScriptVariables::ScriptVariables(XmlDocument^ script)
	{
		_Variables = gcnew Dictionary<String^, Object^>();
		GetNames(script->DocumentElement);
	}
	//==============================================================================================
	generic <class T>
	T ScriptVariables::GetValue(XmlAttribute^ attribute)
	{
		if (attribute == nullptr)
			return T();

		array<String^>^ names = GetNames(attribute->Value);
		if (names == nullptr)
			return XmlHelper::GetValue<T>(attribute);

		if (T::typeid == String::typeid)
		{
			String^ newValue = attribute->Value;
			for each(String^ name in names)
			{
				newValue = newValue->Replace(newValue, MagickConverter::Convert<String^>(_Variables[name]));
			}

			return (T)newValue;
		}
		else
		{
			String^ name = names[0];

			if (T::typeid->IsValueType)
				Throw::IfNull("attribute", _Variables[name], "The variable {0} should be set.", name);

			return MagickConverter::Convert<T>(_Variables[name]);
		}
	}
	//==============================================================================================
	generic <class T>
	T ScriptVariables::GetValue(XmlElement^ element, String^ attribute)
	{
		return GetValue<T>(element->Attributes[attribute]);
	}
	//==============================================================================================
	Object^ ScriptVariables::default::get(String^ name)
	{
		return _Variables[name];
	}
	//==============================================================================================
	void ScriptVariables::default::set(String^ name, Object^ value)
	{
		Set(name, value);
	}
	//==============================================================================================
	IEnumerable<String^>^ ScriptVariables::Names::get()
	{
		return _Variables->Keys;
	}
	//==============================================================================================
	Object^ ScriptVariables::Get(String^ name)
	{
		return _Variables[name];
	}
	//==============================================================================================
	void ScriptVariables::Set(String^ name, Object^ value)
	{
		Throw::IfFalse("name", _Variables->ContainsKey(name), "Invalid variable name: {0}", value);
		_Variables[name] = value;
	}
	//==============================================================================================
}