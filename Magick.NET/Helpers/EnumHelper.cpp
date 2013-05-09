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
#include "stdafx.h"
#include "EnumHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	generic<typename TEnum>
	where TEnum : value class, ValueType
	TEnum EnumHelper::Parse(String^ value, TEnum defaultValue)
	{
		TEnum result;

#if (_MSC_VER == 1700)
		if (!Enum::TryParse<TEnum>(value, true, result))
			return defaultValue;
#elif (_MSC_VER == 1500)
		if (!Enum::IsDefined(TEnum::typeid, value))
			return defaultValue;

		result = (TEnum)Enum::Parse(TEnum::typeid, value);
#else
		Not implemented!
#endif

		return result;
	}
	//==============================================================================================
}