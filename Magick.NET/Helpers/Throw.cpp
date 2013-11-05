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

using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	void Throw::IfFalse(String^ paramName, bool condition, String^ message)
	{
		if (!condition)
			throw gcnew ArgumentException(message, paramName);
	}
	//==============================================================================================
	void Throw::IfInvalidFileName(String^ fileName)
	{
		Throw::IfNullOrEmpty("fileName", fileName);

		if (!fileName->Contains("\\") && fileName->Contains(":"))
			return;

		String^ path = Path::GetFullPath(fileName);
		if (path->EndsWith("]", StringComparison::OrdinalIgnoreCase))
		{
			int endIndex = path->IndexOf("[", StringComparison::OrdinalIgnoreCase);
			if (endIndex != -1)
				path = path->Substring(0, endIndex);
		}

		Throw::IfFalse("fileName", File::Exists(path), "Unable to find file: " + path);
	}
	//==============================================================================================
	void Throw::IfNegative(String^ paramName, Percentage value)
	{
		if (value.ToDouble() < 0.0)
			throw gcnew ArgumentException("Value should be greater then zero.", paramName);
	}
	//==============================================================================================
	void Throw::IfNull(String^ paramName, Object^ value)
	{
		if (value == nullptr)
			throw gcnew ArgumentNullException(paramName);
	}
	//==============================================================================================
	void Throw::IfNull(String^ paramName, Object^ value, String^ message)
	{
		if (value == nullptr)
			throw gcnew ArgumentNullException(paramName, message);
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
	void Throw::IfNullOrEmpty(String^ paramName, String^ value, String^ message)
	{
		Throw::IfNull(paramName, value, message);

		if (value->Length == 0)
			throw gcnew ArgumentException(message, paramName);
	}
	//==============================================================================================
	void Throw::IfOutOfRange(String^ paramName, int index, int length)
	{
		if (index < 0 || index >= length)
			throw gcnew ArgumentOutOfRangeException(paramName);
	}
	//==============================================================================================
	void Throw::IfTrue(String^ paramName, bool condition, String^ message)
	{
		if (condition)
			throw gcnew ArgumentException(message, paramName);
	}
	//==============================================================================================
}
