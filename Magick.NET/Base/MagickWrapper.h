//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick
{
	//==============================================================================================
	template<typename TMagickObject>
	public ref class MagickWrapper abstract
	{
		//===========================================================================================
	private:
		//===========================================================================================
		TMagickObject* _Value;
		//===========================================================================================
		void DisposeValue()
		{
			if (_Value == NULL)
				return;

			delete _Value;
			_Value = NULL;
		}
		//===========================================================================================
		!MagickWrapper()
		{
			DisposeValue();
		}
		//===========================================================================================
	protected private:
		//===========================================================================================
		MagickWrapper()
		{
		}
		//===========================================================================================
		property TMagickObject* Value
		{
			TMagickObject* get()
			{
				if (_Value == NULL)
					throw gcnew ObjectDisposedException(GetType()->ToString());

				return _Value;
			}
			void set(TMagickObject* value)
			{
				_Value = value;
			}
		}
		//===========================================================================================
		void ReplaceValue(TMagickObject& value)
		{
			DisposeValue();
			Value = new TMagickObject(value);
		}
		//===========================================================================================
	public:
		//===========================================================================================
		~MagickWrapper()
		{
			this->!MagickWrapper();
		}
		//===========================================================================================
	};
	//==============================================================================================
}