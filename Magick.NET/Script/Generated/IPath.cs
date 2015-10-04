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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  public sealed partial class MagickScript
  {
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    private void ExecuteIPath(XmlElement element, Collection<IPath> paths)
    {
      switch(element.Name[0])
      {
        case 'a':
        {
          switch(element.Name[3])
          {
            case 'A':
            {
              ExecutePathArcAbs(element, paths);
              return;
            }
            case 'R':
            {
              ExecutePathArcRel(element, paths);
              return;
            }
          }
          break;
        }
        case 'c':
        {
          switch(element.Name[7])
          {
            case 'A':
            {
              ExecutePathCurvetoAbs(element, paths);
              return;
            }
            case 'R':
            {
              ExecutePathCurvetoRel(element, paths);
              return;
            }
          }
          break;
        }
        case 'l':
        {
          switch(element.Name[6])
          {
            case 'A':
            {
              ExecutePathLinetoAbs(element, paths);
              return;
            }
            case 'H':
            {
              switch(element.Name[16])
              {
                case 'A':
                {
                  ExecutePathLinetoHorizontalAbs(element, paths);
                  return;
                }
                case 'R':
                {
                  ExecutePathLinetoHorizontalRel(element, paths);
                  return;
                }
              }
              break;
            }
            case 'R':
            {
              ExecutePathLinetoRel(element, paths);
              return;
            }
            case 'V':
            {
              switch(element.Name[14])
              {
                case 'A':
                {
                  ExecutePathLinetoVerticalAbs(element, paths);
                  return;
                }
                case 'R':
                {
                  ExecutePathLinetoVerticalRel(element, paths);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'm':
        {
          switch(element.Name[6])
          {
            case 'A':
            {
              ExecutePathMovetoAbs(element, paths);
              return;
            }
            case 'R':
            {
              ExecutePathMovetoRel(element, paths);
              return;
            }
          }
          break;
        }
        case 'q':
        {
          switch(element.Name[16])
          {
            case 'A':
            {
              ExecutePathQuadraticCurvetoAbs(element, paths);
              return;
            }
            case 'R':
            {
              ExecutePathQuadraticCurvetoRel(element, paths);
              return;
            }
          }
          break;
        }
        case 's':
        {
          switch(element.Name[6])
          {
            case 'C':
            {
              switch(element.Name[13])
              {
                case 'A':
                {
                  ExecutePathSmoothCurvetoAbs(element, paths);
                  return;
                }
                case 'R':
                {
                  ExecutePathSmoothCurvetoRel(element, paths);
                  return;
                }
              }
              break;
            }
            case 'Q':
            {
              switch(element.Name[22])
              {
                case 'A':
                {
                  ExecutePathSmoothQuadraticCurvetoAbs(element, paths);
                  return;
                }
                case 'R':
                {
                  ExecutePathSmoothQuadraticCurvetoRel(element, paths);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
      }
      throw new NotImplementedException(element.Name);
    }

    private void ExecutePathArcAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathArc> pathArcs_ = CreatePathArcs(element);
      paths.Add(new PathArcAbs(pathArcs_));
    }

    private void ExecutePathArcRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathArc> pathArcs_ = CreatePathArcs(element);
      paths.Add(new PathArcRel(pathArcs_));
    }

    private void ExecutePathCurvetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathCurveto> pathCurvetos_ = CreatePathCurvetos(element);
      paths.Add(new PathCurvetoAbs(pathCurvetos_));
    }

    private void ExecutePathCurvetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathCurveto> pathCurvetos_ = CreatePathCurvetos(element);
      paths.Add(new PathCurvetoRel(pathCurvetos_));
    }

    private void ExecutePathLinetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathLinetoAbs(coordinates_));
    }

    private void ExecutePathLinetoHorizontalAbs(XmlElement element, Collection<IPath> paths)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      paths.Add(new PathLinetoHorizontalAbs(x_));
    }

    private void ExecutePathLinetoHorizontalRel(XmlElement element, Collection<IPath> paths)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      paths.Add(new PathLinetoHorizontalRel(x_));
    }

    private void ExecutePathLinetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathLinetoRel(coordinates_));
    }

    private void ExecutePathLinetoVerticalAbs(XmlElement element, Collection<IPath> paths)
    {
      double y_ = Variables.GetValue<double>(element, "y");
      paths.Add(new PathLinetoVerticalAbs(y_));
    }

    private void ExecutePathLinetoVerticalRel(XmlElement element, Collection<IPath> paths)
    {
      double y_ = Variables.GetValue<double>(element, "y");
      paths.Add(new PathLinetoVerticalRel(y_));
    }

    private void ExecutePathMovetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathMovetoAbs(coordinates_));
    }

    private void ExecutePathMovetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathMovetoRel(coordinates_));
    }

    private void ExecutePathQuadraticCurvetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathQuadraticCurveto> pathQuadraticCurvetos_ = CreatePathQuadraticCurvetos(element);
      paths.Add(new PathQuadraticCurvetoAbs(pathQuadraticCurvetos_));
    }

    private void ExecutePathQuadraticCurvetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<PathQuadraticCurveto> pathQuadraticCurvetos_ = CreatePathQuadraticCurvetos(element);
      paths.Add(new PathQuadraticCurvetoRel(pathQuadraticCurvetos_));
    }

    private void ExecutePathSmoothCurvetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathSmoothCurvetoAbs(coordinates_));
    }

    private void ExecutePathSmoothCurvetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathSmoothCurvetoRel(coordinates_));
    }

    private void ExecutePathSmoothQuadraticCurvetoAbs(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathSmoothQuadraticCurvetoAbs(coordinates_));
    }

    private void ExecutePathSmoothQuadraticCurvetoRel(XmlElement element, Collection<IPath> paths)
    {
      IEnumerable<Coordinate> coordinates_ = CreateCoordinates(element);
      paths.Add(new PathSmoothQuadraticCurvetoRel(coordinates_));
    }
  }
}
