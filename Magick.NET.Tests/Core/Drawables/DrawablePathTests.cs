//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
  public class DrawablePathTests
  {
    private const string _Category = "DrawablePathTests";

    [TestMethod, TestCategory(_Category)]
    public void Test_DrawablePaths()
    {
      using (MagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
      {
        List<IPath> paths = new List<IPath>();
        paths.Add(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
        paths.Add(new PathArcRel(new PathArc(10, 10, 5, 5, 40, false, true)));
        paths.Add(new PathClose());
        paths.Add(new PathCurveToAbs(80, 80, 10, 10, 60, 60));
        paths.Add(new PathCurveToRel(30, 30, 60, 60, 90, 90));
        paths.Add(new PathLineToAbs(new PointD(70, 70)));
        paths.Add(new PathLineToHorizontalAbs(20));
        paths.Add(new PathLineToHorizontalRel(90));
        paths.Add(new PathLineToRel(new PointD(0, 0)));
        paths.Add(new PathLineToVerticalAbs(70));
        paths.Add(new PathLineToVerticalRel(30));
        paths.Add(new PathMoveToAbs(new PointD(50, 50)));
        paths.Add(new PathMoveToRel(new PointD(20, 20)));
        paths.Add(new PathQuadraticCurveToAbs(70, 70, 30, 30));
        paths.Add(new PathQuadraticCurveToRel(10, 10, 40, 40));
        paths.Add(new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)));
        paths.Add(new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)));
        paths.Add(new PathSmoothQuadraticCurveToAbs(50, 50));
        paths.Add(new PathSmoothQuadraticCurveToRel(80, 80));

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
