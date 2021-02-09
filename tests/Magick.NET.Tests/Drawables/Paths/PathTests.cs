// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class PathTests
    {
        [Fact]
        public void Test_Paths()
        {
            using (var image = new MagickImage(MagickColors.Transparent, 100, 100))
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

        [Fact]
        public void Test_Paths_Draw()
        {
            AssertDraw(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
            AssertDraw(new PathArcRel(new PathArc()));
            AssertDraw(new PathClose());
            AssertDraw(new PathCurveToAbs(80, 80, 10, 10, 60, 60));
            AssertDraw(new PathCurveToRel(30, 30, 60, 60, 90, 90));
            AssertDraw(new PathLineToAbs(new PointD(70, 70)));
            AssertDraw(new PathLineToHorizontalAbs(20));
            AssertDraw(new PathLineToHorizontalRel(90));
            AssertDraw(new PathLineToRel(new PointD(0, 0)));
            AssertDraw(new PathLineToVerticalAbs(70));
            AssertDraw(new PathLineToVerticalRel(30));
            AssertDraw(new PathMoveToAbs(new PointD(50, 50)));
            AssertDraw(new PathMoveToRel(new PointD(20, 20)));
            AssertDraw(new PathQuadraticCurveToAbs(70, 70, 30, 30));
            AssertDraw(new PathQuadraticCurveToRel(10, 10, 40, 40));
            AssertDraw(new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)));
            AssertDraw(new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)));
            AssertDraw(new PathSmoothQuadraticCurveToAbs(50, 50));
            AssertDraw(new PathSmoothQuadraticCurveToRel(80, 80));
        }

        [Fact]
        public void Test_Path_Exceptions()
        {
            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathArcAbs();
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathArcAbs(null);
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathArcAbs(new PathArc[] { });
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathArcAbs(new PathArc[] { null });
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathArcRel();
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathArcRel(null);
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathArcRel(new PathArc[] { });
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathArcRel(new PathArc[] { null });
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathLineToAbs();
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathLineToAbs(null);
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathLineToAbs(new PointD[] { });
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathLineToRel();
            });

            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new PathLineToRel(null);
            });

            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new PathLineToRel(new PointD[] { });
            });
        }

        private void AssertDraw(IPath path)
        {
            ((IDrawingWand)path).Draw(null);
        }
    }
}
