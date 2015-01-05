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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Base class for Drawable objects.
	///</summary>
	public ref class Drawable abstract
	{
		//===========================================================================================
	private:
		//===========================================================================================
		Magick::DrawableBase* _Value;
		//===========================================================================================
		!Drawable()
		{
			if (_Value == NULL)
				return;

			delete _Value;
			_Value = NULL;
		}
		//===========================================================================================
	protected private:
		//===========================================================================================
		Drawable(){};
		//===========================================================================================
		property Magick::DrawableBase* BaseValue
		{
			void set(Magick::DrawableBase* value)
			{
				_Value = value;
			}
		}
		//===========================================================================================
	internal:
		//===========================================================================================
		property Magick::DrawableBase* InternalValue
		{
			Magick::DrawableBase* get()
			{
				if (_Value == NULL)
					throw gcnew ObjectDisposedException(GetType()->ToString());

				return _Value;
			}
		}
		//===========================================================================================
	public:
		//===========================================================================================
		~Drawable()
		{
			this->!Drawable();
		}
		//===========================================================================================
	};
	//==============================================================================================
}