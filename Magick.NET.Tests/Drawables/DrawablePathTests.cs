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
using System.Collections.Generic;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class DrawablePathTestss
  {
    private const string _Category = "DrawablePathTests";

    [TestMethod, TestCategory(_Category)]
    public void Test_DrawablePaths()
    {
      using (MagickImage image = new MagickImage(MagickColor.Transparent, 100, 100))
      {
        List<IPath> paths = new List<IPath>();
        paths.Add(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
        paths.Add(new PathArcRel(new PathArc(10, 10, 5, 5, 40, false, true)));
        paths.Add(new PathClosePath());
        paths.Add(new PathCurvetoAbs(new PathCurveto(80, 80, 10, 10, 60, 60)));
        paths.Add(new PathCurvetoRel(new PathCurveto(30, 30, 60, 60, 90, 90)));
        paths.Add(new PathLinetoAbs(new Coordinate(70, 70)));
        paths.Add(new PathLinetoHorizontalAbs(20));
        paths.Add(new PathLinetoHorizontalRel(90));
        paths.Add(new PathLinetoRel(new Coordinate(0, 0)));
        paths.Add(new PathLinetoVerticalAbs(70));
        paths.Add(new PathLinetoVerticalRel(30));
        paths.Add(new PathMovetoAbs(new Coordinate(50, 50)));
        paths.Add(new PathMovetoRel(new Coordinate(20, 20)));
        paths.Add(new PathQuadraticCurvetoAbs(new PathQuadraticCurveto(70, 70, 30, 30)));
        paths.Add(new PathQuadraticCurvetoRel(new PathQuadraticCurveto(10, 10, 40, 40)));
        paths.Add(new PathSmoothCurvetoAbs(new Coordinate(0, 0), new Coordinate(30, 30)));
        paths.Add(new PathSmoothCurvetoRel(new Coordinate(60, 60), new Coordinate(10, 10)));
        paths.Add(new PathSmoothQuadraticCurvetoAbs(new Coordinate(50, 50)));
        paths.Add(new PathSmoothQuadraticCurvetoRel(new Coordinate(80, 80)));

        image.Draw(new DrawablePath(paths));
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_DrawablePath_Exceptions()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathArcAbs();
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathArcAbs(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathArcAbs(new PathArc[] { });
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathArcAbs(new PathArc[] { null });
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathSmoothCurvetoAbs(new Coordinate[] { new Coordinate(0, 0) });
      });
    }
  }
}
