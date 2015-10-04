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

using namespace System::Collections::Generic;
using namespace ImageMagick::Drawables::Paths;

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class Paths abstract sealed
    {
    private:
      template<typename TPathArc>
      static TPathArc* CreatePathArc(IEnumerable<IPathArc^>^ coordinates)
      {
        Magick::PathArcArgsList coordinateList;
        IEnumerator<IPathArc^>^ enumerator = coordinates->GetEnumerator();
        while (enumerator->MoveNext())
        {
          Magick::PathArcArgs coordinate = Magick::PathArcArgs(enumerator->Current->RadiusX,
            enumerator->Current->RadiusY, enumerator->Current->RotationX,
            enumerator->Current->UseLargeArc, enumerator->Current->UseSweep,
            enumerator->Current->X, enumerator->Current->Y);
          coordinateList.push_back(coordinate);
        }

        return new TPathArc(coordinateList);
      }

      template<typename TPath>
      static TPath* CreatePathCoordinates(IEnumerable<Coordinate>^ coordinates)
      {
        Magick::CoordinateList coordinateList;
        IEnumerator<Coordinate>^ enumerator = coordinates->GetEnumerator();
        while (enumerator->MoveNext())
        {
          coordinateList.push_back(Magick::Coordinate(enumerator->Current.X, enumerator->Current.Y));
        }

        return new TPath(coordinateList);
      }

      template<typename TPathCurveto>
      static TPathCurveto* CreatePathCurveto(IEnumerable<IPathCurveto^>^ coordinates)
      {
        Magick::PathCurveToArgsList coordinateList;
        IEnumerator<IPathCurveto^>^ enumerator = coordinates->GetEnumerator();
        while (enumerator->MoveNext())
        {
          Magick::PathCurvetoArgs coordinate = Magick::PathCurvetoArgs(enumerator->Current->X1,
            enumerator->Current->Y1, enumerator->Current->X2, enumerator->Current->Y2,
            enumerator->Current->X, enumerator->Current->Y);
          coordinateList.push_back(coordinate);
        }

        return new TPathCurveto(coordinateList);
      }

      template<typename TPathQuadraticCurveto>
      static TPathQuadraticCurveto* CreatePathQuadraticCurveto(IEnumerable<IPathQuadraticCurveto^>^ coordinates)
      {
        Magick::PathQuadraticCurvetoArgsList coordinateList;
        IEnumerator<IPathQuadraticCurveto^>^ enumerator = coordinates->GetEnumerator();
        while (enumerator->MoveNext())
        {
          Magick::PathQuadraticCurvetoArgs coordinate = Magick::PathQuadraticCurvetoArgs(
            enumerator->Current->X1, enumerator->Current->Y1,
            enumerator->Current->X, enumerator->Current->Y);
          coordinateList.push_back(coordinate);
        }

        return new TPathQuadraticCurveto(coordinateList);
      }

    public:
      static Magick::VPathBase* CreatePath(IPath^ path);

      static Magick::PathArcAbs* CreatePathArcAbs(IPathArcAbs^ path);

      static Magick::PathArcRel* CreatePathArcRel(IPathArcRel^ path);

      static Magick::PathClosePath* CreatePathClosePath();

      static Magick::PathCurvetoAbs* CreatePathCurvetoAbs(IPathCurvetoAbs^ path);

      static Magick::PathCurvetoRel* CreatePathCurvetoRel(IPathCurvetoRel^ path);

      static Magick::PathLinetoAbs* CreatePathLinetoAbs(IPathLinetoAbs^ path);

      static Magick::PathLinetoHorizontalAbs* CreatePathLinetoHorizontalAbs(IPathLinetoHorizontalAbs^ path);

      static Magick::PathLinetoHorizontalRel* CreatePathLinetoHorizontalRel(IPathLinetoHorizontalRel^ path);

      static Magick::PathLinetoRel* CreatePathLinetoRel(IPathLinetoRel^ path);

      static Magick::PathLinetoVerticalAbs* Paths::CreatePathLinetoVerticalAbs(IPathLinetoVerticalAbs^ path);

      static Magick::PathLinetoVerticalRel* Paths::CreatePathLinetoVerticalRel(IPathLinetoVerticalRel^ path);

      static Magick::PathMovetoAbs* CreatePathMovetoAbs(IPathMovetoAbs^ path);

      static Magick::PathMovetoRel* CreatePathMovetoRel(IPathMovetoRel^ path);

      static Magick::PathQuadraticCurvetoAbs* CreatePathQuadraticCurvetoAbs(IPathQuadraticCurvetoAbs^ path);

      static Magick::PathQuadraticCurvetoRel* CreatePathQuadraticCurvetoRel(IPathQuadraticCurvetoRel^ path);

      static Magick::PathSmoothCurvetoAbs* CreatePathSmoothCurvetoAbs(IPathSmoothCurvetoAbs^ path);

      static Magick::PathSmoothCurvetoRel* CreatePathSmoothCurvetoRel(IPathSmoothCurvetoRel^ path);

      static Magick::PathSmoothQuadraticCurvetoAbs* PathSmoothQuadraticCurvetoAbs(IPathSmoothQuadraticCurvetoAbs^ path);

      static Magick::PathSmoothQuadraticCurvetoRel* PathSmoothQuadraticCurvetoRel(IPathSmoothQuadraticCurvetoRel^ path);
    };
  }
}