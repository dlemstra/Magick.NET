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
#include "Pixel.h"

namespace ImageMagick
{
	//==============================================================================================
	Pixel::Pixel(int x, int y, int channels)
	{
		_X = x;
		_Y = y;

		int size = channels;
		if (size < 0)
			size = 1;
		else if (size > 5)
			size = 5;

		_Values = gcnew array<Magick::Quantum>(size);
	}
	//==============================================================================================
	Magick::Quantum Pixel::GetChannel(int channel)
	{
		if (channel < 0 || channel >= _Values->Length)
			return 0;

		return _Values[channel];
	}
	//==============================================================================================
	void Pixel::SetChannel(int channel, Magick::Quantum value)
	{
		if (channel < 0 || channel >= _Values->Length)
			return;

		_Values[channel] = value;
	}
	//==============================================================================================
}