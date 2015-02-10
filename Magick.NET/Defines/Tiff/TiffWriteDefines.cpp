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
#include "TiffWriteDefines.h"

namespace ImageMagick
{
	//==============================================================================================
	void TiffWriteDefines::AddDefines(Collection<IDefine^>^ defines)
	{
		if (defines == nullptr)
			return;

		if (Alpha.HasValue)
			defines->Add(CreateDefine("alpha", Alpha.Value));

		if (Endian.HasValue && Endian.Value != ImageMagick::Endian::Undefined)
			defines->Add(CreateDefine("endian", Endian.Value));

		if (FillOrder.HasValue && FillOrder.Value != ImageMagick::Endian::Undefined)
			defines->Add(CreateDefine("fill-order", FillOrder.Value));

		if (RowsPerStrip.HasValue)
			defines->Add(CreateDefine("rows-per-strip", RowsPerStrip.Value));

		if (TileGeometry != nullptr)
			defines->Add(CreateDefine("tile-geometry", TileGeometry->ToString()));
	}
	//==============================================================================================
}