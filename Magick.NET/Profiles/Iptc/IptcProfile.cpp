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
#include "IptcProfile.h"
#include "IptcTag.h"
#include "..\..\Helpers\EnumHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	void IptcProfile::Initialize()
	{
		if (_Values != nullptr)
			return;

		_Values = gcnew List<IptcValue^>();

		if (Data[0] != 0x1c)
			return;

		int i = 0;
		while(i < Data->Length)
		{
			if (Data[i++] != 28)
				continue;

			i++;

			IptcTag tag = EnumHelper::Parse<IptcTag>((int)Data[i++], IptcTag::Unknown);

			int count = (int)Data[i++] << 8;
			count = count | (int)Data[i++];

			array<Byte>^ data = gcnew array<Byte>(count);
			if (count > 0)
				Buffer::BlockCopy(Data, i, data, 0, count);
			_Values->Add(gcnew IptcValue(tag, data));

			i += count;
		}
	}
	//==============================================================================================
	IEnumerable<IptcValue^>^ IptcProfile::Values::get()
	{
		Initialize();
		return _Values;
	}
	//==============================================================================================
}