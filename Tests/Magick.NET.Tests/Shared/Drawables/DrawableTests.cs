// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class DrawableTests
    {
        [TestMethod]
        public void Test_Drawables()
        {
            PointD[] coordinates = new PointD[3];
            coordinates[0] = new PointD(0, 0);
            coordinates[1] = new PointD(50, 50);
            coordinates[2] = new PointD(99, 99);

            using (IMagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
            {
                image.Draw(new DrawableAffine(0, 0, 1, 1, 2, 2));
                image.Draw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
                image.Draw(new DrawableArc(0, 0, 10, 10, 45, 90));

                var bezier = new DrawableBezier(coordinates.ToList());
                Assert.AreEqual(3, bezier.Coordinates.Count());
                image.Draw(bezier);

                image.Draw(new DrawableBorderColor(MagickColors.Fuchsia));
                image.Draw(new DrawableCircle(0, 0, 50, 50));
                image.Draw(new DrawableClipPath("foo"));
                image.Draw(new DrawableClipRule(FillRule.Nonzero));
                image.Draw(new DrawableClipUnits(ClipPathUnit.UserSpaceOnUse));
                image.Draw(new DrawableColor(0, 0, PaintMethod.Floodfill));

                using (IMagickImage compositeImage = new MagickImage(new MagickColor("red"), 50, 50))
                {
                    image.Draw(new DrawableComposite(0, 0, compositeImage));
                    image.Draw(new DrawableComposite(0, 0, CompositeOperator.Over, compositeImage));
                    image.Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), compositeImage));
                    image.Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), CompositeOperator.Over, compositeImage));
                }

                image.Draw(new DrawableDensity(97));
                image.Draw(new DrawableEllipse(10, 10, 4, 4, 0, 360));
                image.Draw(new DrawableFillColor(MagickColors.Red));
                image.Draw(new DrawableFillOpacity(new Percentage(50)));
                image.Draw(new DrawableFillRule(FillRule.EvenOdd));
                image.Draw(new DrawableFont("Arial.ttf"));
                image.Draw(new DrawableFont("Arial"));
                image.Draw(new DrawableGravity(Gravity.Center));
                image.Draw(new DrawableLine(20, 20, 40, 40));
                image.Draw(new DrawablePoint(60, 60));
                image.Draw(new DrawableFontPointSize(5));
                image.Draw(new DrawablePolygon(coordinates));
                image.Draw(new DrawablePolygon(coordinates.ToList()));
                image.Draw(new DrawablePolyline(coordinates));
                image.Draw(new DrawablePolyline(coordinates.ToList()));
                image.Draw(new DrawableRectangle(30, 30, 70, 70));
                image.Draw(new DrawableRotation(180));
                image.Draw(new DrawableRoundRectangle(30, 30, 50, 50, 70, 70));
                image.Draw(new DrawableScaling(15, 15));
                image.Draw(new DrawableSkewX(90));
                image.Draw(new DrawableSkewY(90));
                image.Draw(new DrawableStrokeAntialias(true));
                image.Draw(new DrawableStrokeColor(MagickColors.Purple));
                image.Draw(new DrawableStrokeDashArray(new double[2] { 10, 20 }));
                image.Draw(new DrawableStrokeDashOffset(2));
                image.Draw(new DrawableStrokeLineCap(LineCap.Square));
                image.Draw(new DrawableStrokeLineJoin(LineJoin.Bevel));
                image.Draw(new DrawableStrokeMiterLimit(5));
                image.Draw(new DrawableStrokeOpacity(new Percentage(80)));
                image.Draw(new DrawableStrokeWidth(4));
                image.Draw(new DrawableText(0, 60, "test"));
                image.Draw(new DrawableTextAlignment(TextAlignment.Center));
                image.Draw(new DrawableTextAntialias(true));
                image.Draw(new DrawableTextDecoration(TextDecoration.LineThrough));
                image.Draw(new DrawableTextDirection(TextDirection.RightToLeft));
                image.Draw(new DrawableTextEncoding(Encoding.ASCII));
                image.Draw(new DrawableTextInterlineSpacing(4));
                image.Draw(new DrawableTextInterwordSpacing(6));
                image.Draw(new DrawableTextKerning(2));
                image.Draw(new DrawableTextUnderColor(MagickColors.Yellow));
                image.Draw(new DrawableTranslation(65, 65));
                image.Draw(new DrawableViewbox(0, 0, 100, 100));

                image.Draw(new DrawablePushClipPath("#1"), new DrawablePopClipPath());
                image.Draw(new DrawablePushGraphicContext(), new DrawablePopGraphicContext());
                image.Draw(new DrawablePushPattern("test", 30, 30, 10, 10), new DrawablePopPattern(), new DrawableFillPatternUrl("#test"), new DrawableStrokePatternUrl("#test"));
            }
        }

        [TestMethod]
        public void Test_Drawables_Draw()
        {
            PointD[] coordinates = new PointD[3];
            coordinates[0] = new PointD(0, 0);
            coordinates[1] = new PointD(50, 50);
            coordinates[2] = new PointD(99, 99);

            Test_Drawables_Draw(new DrawableAffine(0, 0, 1, 1, 2, 2));
            Test_Drawables_Draw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
            Test_Drawables_Draw(new DrawableArc(0, 0, 10, 10, 45, 90));
            Test_Drawables_Draw(new DrawableBezier(coordinates));
            Test_Drawables_Draw(new DrawableBorderColor(MagickColors.Fuchsia));
            Test_Drawables_Draw(new DrawableCircle(0, 0, 50, 50));
            Test_Drawables_Draw(new DrawableClipPath("foo"));
            Test_Drawables_Draw(new DrawableClipRule(FillRule.Nonzero));
            Test_Drawables_Draw(new DrawableClipUnits(ClipPathUnit.UserSpaceOnUse));
            Test_Drawables_Draw(new DrawableColor(0, 0, PaintMethod.Floodfill));

            using (IMagickImage compositeImage = new MagickImage(new MagickColor("red"), 50, 50))
            {
                Test_Drawables_Draw(new DrawableComposite(0, 0, compositeImage));
                Test_Drawables_Draw(new DrawableComposite(0, 0, CompositeOperator.Over, compositeImage));
                Test_Drawables_Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), compositeImage));
                Test_Drawables_Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), CompositeOperator.Over, compositeImage));
            }

            Test_Drawables_Draw(new DrawableDensity(97));
            Test_Drawables_Draw(new DrawableEllipse(10, 10, 4, 4, 0, 360));
            Test_Drawables_Draw(new DrawableFillColor(MagickColors.Red));
            Test_Drawables_Draw(new DrawableFillOpacity(new Percentage(50)));
            Test_Drawables_Draw(new DrawableFillRule(FillRule.EvenOdd));
            Test_Drawables_Draw(new DrawableFont("Arial"));
            Test_Drawables_Draw(new DrawableGravity(Gravity.Center));
            Test_Drawables_Draw(new DrawableLine(20, 20, 40, 40));
            Test_Drawables_Draw(new DrawablePoint(60, 60));
            Test_Drawables_Draw(new DrawableFontPointSize(5));
            Test_Drawables_Draw(new DrawablePolygon(coordinates));
            Test_Drawables_Draw(new DrawablePolyline(coordinates));
            Test_Drawables_Draw(new DrawableRectangle(30, 30, 70, 70));
            Test_Drawables_Draw(new DrawableRotation(180));
            Test_Drawables_Draw(new DrawableRoundRectangle(30, 30, 50, 50, 70, 70));
            Test_Drawables_Draw(new DrawableScaling(15, 15));
            Test_Drawables_Draw(new DrawableSkewX(90));
            Test_Drawables_Draw(new DrawableSkewY(90));
            Test_Drawables_Draw(new DrawableStrokeAntialias(true));
            Test_Drawables_Draw(new DrawableStrokeColor(MagickColors.Purple));
            Test_Drawables_Draw(new DrawableStrokeDashArray(new double[2] { 10, 20 }));
            Test_Drawables_Draw(new DrawableStrokeDashOffset(2));
            Test_Drawables_Draw(new DrawableStrokeLineCap(LineCap.Square));
            Test_Drawables_Draw(new DrawableStrokeLineJoin(LineJoin.Bevel));
            Test_Drawables_Draw(new DrawableStrokeMiterLimit(5));
            Test_Drawables_Draw(new DrawableStrokeOpacity(new Percentage(80)));
            Test_Drawables_Draw(new DrawableStrokeWidth(4));
            Test_Drawables_Draw(new DrawableText(0, 60, "test"));
            Test_Drawables_Draw(new DrawableTextAlignment(TextAlignment.Center));
            Test_Drawables_Draw(new DrawableTextAntialias(true));
            Test_Drawables_Draw(new DrawableTextDecoration(TextDecoration.LineThrough));
            Test_Drawables_Draw(new DrawableTextDirection(TextDirection.RightToLeft));
            Test_Drawables_Draw(new DrawableTextEncoding(Encoding.ASCII));
            Test_Drawables_Draw(new DrawableTextInterlineSpacing(4));
            Test_Drawables_Draw(new DrawableTextInterwordSpacing(6));
            Test_Drawables_Draw(new DrawableTextKerning(2));
            Test_Drawables_Draw(new DrawableTextUnderColor(MagickColors.Yellow));
            Test_Drawables_Draw(new DrawableTranslation(65, 65));
            Test_Drawables_Draw(new DrawableViewbox(0, 0, 100, 100));

            Test_Drawables_Draw(new DrawablePushClipPath("#1"));
            Test_Drawables_Draw(new DrawablePopClipPath());
            Test_Drawables_Draw(new DrawablePushGraphicContext());
            Test_Drawables_Draw(new DrawablePopGraphicContext());
            Test_Drawables_Draw(new DrawablePushPattern("test", 30, 30, 10, 10));
            Test_Drawables_Draw(new DrawablePopPattern());
            Test_Drawables_Draw(new DrawableFillPatternUrl("#test"));
            Test_Drawables_Draw(new DrawableStrokePatternUrl("#test"));
        }

        [TestMethod]
        public void Test_Drawables_Exceptions()
        {
            ExceptionAssert.ThrowsArgumentException("coordinates", () =>
            {
                new DrawableBezier();
            });

            ExceptionAssert.ThrowsArgumentNullException("coordinates", () =>
            {
                new DrawableBezier(null);
            });

            ExceptionAssert.ThrowsArgumentException("coordinates", () =>
            {
                new DrawableBezier(new PointD[] { });
            });

            ExceptionAssert.ThrowsArgumentNullException("clipPath", () =>
            {
                new DrawableClipPath(null);
            });

            ExceptionAssert.ThrowsArgumentException("clipPath", () =>
            {
                new DrawableClipPath(string.Empty);
            });

            ExceptionAssert.ThrowsArgumentNullException("offset", () =>
            {
                new DrawableComposite(null, new MagickImage(Files.Builtin.Logo));
            });

            ExceptionAssert.ThrowsArgumentNullException("image", () =>
            {
                new DrawableComposite(new MagickGeometry(), null);
            });

            ExceptionAssert.ThrowsArgumentNullException("color", () =>
            {
                new DrawableFillColor(null);
            });

            ExceptionAssert.ThrowsArgumentNullException("family", () =>
            {
                new DrawableFont(null);
            });

            ExceptionAssert.ThrowsArgumentException("family", () =>
            {
                new DrawableFont(string.Empty);
            });

            ExceptionAssert.Throws<MagickDrawErrorException>(() =>
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Wizard))
                {
                    image.Draw(new DrawableFillPatternUrl("#fail"));
                }
            });

            ExceptionAssert.ThrowsArgumentException("coordinates", () =>
            {
                new DrawablePolygon(new PointD[] { new PointD(0, 0) });
            });

            ExceptionAssert.ThrowsArgumentException("coordinates", () =>
            {
                new DrawablePolyline(new PointD[] { new PointD(0, 0), new PointD(0, 0) });
            });

            ExceptionAssert.ThrowsArgumentNullException("color", () =>
            {
                new DrawableStrokeColor(null);
            });

            ExceptionAssert.ThrowsArgumentNullException("value", () =>
            {
                new DrawableText(0, 0, null);
            });

            ExceptionAssert.ThrowsArgumentException("value", () =>
            {
                new DrawableText(0, 0, string.Empty);
            });

            ExceptionAssert.ThrowsArgumentNullException("encoding", () =>
            {
                new DrawableTextEncoding(null);
            });
        }

        private void Test_Drawables_Draw(IDrawable drawable)
        {
            ((IDrawingWand)drawable).Draw(null);
        }
    }
}
