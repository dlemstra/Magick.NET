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
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class DrawableTests
  {
    private const string _Category = "DrawableTests";

    [TestMethod, TestCategory(_Category)]
    public void Test_Drawables()
    {
      PointD[] coordinates = new PointD[3];
      coordinates[0] = new PointD(0, 0);
      coordinates[1] = new PointD(50, 50);
      coordinates[2] = new PointD(99, 99);

      using (MagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
      {
        image.Draw(new DrawableAffine(0, 0, 1, 1, 2, 2));
        image.Draw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
        image.Draw(new DrawableArc(0, 0, 10, 10, 45, 90));
        image.Draw(new DrawableBezier(coordinates));
        image.Draw(new DrawableBorderColor(MagickColors.Fuchsia));
        image.Draw(new DrawableCircle(0, 0, 50, 50));
        image.Draw(new DrawableClipPath("foo"));
        image.Draw(new DrawableClipRule(FillRule.Nonzero));
        image.Draw(new DrawableClipUnits(ClipPathUnit.UserSpaceOnUse));
        image.Draw(new DrawableColor(0, 0, PaintMethod.Floodfill));

        using (MagickImage compositeImage = new MagickImage(new MagickColor("red"), 50, 50))
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
        image.Draw(new DrawableFont("Arial"));
        image.Draw(new DrawableGravity(Gravity.Center));
        image.Draw(new DrawableLine(20, 20, 40, 40));
        image.Draw(new DrawablePoint(60, 60));
        image.Draw(new DrawableFontPointSize(5));
        image.Draw(new DrawablePolygon(coordinates));
        image.Draw(new DrawablePolyline(coordinates));
        image.Draw(new DrawableRectangle(30, 30, 70, 70));
        image.Draw(new DrawableRotation(180));
        image.Draw(new DrawableRoundRectangle(50, 50, 30, 30, 70, 70));
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Drawables_Exceptions()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawableBezier();
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableBezier(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawableBezier(new PointD[] { });
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableClipPath(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawableClipPath("");
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableComposite(null, new MagickImage(Files.Builtin.Logo));
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableComposite(new MagickGeometry(), null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableFillColor(null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableFont(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawableFont("");
      });

      ExceptionAssert.Throws<MagickDrawErrorException>(delegate ()
      {
        using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
        {
          image.Draw(new DrawableFillPatternUrl("#fail"));
        }
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawablePolygon(new PointD[] { new PointD(0, 0) });
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawablePolyline(new PointD[] { new PointD(0, 0), new PointD(0, 0) });
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableStrokeColor(null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableText(0, 0, null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new DrawableText(0, 0, "");
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new DrawableTextEncoding(null);
      });
    }
  }
}
