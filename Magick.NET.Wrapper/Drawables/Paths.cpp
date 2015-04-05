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
#include "Paths.h"

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		Magick::VPathBase* Paths::CreatePath(IPath^ path)
		{
			Type^ interfaceType = path->GetType();

			if (IPathArcAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathArcAbs((IPathArcAbs^)path);

			if (IPathArcRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathArcRel((IPathArcRel^)path);

			if (IPathClosePath::typeid->IsAssignableFrom(interfaceType))
				return CreatePathClosePath();

			if (IPathCurvetoAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathCurvetoAbs((IPathCurvetoAbs^)path);

			if (IPathCurvetoRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathCurvetoRel((IPathCurvetoRel^)path);

			if (IPathLinetoAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoAbs((IPathLinetoAbs^)path);

			if (IPathLinetoHorizontalAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoHorizontalAbs((IPathLinetoHorizontalAbs^)path);

			if (IPathLinetoHorizontalRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoHorizontalRel((IPathLinetoHorizontalRel^)path);

			if (IPathLinetoRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoRel((IPathLinetoRel^)path);

			if (IPathLinetoVerticalAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoVerticalAbs((IPathLinetoVerticalAbs^)path);

			if (IPathLinetoVerticalRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathLinetoVerticalRel((IPathLinetoVerticalRel^)path);

			if (IPathMovetoAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathMovetoAbs((IPathMovetoAbs^)path);

			if (IPathMovetoRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathMovetoRel((IPathMovetoRel^)path);

			if (IPathQuadraticCurvetoAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathQuadraticCurvetoAbs((IPathQuadraticCurvetoAbs^)path);

			if (IPathQuadraticCurvetoRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathQuadraticCurvetoRel((IPathQuadraticCurvetoRel^)path);

			if (IPathSmoothCurvetoAbs::typeid->IsAssignableFrom(interfaceType))
				return CreatePathSmoothCurvetoAbs((IPathSmoothCurvetoAbs^)path);

			if (IPathSmoothCurvetoRel::typeid->IsAssignableFrom(interfaceType))
				return CreatePathSmoothCurvetoRel((IPathSmoothCurvetoRel^)path);

			if (IPathSmoothQuadraticCurvetoAbs::typeid->IsAssignableFrom(interfaceType))
				return PathSmoothQuadraticCurvetoAbs((IPathSmoothQuadraticCurvetoAbs^)path);

			if (IPathSmoothQuadraticCurvetoRel::typeid->IsAssignableFrom(interfaceType))
				return PathSmoothQuadraticCurvetoRel((IPathSmoothQuadraticCurvetoRel^)path);

			throw gcnew NotImplementedException(interfaceType->Name);
		}
		//===========================================================================================
		Magick::PathArcAbs* Paths::CreatePathArcAbs(IPathArcAbs^ path)
		{
			return CreatePathArc<Magick::PathArcAbs>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathArcRel* Paths::CreatePathArcRel(IPathArcRel^ path)
		{
			return CreatePathArc<Magick::PathArcRel>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathClosePath* Paths::CreatePathClosePath()
		{
			return new Magick::PathClosePath();
		}
		//===========================================================================================
		Magick::PathCurvetoAbs* Paths::CreatePathCurvetoAbs(IPathCurvetoAbs^ path)
		{
			return CreatePathCurveto<Magick::PathCurvetoAbs>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathCurvetoRel* Paths::CreatePathCurvetoRel(IPathCurvetoRel^ path)
		{
			return CreatePathCurveto<Magick::PathCurvetoRel>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathLinetoAbs* Paths::CreatePathLinetoAbs(IPathLinetoAbs^ path)
		{
			return CreatePathCoordinates<Magick::PathLinetoAbs>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathLinetoHorizontalAbs* Paths::CreatePathLinetoHorizontalAbs(IPathLinetoHorizontalAbs^ path)
		{
			return new Magick::PathLinetoHorizontalAbs(path->X);
		}
		//===========================================================================================
		Magick::PathLinetoHorizontalRel* Paths::CreatePathLinetoHorizontalRel(IPathLinetoHorizontalRel^ path)
		{
			return new Magick::PathLinetoHorizontalRel(path->X);
		}
		//===========================================================================================
		Magick::PathLinetoRel* Paths::CreatePathLinetoRel(IPathLinetoRel^ path)
		{
			return CreatePathCoordinates<Magick::PathLinetoRel>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathLinetoVerticalAbs* Paths::CreatePathLinetoVerticalAbs(IPathLinetoVerticalAbs^ path)
		{
			return new Magick::PathLinetoVerticalAbs(path->Y);
		}
		//===========================================================================================
		Magick::PathLinetoVerticalRel* Paths::CreatePathLinetoVerticalRel(IPathLinetoVerticalRel^ path)
		{
			return new Magick::PathLinetoVerticalRel(path->Y);
		}
		//===========================================================================================
		Magick::PathMovetoAbs* Paths::CreatePathMovetoAbs(IPathMovetoAbs^ path)
		{
			return CreatePathCoordinates<Magick::PathMovetoAbs>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathMovetoRel* Paths::CreatePathMovetoRel(IPathMovetoRel^ path)
		{
			return CreatePathCoordinates<Magick::PathMovetoRel>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathQuadraticCurvetoAbs* Paths::CreatePathQuadraticCurvetoAbs(IPathQuadraticCurvetoAbs^ path)
		{
			return CreatePathQuadraticCurveto<Magick::PathQuadraticCurvetoAbs>(path->Coordinates);
		}
		//===========================================================================================
		Magick::PathQuadraticCurvetoRel* Paths::CreatePathQuadraticCurvetoRel(IPathQuadraticCurvetoRel^ path)
		{
			return CreatePathQuadraticCurveto<Magick::PathQuadraticCurvetoRel>(path->Coordinates);
		}
		//========================================================================================
		Magick::PathSmoothCurvetoAbs* Paths::CreatePathSmoothCurvetoAbs(IPathSmoothCurvetoAbs^ path)
		{
			return CreatePathCoordinates<Magick::PathSmoothCurvetoAbs>(path->Coordinates);
		}
		//========================================================================================
		Magick::PathSmoothCurvetoRel* Paths::CreatePathSmoothCurvetoRel(IPathSmoothCurvetoRel^ path)
		{
			return CreatePathCoordinates<Magick::PathSmoothCurvetoRel>(path->Coordinates);
		}
		//========================================================================================
		Magick::PathSmoothQuadraticCurvetoAbs* Paths::PathSmoothQuadraticCurvetoAbs(IPathSmoothQuadraticCurvetoAbs^ path)
		{
			return CreatePathCoordinates<Magick::PathSmoothQuadraticCurvetoAbs>(path->Coordinates);
		}
		//========================================================================================
		Magick::PathSmoothQuadraticCurvetoRel* Paths::PathSmoothQuadraticCurvetoRel(IPathSmoothQuadraticCurvetoRel^ path)
		{
			return CreatePathCoordinates<Magick::PathSmoothQuadraticCurvetoRel>(path->Coordinates);
		}
		//===========================================================================================
	}
}