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
#include "JpegWriteDefines.h"

namespace ImageMagick
{
	//==============================================================================================
	void JpegWriteDefines::AddDefines(Collection<IDefine^>^ defines)
	{
		if (defines == nullptr)
			return;

		if (DctMethod.HasValue)
			defines->Add(CreateDefine("dct-method", DctMethod.Value));

		if (Extent.HasValue)
			defines->Add(CreateDefine("extent", Extent.Value + "KB"));

		if (OptimizeCoding.HasValue)
			defines->Add(CreateDefine("optimize-coding", OptimizeCoding.Value));

		if (Quality != nullptr)
			defines->Add(CreateDefine("quality", Quality));

		if (!String::IsNullOrEmpty(QuantizationTables))
			defines->Add(CreateDefine("q-table", QuantizationTables));

		if (SamplingFactors != nullptr)
		{
			String^ value = "";
			for each(MagickGeometry^ samplingFactor in SamplingFactors)
			{
				if (value->Length != 0)
					value += ",";

				value += samplingFactor->ToString();
			}

			if (!String::IsNullOrEmpty(value))
				defines->Add(CreateDefine("sampling-factor", value));
		}
	}
	//==============================================================================================
}