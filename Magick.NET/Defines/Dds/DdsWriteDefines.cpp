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
#include "DdsWriteDefines.h"

namespace ImageMagick
{
	//==============================================================================================
	void DdsWriteDefines::AddDefines(Collection<IDefine^>^ defines)
	{
		if (defines == nullptr)
			return;

		if (ClusterFit.HasValue)
			defines->Add(CreateDefine("cluster-fit", ClusterFit.Value));

		if (Compression.HasValue)
			defines->Add(CreateDefine("compression", Compression.Value));

		if (Mipmaps.HasValue)
			defines->Add(CreateDefine("mipmaps", Mipmaps.Value));

		if (WeightByAlpha.HasValue)
			defines->Add(CreateDefine("weight-by-alpha", WeightByAlpha.Value));
	}
	//==============================================================================================
}