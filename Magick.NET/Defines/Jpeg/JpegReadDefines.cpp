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
#include "JpegReadDefines.h"
#include "..\..\Helpers\EnumHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	void JpegReadDefines::AddDefines(Collection<IDefine^>^ defines)
	{
		if (defines == nullptr)
			return;

		if (BlockSmoothing.HasValue)
			defines->Add(CreateDefine("block-smoothing", BlockSmoothing.Value));

		if (Colors.HasValue)
			defines->Add(CreateDefine("colors", Colors.Value));

		if (DctMethod.HasValue)
			defines->Add(CreateDefine("dct-method", DctMethod.Value));

		if (FancyUpsampling.HasValue)
			defines->Add(CreateDefine("fancy-upsampling", FancyUpsampling.Value));

		if (Size != nullptr)
			defines->Add(CreateDefine("size", Size));

		if (SkipProfiles.HasValue)
		{
			String^ value = "";
			for each(ProfileTypes profileType in EnumHelper::GetFlags(SkipProfiles.Value))
			{
				if (value->Length != 0)
					value += ",";

				value += Enum::GetName(ProfileTypes::typeid, profileType);
			}

			if (!String::IsNullOrEmpty(value))
				defines->Add(gcnew MagickDefine(MagickFormat::Unknown, "profile:skip", value));
		}
	}
	//==============================================================================================
}