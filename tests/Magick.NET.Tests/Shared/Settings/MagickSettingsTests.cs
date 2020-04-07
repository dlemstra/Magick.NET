// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class MagickSettingsTests
    {
        [TestMethod]
        public void Test_Affine()
        {
            using (IMagickImage image = new MagickImage(MagickColors.White, 300, 300))
            {
                Assert.AreEqual(null, image.Settings.Affine);

                image.Annotate("Magick.NET", Gravity.Center);
                ColorAssert.AreEqual(MagickColors.White, image, 200, 200);

                image.Settings.Affine = new DrawableAffine(10, 20, 30, 40, 50, 60);
                image.Annotate("Magick.NET", Gravity.Center);
                ColorAssert.AreEqual(MagickColors.Black, image, 200, 200);
            }
        }

        [TestMethod]
        public void Test_BorderColor()
        {
            using (IMagickImage image = new MagickImage(MagickColors.MediumTurquoise, 10, 10))
            {
                ColorAssert.AreEqual(new MagickColor("#df"), image.Settings.BorderColor);

                image.Settings.FillColor = MagickColors.Beige;
                image.Settings.BorderColor = MagickColors.MediumTurquoise;
                image.Extent(20, 20, Gravity.Center, MagickColors.Aqua);
                image.Draw(new DrawableAlpha(0, 0, PaintMethod.FillToBorder));

                ColorAssert.AreEqual(MagickColors.Beige, image, 0, 0);
                ColorAssert.AreEqual(MagickColors.MediumTurquoise, image, 10, 10);
            }
        }

        [TestMethod]
        public void Test_ColorSpace()
        {
            using (IMagickImage image = new MagickImage())
            {
                Assert.AreEqual(ColorSpace.Undefined, image.Settings.ColorSpace);

                image.Read(Files.ImageMagickJPG);
                ColorAssert.AreEqual(MagickColors.White, image, 0, 0);

                image.Settings.ColorSpace = ColorSpace.Rec601YCbCr;
                image.Read(Files.ImageMagickJPG);
                ColorAssert.AreEqual(new MagickColor("#ff8080"), image, 0, 0);
            }
        }

        [TestMethod]
        public void Test_ColorType()
        {
            using (IMagickImage image = new MagickImage())
            {
                Assert.AreEqual(ColorType.Undefined, image.Settings.ColorType);

                image.Read(Files.Builtin.Wizard);
                image.ColorType = ColorType.Grayscale;

                Assert.AreEqual(ColorType.Grayscale, image.ColorType);
                image.Settings.ColorType = ColorType.TrueColor;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Jpg;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (IMagickImage result = new MagickImage(memStream))
                    {
                        Assert.AreEqual(ColorType.TrueColor, result.ColorType);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_Compression()
        {
            using (IMagickImage image = new MagickImage())
            {
                Assert.AreEqual(CompressionMethod.Undefined, image.Settings.Compression);

                image.Read(Files.Builtin.Wizard);

                image.Settings.Compression = CompressionMethod.NoCompression;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Bmp;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (IMagickImage result = new MagickImage(memStream))
                    {
                        Assert.AreEqual(CompressionMethod.NoCompression, result.Compression);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_Debug()
        {
            using (IMagickImage image = new MagickImage())
            {
                Assert.AreEqual(false, image.Settings.Debug);
            }
        }

        [TestMethod]
        public void Test_Endian()
        {
            using (IMagickImage image = new MagickImage(Files.NoisePNG))
            {
                Assert.AreEqual(Endian.Undefined, image.Settings.Endian);

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Settings.Endian = Endian.MSB;
                    image.Format = MagickFormat.Ipl;
                    image.Write(memStream);
                    memStream.Position = 0;

                    MagickReadSettings settings = new MagickReadSettings();
                    settings.Format = MagickFormat.Ipl;

                    using (IMagickImage result = new MagickImage(memStream, settings))
                    {
                        Assert.AreEqual(Endian.MSB, result.Endian);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_FillColor()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
            {
                ColorAssert.AreEqual(MagickColors.Black, image.Settings.FillColor);

                Pixel pixelA;
                image.Settings.FillColor = MagickColors.Red;
                image.Read("caption:Magick.NET");

                Assert.AreEqual(100, image.Width);
                Assert.AreEqual(100, image.Height);

                using (IPixelCollection pixels = image.GetPixels())
                {
                    pixelA = pixels.GetPixel(64, 6);
                }

                Pixel pixelB;
                image.Settings.FillColor = MagickColors.Yellow;
                image.Read("caption:Magick.NET");
                using (IPixelCollection pixels = image.GetPixels())
                {
                    pixelB = pixels.GetPixel(64, 6);
                }

                ColorAssert.AreNotEqual(pixelA.ToColor(), pixelB.ToColor());
            }
        }

        [TestMethod]
        public void Test_FillPattern()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Transparent, 500, 500))
            {
                Assert.AreEqual(null, image.Settings.FillPattern);

                image.Settings.FillPattern = new MagickImage(Files.SnakewarePNG);

                PointD[] coordinates = new PointD[4];
                coordinates[0] = new PointD(50, 50);
                coordinates[1] = new PointD(150, 50);
                coordinates[2] = new PointD(150, 150);
                coordinates[3] = new PointD(50, 150);
                image.Draw(new DrawablePolygon(coordinates));

                ColorAssert.AreEqual(new MagickColor("#02"), image, 84, 80);
            }
        }

        [TestMethod]
        public void Test_FillRule()
        {
            using (IMagickImage image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.AreEqual(FillRule.EvenOdd, image.Settings.FillRule);

                image.Settings.FillRule = FillRule.Nonzero;
                image.Settings.FillColor = MagickColors.White;
                image.Settings.StrokeColor = MagickColors.Black;
                image.Draw(new DrawablePath(
                  new PathMoveToAbs(40, 10),
                  new PathLineToAbs(20, 20),
                  new PathLineToAbs(70, 50),
                  new PathClose(),
                  new PathMoveToAbs(20, 40),
                  new PathLineToAbs(70, 40),
                  new PathLineToAbs(90, 10),
                  new PathClose()));

                ColorAssert.AreEqual(MagickColors.White, image, 50, 30);
            }
        }

        [TestMethod]
        public void Test_Interlace()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Fuchsia, 100, 60))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Jpeg;
                    image.Write(memStream);

                    memStream.Position = 0;
                    image.Read(memStream);
                    Assert.AreEqual(Interlace.NoInterlace, image.Interlace);
                }

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Interlace = Interlace.Undefined;
                    image.Write(memStream);

                    memStream.Position = 0;
                    image.Read(memStream);
                    Assert.AreEqual(Interlace.Jpeg, image.Interlace);
                }
            }
        }

        [TestMethod]
        public void Test_StrokeAntiAlias()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Purple, 300, 300))
            {
                Assert.AreEqual(true, image.Settings.StrokeAntiAlias);

                image.Settings.StrokeWidth = 20;
                image.Settings.StrokeAntiAlias = false;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Draw(new DrawableCircle(150, 150, 100, 100));

                ColorAssert.AreEqual(MagickColors.Orange, image, 84, 103);
            }
        }

        [TestMethod]
        public void Test_StrokeDashArray()
        {
            using (IMagickImage image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.AreEqual(null, image.Settings.StrokeDashArray);
                Assert.AreEqual(0, image.Settings.StrokeDashOffset);

                image.Settings.StrokeColor = MagickColors.Moccasin;
                image.Settings.StrokeDashArray = new double[] { 5.0, 8.0, 10.0 };
                image.Settings.StrokeDashOffset = 1;

                image.Draw(new DrawablePath(new PathMoveToAbs(10, 20), new PathLineToAbs(90, 20)));

                ColorAssert.AreEqual(MagickColors.Moccasin, image, 13, 20);
                ColorAssert.AreEqual(MagickColors.Moccasin, image, 37, 20);
                ColorAssert.AreEqual(MagickColors.Moccasin, image, 60, 20);
                ColorAssert.AreEqual(MagickColors.Moccasin, image, 84, 20);
            }
        }

        [TestMethod]
        public void Test_StrokeLineCap()
        {
            using (IMagickImage image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.AreEqual(LineCap.Butt, image.Settings.StrokeLineCap);

                image.Settings.StrokeWidth = 8;
                image.Settings.StrokeColor = MagickColors.Sienna;
                image.Settings.StrokeLineCap = LineCap.Round;

                image.Draw(new DrawablePath(new PathMoveToAbs(40, 20), new PathLineToAbs(40, 70)));

                ColorAssert.AreEqual(MagickColors.Sienna, image, 40, 17);
            }
        }

        [TestMethod]
        public void Test_StrokeLineJoin()
        {
            using (IMagickImage image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.AreEqual(LineJoin.Miter, image.Settings.StrokeLineJoin);

                image.Settings.StrokeWidth = 5;
                image.Settings.StrokeColor = MagickColors.LemonChiffon;
                image.Settings.StrokeLineJoin = LineJoin.Round;

                image.Draw(new DrawablePath(new PathMoveToAbs(75, 70), new PathLineToAbs(90, 20), new PathLineToAbs(105, 70)));

                ColorAssert.AreEqual(MagickColors.SkyBlue, image, 90, 12);
            }
        }

        [TestMethod]
        public void Test_StrokeMiterLimit()
        {
            using (IMagickImage image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.AreEqual(10, image.Settings.StrokeMiterLimit);

                image.Settings.StrokeWidth = 5;
                image.Settings.StrokeColor = MagickColors.MediumSpringGreen;
                image.Settings.StrokeMiterLimit = 6;

                image.Draw(new DrawablePath(new PathMoveToAbs(65, 70), new PathLineToAbs(80, 20), new PathLineToAbs(95, 70)));

                ColorAssert.AreEqual(MagickColors.SkyBlue, image, 80, 18);
            }
        }

        [TestMethod]
        public void Test_StrokePattern()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 250, 100))
            {
                Assert.AreEqual(null, image.Settings.StrokePattern);

                image.Settings.StrokeWidth = 40;
                image.Settings.StrokePattern = new MagickImage(Files.Builtin.Logo);

                image.Draw(new DrawableLine(50, 50, 200, 50));

                ColorAssert.AreEqual(MagickColors.Red, image, 49, 50);
                ColorAssert.AreEqual(MagickColors.White, image, 50, 50);
                ColorAssert.AreEqual(MagickColors.White, image, 50, 70);
                ColorAssert.AreEqual(MagickColors.White, image, 200, 50);
                ColorAssert.AreEqual(MagickColors.White, image, 200, 70);
                ColorAssert.AreEqual(MagickColors.Red, image, 201, 50);
            }
        }

        [TestMethod]
        public void Test_StrokeWidth()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Purple, 300, 300))
            {
                Assert.AreEqual(1, image.Settings.StrokeWidth);
                Assert.AreEqual(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0), image.Settings.StrokeColor);

                image.Settings.StrokeWidth = 40;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Draw(new DrawableCircle(150, 150, 100, 100));

                ColorAssert.AreEqual(MagickColors.Black, image, 150, 150);
                ColorAssert.AreEqual(MagickColors.Orange, image, 201, 150);
                ColorAssert.AreEqual(MagickColors.Purple, image, 244, 150);
            }
        }
    }
}
