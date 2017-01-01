//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class PathTests
  {
    private void Test_Paths_Draw(IPath path)
    {
      path.Draw(null);
    }

    [TestMethod]
    public void Test_Paths()
    {
      using (MagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
      {
        List<IPath> paths = new List<IPath>();
        paths.Add(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
        paths.Add(new PathArcAbs(new PathArc[] { new PathArc(50, 50, 20, 20, 45, true, false) }.ToList()));
        paths.Add(new PathArcRel(new PathArc(10, 10, 5, 5, 40, false, true)));
        paths.Add(new PathArcRel(new PathArc[] { new PathArc(10, 10, 5, 5, 40, false, true) }.ToList()));
        paths.Add(new PathClose());
        paths.Add(new PathCurveToAbs(80, 80, 10, 10, 60, 60));
        paths.Add(new PathCurveToRel(30, 30, 60, 60, 90, 90));
        paths.Add(new PathLineToAbs(new PointD(70, 70)));
        paths.Add(new PathLineToAbs(new PointD[] { new PointD(70, 70) }.ToList()));
        paths.Add(new PathLineToHorizontalAbs(20));
        paths.Add(new PathLineToHorizontalRel(90));
        paths.Add(new PathLineToRel(new PointD(0, 0)));
        paths.Add(new PathLineToRel(new PointD[] { new PointD(0, 0) }.ToList()));
        paths.Add(new PathLineToVerticalAbs(70));
        paths.Add(new PathLineToVerticalRel(30));
        paths.Add(new PathMoveToAbs(new PointD(50, 50)));
        paths.Add(new PathMoveToAbs(new PointD(50, 50)));
        paths.Add(new PathMoveToRel(new PointD(20, 20)));
        paths.Add(new PathMoveToRel(20, 20));
        paths.Add(new PathQuadraticCurveToAbs(70, 70, 30, 30));
        paths.Add(new PathQuadraticCurveToRel(10, 10, 40, 40));
        paths.Add(new PathSmoothCurveToAbs(0, 0, 30, 30));
        paths.Add(new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)));
        paths.Add(new PathSmoothCurveToRel(60, 60, 10, 10));
        paths.Add(new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)));
        paths.Add(new PathSmoothQuadraticCurveToAbs(50, 50));
        paths.Add(new PathSmoothQuadraticCurveToRel(80, 80));

        image.Draw(new DrawablePath(paths));
      }
    }

    [TestMethod]
    public void Test_Paths_Draw()
    {
      Test_Paths_Draw(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
      Test_Paths_Draw(new PathArcRel(new PathArc()));
      Test_Paths_Draw(new PathClose());
      Test_Paths_Draw(new PathCurveToAbs(80, 80, 10, 10, 60, 60));
      Test_Paths_Draw(new PathCurveToRel(30, 30, 60, 60, 90, 90));
      Test_Paths_Draw(new PathLineToAbs(new PointD(70, 70)));
      Test_Paths_Draw(new PathLineToHorizontalAbs(20));
      Test_Paths_Draw(new PathLineToHorizontalRel(90));
      Test_Paths_Draw(new PathLineToRel(new PointD(0, 0)));
      Test_Paths_Draw(new PathLineToVerticalAbs(70));
      Test_Paths_Draw(new PathLineToVerticalRel(30));
      Test_Paths_Draw(new PathMoveToAbs(new PointD(50, 50)));
      Test_Paths_Draw(new PathMoveToRel(new PointD(20, 20)));
      Test_Paths_Draw(new PathQuadraticCurveToAbs(70, 70, 30, 30));
      Test_Paths_Draw(new PathQuadraticCurveToRel(10, 10, 40, 40));
      Test_Paths_Draw(new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)));
      Test_Paths_Draw(new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)));
      Test_Paths_Draw(new PathSmoothQuadraticCurveToAbs(50, 50));
      Test_Paths_Draw(new PathSmoothQuadraticCurveToRel(80, 80));
    }

    [TestMethod]
    public void Test_Path_Exceptions()
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
        new PathArcRel();
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathArcRel(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathArcRel(new PathArc[] { });
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathArcRel(new PathArc[] { null });
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathLineToAbs();
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathLineToAbs(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathLineToAbs(new PointD[] { });
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathLineToRel();
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PathLineToRel(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PathLineToRel(new PointD[] { });
      });
    }
  }
}
