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

#include "Drawable.h"
#include "..\Coordinate.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{	
	//==============================================================================================
	template<typename TMagickObject>
	public ref class DrawableWrapper abstract : Drawable
	{
		//===========================================================================================
	protected private:
		//===========================================================================================
		DrawableWrapper(){};
		//===========================================================================================
		property TMagickObject* Value
		{
			TMagickObject* get()
			{
				return (TMagickObject*)InternalValue;
			}
		}
		//===========================================================================================
		void CreateBaseValue(IEnumerable<Coordinate>^ coordinates)
		{
			Throw::IfNull("coordinates", coordinates);

			Magick::CoordinateList magickCoordinates;
			IEnumerator<Coordinate>^ enumerator = coordinates->GetEnumerator();

			int count = 0;
			while(enumerator->MoveNext())
			{
				magickCoordinates.push_back(Magick::Coordinate(enumerator->Current.X, enumerator->Current.Y));
				count++;
			}

			Throw::IfTrue("coordinates", count < 3, "Coordinates must contain at least 3 coordinates.");

			BaseValue = new TMagickObject(magickCoordinates);
		}
		//===========================================================================================
	};
	//==============================================================================================
}