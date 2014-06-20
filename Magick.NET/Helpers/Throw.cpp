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

using namespace System::IO;
using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	String^ Throw::FormatMessage(String^ message, ... array<Object^>^ args)
	{
		if (args->Length == 0)
			return message;

		return String::Format(CultureInfo::InvariantCulture, message, args);
	}
	//==============================================================================================
	void Throw::IfFalse(String^ paramName, bool condition, String^ message, ... array<Object^>^ args)
	{
		if (!condition)
			throw gcnew ArgumentException(FormatMessage(message, args), paramName);
	}
	//==============================================================================================
	void Throw::IfInvalidFileName(String^ fileName)
	{
		Throw::IfNullOrEmpty("fileName", fileName);

		if (!fileName->Contains("\\") && fileName->Contains(":"))
			return;

		if (fileName->Length > 248)
			return;

		String^ path = Path::GetFullPath(fileName);
		if (path->EndsWith("]", StringComparison::OrdinalIgnoreCase))
		{
			int endIndex = path->IndexOf("[", StringComparison::OrdinalIgnoreCase);
			if (endIndex != -1)
				path = path->Substring(0, endIndex);
		}

		Throw::IfFalse("fileName", File::Exists(path), "Unable to find file: {0}", path);
	}
	//==============================================================================================
	void Throw::IfNegative(String^ paramName, Percentage value)
	{
		if ((double)value < 0.0)
			throw gcnew ArgumentException("Value should be greater then zero.", paramName);
	}
	//==============================================================================================
	void Throw::IfNull(String^ paramName, Object^ value)
	{
		if (value == nullptr)
			throw gcnew ArgumentNullException(paramName);
	}
	//==============================================================================================
	void Throw::IfNull(String^ paramName, Object^ value, String^ message, ... array<Object^>^ args)
	{
		if (value == nullptr)
			throw gcnew ArgumentNullException(paramName, FormatMessage(message, args));
	}
	//==============================================================================================
	void Throw::IfNullOrEmpty(String^ paramName, Array^ value)
	{
		Throw::IfNull(paramName, value);

		if (value->Length == 0)
			throw gcnew ArgumentException("Value cannot be empty", paramName);
	}
	//==============================================================================================
	void Throw::IfNullOrEmpty(String^ paramName, String^ value)
	{
		Throw::IfNull(paramName, value);

		if (value->Length == 0)
			throw gcnew ArgumentException("Value cannot be empty", paramName);
	}
	//==============================================================================================
	void Throw::IfNullOrEmpty(String^ paramName, String^ value, String^ message, ... array<Object^>^ args)
	{
		Throw::IfNull(paramName, value, message, args);

		if (value->Length == 0)
			throw gcnew ArgumentException(FormatMessage(message, args), paramName);
	}
	//==============================================================================================
	void Throw::IfOutOfRange(String^ paramName, int index, int length)
	{
		if (index < 0 || index >= length)
			throw gcnew ArgumentOutOfRangeException(paramName);
	}
	//==============================================================================================
	void Throw::IfTrue(String^ paramName, bool condition, String^ message, ... array<Object^>^ args)
	{
		if (condition)
			throw gcnew ArgumentException(FormatMessage(message, args), paramName);
	}
	//==============================================================================================
}
