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
#pragma once

#include "..\MagickColor.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Base class for colors
	///</summary>
	public ref class ColorBase abstract
	{
		//===========================================================================================
	private:
		//===========================================================================================
		bool _HasAlpha;
		MagickColor^ _Value;
		//===========================================================================================
	protected:
		//===========================================================================================
		ColorBase(bool hasAlpha);
		//===========================================================================================
		ColorBase(bool hasAlpha, MagickColor^ color);
		//===========================================================================================
		property MagickColor^ Value
		{
			MagickColor^ get()
			{
				return _Value;
			}
		}
		//===========================================================================================
		virtual void UpdateValue();
		//===========================================================================================
	public:
		//===========================================================================================
		static operator MagickColor^ (ColorBase^ color)
		{
			if (color == nullptr)
				return nullptr;

			return color->ToMagickColor();
		}
		///==========================================================================================
		///<summary>
		/// Converts the value of this instance to an equivalent MagickColor.
		///</summary>
		MagickColor^ ToMagickColor();
		//===========================================================================================
	};
	//==============================================================================================
}