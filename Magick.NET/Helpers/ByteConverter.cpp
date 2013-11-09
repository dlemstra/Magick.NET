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
#include "ByteConverter.h"

namespace ImageMagick
{
	//==============================================================================================
	int ByteConverter::ToInt(array<Byte>^ data, int% offset)
	{
		if (offset + 4 > data->Length)
			return 0;

		int test = BitConverter::ToUInt32(data, offset);
		if (test == -1)
			return 0;

		int result = (int)(data[offset++] << 24);
		result = (result | (int)(data[offset++] << 16));
		result = (result | (int)(data[offset++] << 8));
		result = (result | (int)(data[offset++]));
		return (result & 0xffffffff);
	}
	//==============================================================================================
	short ByteConverter::ToShort(array<Byte>^ data, int% offset)
	{
		if (offset + 2 > data->Length)
			return 0;

		short result = (short)data[offset++] << 8;
		result = result | (short)data[offset++];
		return(result & 0xffff);
	}
	//==============================================================================================
}