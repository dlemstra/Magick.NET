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
#include "DefineCreator.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	DefineCreator::DefineCreator(MagickFormat format)
	{
		_Format = format;
	}
	//==============================================================================================
	MagickDefine^ DefineCreator::CreateDefine(String^ name, bool value)
	{
		return gcnew MagickDefine(_Format, name, value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	MagickDefine^ DefineCreator::CreateDefine(String^ name, int value)
	{
		return gcnew MagickDefine(_Format, name, value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	MagickDefine^ DefineCreator::CreateDefine(String^ name, MagickGeometry^ value)
	{
		return gcnew MagickDefine(_Format, name, value->ToString());
	}
	//==============================================================================================
	MagickDefine^ DefineCreator::CreateDefine(String^ name, String^ value)
	{
		return gcnew MagickDefine(_Format, name, value);
	}
	//==============================================================================================
	generic<typename TEnum>
	where TEnum : value class, ValueType
	MagickDefine^ DefineCreator::CreateDefine(String^ name, TEnum value)
	{
		return gcnew MagickDefine(_Format, name, Enum::GetName(TEnum::typeid, value));
	}
	//==============================================================================================
	IEnumerable<IDefine^>^ DefineCreator::Defines::get()
	{
		Collection<IDefine^>^ defines = gcnew Collection<IDefine^>();
		AddDefines(defines);
		return defines;
	}
	//==============================================================================================
}