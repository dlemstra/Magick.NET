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

#include "PathBase.h"
#include "..\..\Coordinate.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	//==============================================================================================
	template<typename TMagickObject>
	public ref class PathWrapper abstract : PathBase
	{
		//===========================================================================================
	protected private:
		//===========================================================================================
		PathWrapper() {};
		//===========================================================================================
		template<typename TPathArgs>
		void CreateBaseValue(TPathArgs coordinate)
		{
			Throw::IfNull("coordinate", coordinate);

			BaseValue = new TMagickObject(*coordinate->InternalValue);
		}
		//===========================================================================================
		template<typename TMagickPathArgs, typename TPathArgs>
		void CreateBaseValue(IEnumerable<TPathArgs^>^ coordinates)
		{
			Throw::IfNull("coordinates", coordinates);

			std::list<TMagickPathArgs> coordinateList;
			IEnumerator<TPathArgs^>^ enumerator = coordinates->GetEnumerator();
			while(enumerator->MoveNext())
			{
				coordinateList.push_back(*(enumerator->Current->InternalValue));
			}

			BaseValue = new TMagickObject(coordinateList);
		}
		//===========================================================================================
		void CreateBaseValue(Coordinate^ coordinate)
		{
			Throw::IfNull("coordinate", coordinate);

			BaseValue = new TMagickObject(Magick::Coordinate(coordinate->X, coordinate->Y));
		}
		//===========================================================================================
		void CreateBaseValue(IEnumerable<Coordinate^>^ coordinates)
		{
			Throw::IfNull("coordinates", coordinates);

			Magick::CoordinateList magickCoordinates;

			IEnumerator<Coordinate^>^ enumerator = coordinates->GetEnumerator();
			while(enumerator->MoveNext())
			{
				magickCoordinates.push_back(Magick::Coordinate(enumerator->Current->X, enumerator->Current->Y));
			}

			BaseValue = new TMagickObject(magickCoordinates);
		}
		//===========================================================================================
	};
	//==============================================================================================
}