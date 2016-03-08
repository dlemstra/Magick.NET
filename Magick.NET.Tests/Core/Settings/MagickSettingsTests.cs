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
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickSettingsTests
  {
    private const string _Category = "MagickSettings";

    [TestMethod, TestCategory(_Category)]
    public void Test_Dispose()
    {
      using (MagickImage image = new MagickImage())
      {
        ((IDisposable)image.Settings).Dispose();

        ExceptionAssert.Throws<ObjectDisposedException>(delegate ()
        {
          image.Settings.ColorSpace = ColorSpace.CMYK;
        });
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_FillColor()
    {
      using (MagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
      {
        image.Settings.FillColor = null;

        Pixel pixelA;
        image.Settings.FillColor = MagickColors.Red;
        image.Read("caption:Magick.NET");
        using (PixelCollection pixels = image.GetPixels())
        {
          pixelA = pixels.GetPixel(69, 6);
        }

        Pixel pixelB;
        image.Settings.FillColor = MagickColors.Yellow;
        image.Read("caption:Magick.NET");
        using (PixelCollection pixels = image.GetPixels())
        {
          pixelB = pixels.GetPixel(69, 6);
        }

        ColorAssert.AreNotEqual(pixelA.ToColor(), pixelB.ToColor());
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Properties()
    {
      using (MagickImage image = new MagickImage())
      {
        MagickSettings settings = image.Settings;

        Assert.AreEqual(true, settings.Adjoin);
        settings.Adjoin = false;
        Assert.AreEqual(false, settings.Adjoin);

        MagickColor alphaColor = new MagickColor("#bd");
        ColorAssert.AreEqual(alphaColor, settings.AlphaColor);
        alphaColor = MagickColors.Tan;
        settings.AlphaColor = alphaColor;
        ColorAssert.AreEqual(alphaColor, settings.AlphaColor);

        MagickColor backgroundColor = new MagickColor("white");
        ColorAssert.AreEqual(backgroundColor, settings.BackgroundColor);
        backgroundColor = new MagickColor("purple");
        settings.BackgroundColor = backgroundColor;
        ColorAssert.AreEqual(backgroundColor, settings.BackgroundColor);

        MagickColor borderColor = new MagickColor("#df");
        ColorAssert.AreEqual(borderColor, settings.BorderColor);
        borderColor = new MagickColor("orange");
        settings.BorderColor = borderColor;
        ColorAssert.AreEqual(borderColor, settings.BorderColor);

        Assert.AreEqual(ColorSpace.Undefined, settings.ColorSpace);
        settings.ColorSpace = ColorSpace.CMYK;
        Assert.AreEqual(ColorSpace.CMYK, settings.ColorSpace);

        Assert.AreEqual(ColorType.Undefined, settings.ColorType);
        settings.ColorType = ColorType.ColorSeparation;
        Assert.AreEqual(ColorType.ColorSeparation, settings.ColorType);

        Assert.AreEqual(CompressionMethod.Undefined, settings.CompressionMethod);
        settings.CompressionMethod = CompressionMethod.BZip;
        Assert.AreEqual(CompressionMethod.BZip, settings.CompressionMethod);

        Assert.AreEqual(false, settings.Debug);
        settings.Debug = true;
        Assert.AreEqual(true, settings.Debug);
        settings.Debug = false;

        PointD density = new PointD(72);
        Assert.AreEqual(density, settings.Density);
        density = new PointD(100);
        settings.Density = density;
        Assert.AreEqual(density, settings.Density);

        density = new PointD(50, 10);
        settings.Density = density;
        Assert.AreEqual(density, settings.Density);

        Assert.AreEqual(Endian.Undefined, settings.Endian);
        settings.Endian = Endian.MSB;
        Assert.AreEqual(Endian.MSB, settings.Endian);

        MagickColor fillColor = MagickColors.Black;
        ColorAssert.AreEqual(fillColor, settings.FillColor);
        fillColor = new MagickColor("purple");
        settings.FillColor = fillColor;
        ColorAssert.AreEqual(fillColor, settings.FillColor);

        using (MagickImage fillPattern = new MagickImage(Files.CirclePNG))
        {
          Assert.AreEqual(null, settings.FillPattern);
          settings.FillPattern = fillPattern;
          Assert.AreEqual(fillPattern, settings.FillPattern);
        }

        Assert.AreEqual(FillRule.EvenOdd, settings.FillRule);
        settings.FillRule = FillRule.Nonzero;
        Assert.AreEqual(FillRule.Nonzero, settings.FillRule);

        Assert.AreEqual(null, settings.Font);
        settings.Font = "Comic Sans";
        Assert.AreEqual("Comic Sans", settings.Font);

        Assert.AreEqual(null, settings.FontFamily);
        settings.FontFamily = "Trajan";
        Assert.AreEqual("Trajan", settings.FontFamily);

        Assert.AreEqual(0, settings.FontPointsize);
        settings.FontPointsize = 60;
        Assert.AreEqual(60, settings.FontPointsize);

        Assert.AreEqual(FontStyleType.Undefined, settings.FontStyle);
        settings.FontStyle = FontStyleType.Oblique;
        Assert.AreEqual(FontStyleType.Oblique, settings.FontStyle);

        Assert.AreEqual(FontWeight.Undefined, settings.FontWeight);
        settings.FontWeight = FontWeight.ExtraBold;
        Assert.AreEqual(FontWeight.ExtraBold, settings.FontWeight);

        Assert.AreEqual(MagickFormat.Unknown, settings.Format);
        settings.Format = MagickFormat.WebP;
        Assert.AreEqual(MagickFormat.WebP, settings.Format);

        MagickGeometry page = new MagickGeometry(0, 0, 0, 0);
        Assert.AreEqual(page, settings.Page);
        page = new MagickGeometry(0, 0, 10, 10);
        settings.Page = page;
        Assert.AreEqual(page, settings.Page);

        Assert.AreEqual(true, settings.StrokeAntiAlias);
        settings.StrokeAntiAlias = false;
        Assert.AreEqual(false, settings.StrokeAntiAlias);

        MagickColor strokeColor = new MagickColor(0, 0, 0, 0);
        ColorAssert.AreEqual(strokeColor, settings.StrokeColor);
        strokeColor = MagickColors.Fuchsia;
        settings.StrokeColor = strokeColor;
        ColorAssert.AreEqual(strokeColor, settings.StrokeColor);

        Assert.AreEqual(null, settings.StrokeDashArray);
        List<double> dashArray = new List<double>(new double[] { 1.0, 2.0, 3.0 });
        settings.StrokeDashArray = dashArray;
        CollectionAssert.AreEqual(dashArray, new List<double>(settings.StrokeDashArray));

        Assert.AreEqual(0, settings.StrokeDashOffset);
        settings.StrokeDashOffset = 5;
        Assert.AreEqual(5, settings.StrokeDashOffset);

        Assert.AreEqual(LineCap.Butt, settings.StrokeLineCap);
        settings.StrokeLineCap = LineCap.Round;
        Assert.AreEqual(LineCap.Round, settings.StrokeLineCap);

        Assert.AreEqual(LineJoin.Miter, settings.StrokeLineJoin);
        settings.StrokeLineJoin = LineJoin.Round;
        Assert.AreEqual(LineJoin.Round, settings.StrokeLineJoin);

        Assert.AreEqual(10, settings.StrokeMiterLimit);
        settings.StrokeMiterLimit = 8;
        Assert.AreEqual(8, settings.StrokeMiterLimit);

        using (MagickImage strokePattern = new MagickImage(Files.MagickNETIconPNG))
        {
          Assert.AreEqual(null, settings.StrokePattern);
          settings.StrokePattern = strokePattern;
          Assert.AreEqual(strokePattern, settings.StrokePattern);
        }

        Assert.AreEqual(1, settings.StrokeWidth);
        settings.StrokeWidth = 9.5;
        Assert.AreEqual(9.5, settings.StrokeWidth);

        Assert.AreEqual(true, settings.TextAntiAlias);
        settings.TextAntiAlias = false;
        Assert.AreEqual(false, settings.TextAntiAlias);

        Assert.AreEqual(TextDirection.Undefined, settings.TextDirection);
        settings.TextDirection = TextDirection.RightToLeft;
        Assert.AreEqual(TextDirection.RightToLeft, settings.TextDirection);

        Assert.AreEqual(null, settings.TextEncoding);
        settings.TextEncoding = Encoding.Unicode;
        Assert.AreEqual(Encoding.Unicode, settings.TextEncoding);

        Assert.AreEqual(Gravity.Undefined, settings.TextGravity);
        settings.TextGravity = Gravity.North;
        Assert.AreEqual(Gravity.North, settings.TextGravity);

        Assert.AreEqual(0, settings.TextInterlineSpacing);
        settings.TextInterlineSpacing = 3.4;
        Assert.AreEqual(3.4, settings.TextInterlineSpacing);

        Assert.AreEqual(0, settings.TextInterwordSpacing);
        settings.TextInterwordSpacing = 7.8;
        Assert.AreEqual(7.8, settings.TextInterwordSpacing);

        Assert.AreEqual(0, settings.TextKerning);
        settings.TextKerning = 6.2;
        Assert.AreEqual(6.2, settings.TextKerning);

        MagickColor underColor = new MagickColor(0, 0, 0, 0);
        ColorAssert.AreEqual(underColor, settings.TextUnderColor);
        underColor = new MagickColor("yellow");
        settings.TextUnderColor = underColor;
        ColorAssert.AreEqual(underColor, settings.TextUnderColor);

        Assert.AreEqual(false, settings.Verbose);
        settings.Verbose = true;
        Assert.AreEqual(true, settings.Verbose);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_StrokeWidth()
    {
      using (MagickImage image = new MagickImage(MagickColors.Purple, 300, 300))
      {
        image.Settings.StrokeWidth = 40;
        image.Settings.StrokeColor = MagickColors.Orange;
        image.Draw(new DrawableCircle(150, 150, 100, 100));

        ColorAssert.AreEqual(MagickColors.Black, image, 150, 150);
        ColorAssert.AreEqual(MagickColors.Orange, image, 201, 150);
        ColorAssert.AreEqual(MagickColors.Purple, image, 244, 150);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TextGravity()
    {
      using (MagickImage image = new MagickImage("xc:red", 300, 300))
      {
        image.Settings.BackgroundColor = MagickColors.Yellow;
        image.Settings.StrokeColor = MagickColors.Fuchsia;
        image.Settings.FillColor = MagickColors.Fuchsia;
        image.Settings.TextGravity = Gravity.Center;

        image.Read("label:Test");

        ColorAssert.AreEqual(MagickColors.Yellow, image, 50, 80);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 50, 160);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TextInterlineSpacing()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.TextInterlineSpacing = 10;
        image.Read("label:First\nSecond");

        Assert.AreEqual(43, image.Width);
        Assert.AreEqual(39, image.Height);

        image.Settings.TextInterlineSpacing = 20;
        image.Read("label:First\nSecond");

        Assert.AreEqual(43, image.Width);
        Assert.AreEqual(49, image.Height);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TextInterwordSpacing()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.TextInterwordSpacing = 10;
        image.Read("label:First second");

        Assert.AreEqual(74, image.Width);
        Assert.AreEqual(15, image.Height);

        image.Settings.TextInterwordSpacing = 20;
        image.Read("label:First second");

        Assert.AreEqual(84, image.Width);
        Assert.AreEqual(15, image.Height);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TextKerning()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.TextKerning = 10;
        image.Read("label:First");

        Assert.AreEqual(66, image.Width);
        Assert.AreEqual(15, image.Height);

        image.Settings.TextKerning = 20;
        image.Read("label:First");

        Assert.AreEqual(106, image.Width);
        Assert.AreEqual(15, image.Height);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TextUnderColor()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.TextUnderColor = MagickColors.Purple;
        image.Read("label:First");

        Assert.AreEqual(26, image.Width);
        Assert.AreEqual(15, image.Height);

        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.White, image, 24, 0);
      }
    }
  }
}
