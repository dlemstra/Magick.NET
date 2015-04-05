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
#include "EnumHelper.h"

using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		generic<typename TEnum>
		where TEnum: value class, ValueType
		IEnumerable<TEnum>^ EnumHelper::GetFlags(TEnum value)
		{
			Collection<TEnum>^ flags = gcnew Collection<TEnum>();

			for each (TEnum enumValue in Enum::GetValues(TEnum::typeid))
			{
				if (((int)value & (int)enumValue) != 0)
					flags->Add(enumValue);
			}

			return flags;
		}
		//===========================================================================================
		generic<typename TEnum>
		where TEnum : value class, ValueType
		TEnum EnumHelper::Parse(String^ value, TEnum defaultValue)
		{
#if (NET20)
			Type^ type = TEnum::typeid;

			for each (String^ name in Enum::GetNames(type))
			{
				if (name->Equals(value, StringComparison::OrdinalIgnoreCase))
					return (TEnum)Enum::Parse(type, name);
			}

			return defaultValue;
#else
			TEnum result;
			if (!Enum::TryParse<TEnum>(value, true, result))
				return defaultValue;

			return result;
#endif
		}
		//===========================================================================================
	}
}