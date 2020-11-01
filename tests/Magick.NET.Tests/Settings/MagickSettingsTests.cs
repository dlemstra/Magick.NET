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
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        [Fact]
        public void Test_Affine()
        {
            using (var image = new MagickImage(MagickColors.White, 300, 300))
            {
                Assert.Null(image.Settings.Affine);

                image.Annotate("Magick.NET", Gravity.Center);
                ColorAssert.Equal(MagickColors.White, image, 200, 200);

                image.Settings.Affine = new DrawableAffine(10, 20, 30, 40, 50, 60);
                image.Annotate("Magick.NET", Gravity.Center);
                ColorAssert.Equal(MagickColors.Black, image, 200, 200);
            }
        }

        [Fact]
        public void Test_BorderColor()
        {
            using (var image = new MagickImage(MagickColors.MediumTurquoise, 10, 10))
            {
                ColorAssert.Equal(new MagickColor("#df"), image.Settings.BorderColor);

                image.Settings.FillColor = MagickColors.Beige;
                image.Settings.BorderColor = MagickColors.MediumTurquoise;
                image.Extent(20, 20, Gravity.Center, MagickColors.Aqua);
                image.Draw(new DrawableAlpha(0, 0, PaintMethod.FillToBorder));

                ColorAssert.Equal(MagickColors.Beige, image, 0, 0);
                ColorAssert.Equal(MagickColors.MediumTurquoise, image, 10, 10);
            }
        }

        [Fact]
        public void Test_ColorSpace()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(ColorSpace.Undefined, image.Settings.ColorSpace);

                image.Read(Files.ImageMagickJPG);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);

                image.Settings.ColorSpace = ColorSpace.Rec601YCbCr;
                image.Read(Files.ImageMagickJPG);
                ColorAssert.Equal(new MagickColor("#ff8080"), image, 0, 0);
            }
        }

        [Fact]
        public void Test_ColorType()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(ColorType.Undefined, image.Settings.ColorType);

                image.Read(Files.Builtin.Wizard);
                image.ColorType = ColorType.Grayscale;

                Assert.Equal(ColorType.Grayscale, image.ColorType);
                image.Settings.ColorType = ColorType.TrueColor;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Jpg;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(ColorType.TrueColor, result.ColorType);
                    }
                }
            }
        }

        [Fact]
        public void Test_Compression()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(CompressionMethod.Undefined, image.Settings.Compression);

                image.Read(Files.Builtin.Wizard);

                image.Settings.Compression = CompressionMethod.NoCompression;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Bmp;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(CompressionMethod.NoCompression, result.Compression);
                    }
                }
            }
        }

        [Fact]
        public void Test_Debug()
        {
            using (var image = new MagickImage())
            {
                Assert.False(image.Settings.Debug);
            }
        }

        [Fact]
        public void Test_Endian()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                Assert.Equal(Endian.Undefined, image.Settings.Endian);

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Settings.Endian = Endian.MSB;
                    image.Format = MagickFormat.Ipl;
                    image.Write(memStream);
                    memStream.Position = 0;

                    MagickReadSettings settings = new MagickReadSettings();
                    settings.Format = MagickFormat.Ipl;

                    using (var result = new MagickImage(memStream, settings))
                    {
                        Assert.Equal(Endian.MSB, result.Endian);
                    }
                }
            }
        }

        [Fact]
        public void Test_FillColor()
        {
            using (var image = new MagickImage(MagickColors.Transparent, 100, 100))
            {
                ColorAssert.Equal(MagickColors.Black, image.Settings.FillColor);

                IPixel<QuantumType> pixelA;
                image.Settings.FillColor = MagickColors.Red;
                image.Read("caption:Magick.NET");

                Assert.Equal(100, image.Width);
                Assert.Equal(100, image.Height);

                using (var pixels = image.GetPixels())
                {
                    pixelA = pixels.GetPixel(64, 6);
                }

                IPixel<QuantumType> pixelB;
                image.Settings.FillColor = MagickColors.Yellow;
                image.Read("caption:Magick.NET");
                using (var pixels = image.GetPixels())
                {
                    pixelB = pixels.GetPixel(64, 6);
                }

                ColorAssert.NotEqual(pixelA.ToColor(), pixelB.ToColor());
            }
        }

        [Fact]
        public void Test_FillPattern()
        {
            using (var image = new MagickImage(MagickColors.Transparent, 500, 500))
            {
                Assert.Null(image.Settings.FillPattern);

                image.Settings.FillPattern = new MagickImage(Files.SnakewarePNG);

                PointD[] coordinates = new PointD[4];
                coordinates[0] = new PointD(50, 50);
                coordinates[1] = new PointD(150, 50);
                coordinates[2] = new PointD(150, 150);
                coordinates[3] = new PointD(50, 150);
                image.Draw(new DrawablePolygon(coordinates));

                ColorAssert.Equal(new MagickColor("#02"), image, 84, 80);
            }
        }

        [Fact]
        public void Test_FillRule()
        {
            using (var image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.Equal(FillRule.EvenOdd, image.Settings.FillRule);

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

                ColorAssert.Equal(MagickColors.White, image, 50, 30);
            }
        }

        [Fact]
        public void Test_Interlace()
        {
            using (var image = new MagickImage(MagickColors.Fuchsia, 100, 60))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Jpeg;
                    image.Write(memStream);

                    memStream.Position = 0;
                    image.Read(memStream);
                    Assert.Equal(Interlace.NoInterlace, image.Interlace);
                }

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Interlace = Interlace.Undefined;
                    image.Write(memStream);

                    memStream.Position = 0;
                    image.Read(memStream);
                    Assert.Equal(Interlace.Jpeg, image.Interlace);
                }
            }
        }

        [Fact]
        public void Test_StrokeAntiAlias()
        {
            using (var image = new MagickImage(MagickColors.Purple, 300, 300))
            {
                Assert.True(image.Settings.StrokeAntiAlias);

                image.Settings.StrokeWidth = 20;
                image.Settings.StrokeAntiAlias = false;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Draw(new DrawableCircle(150, 150, 100, 100));

                ColorAssert.Equal(MagickColors.Purple, image, 69, 149);
                ColorAssert.Equal(MagickColors.Orange, image, 69, 150);
            }
        }

        [Fact]
        public void Test_StrokeDashArray()
        {
            using (var image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.Null(image.Settings.StrokeDashArray);
                Assert.Equal(0, image.Settings.StrokeDashOffset);

                image.Settings.StrokeColor = MagickColors.Moccasin;
                image.Settings.StrokeDashArray = new double[] { 5.0, 8.0, 10.0 };
                image.Settings.StrokeDashOffset = 1;

                image.Draw(new DrawablePath(new PathMoveToAbs(10, 20), new PathLineToAbs(90, 20)));

                ColorAssert.Equal(MagickColors.Moccasin, image, 13, 20);
                ColorAssert.Equal(MagickColors.Moccasin, image, 37, 20);
                ColorAssert.Equal(MagickColors.Moccasin, image, 60, 20);
                ColorAssert.Equal(MagickColors.Moccasin, image, 84, 20);
            }
        }

        [Fact]
        public void Test_StrokeLineCap()
        {
            using (var image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.Equal(LineCap.Butt, image.Settings.StrokeLineCap);

                image.Settings.StrokeWidth = 8;
                image.Settings.StrokeColor = MagickColors.Sienna;
                image.Settings.StrokeLineCap = LineCap.Round;

                image.Draw(new DrawablePath(new PathMoveToAbs(40, 20), new PathLineToAbs(40, 70)));

                ColorAssert.Equal(MagickColors.Sienna, image, 40, 17);
            }
        }

        [Fact]
        public void Test_StrokeLineJoin()
        {
            using (var image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.Equal(LineJoin.Miter, image.Settings.StrokeLineJoin);

                image.Settings.StrokeWidth = 5;
                image.Settings.StrokeColor = MagickColors.LemonChiffon;
                image.Settings.StrokeLineJoin = LineJoin.Round;

                image.Draw(new DrawablePath(new PathMoveToAbs(75, 70), new PathLineToAbs(90, 20), new PathLineToAbs(105, 70)));

                ColorAssert.Equal(MagickColors.SkyBlue, image, 90, 12);
            }
        }

        [Fact]
        public void Test_StrokeMiterLimit()
        {
            using (var image = new MagickImage(MagickColors.SkyBlue, 100, 60))
            {
                Assert.Equal(10, image.Settings.StrokeMiterLimit);

                image.Settings.StrokeWidth = 5;
                image.Settings.StrokeColor = MagickColors.MediumSpringGreen;
                image.Settings.StrokeMiterLimit = 6;

                image.Draw(new DrawablePath(new PathMoveToAbs(65, 70), new PathLineToAbs(80, 20), new PathLineToAbs(95, 70)));

                ColorAssert.Equal(MagickColors.SkyBlue, image, 80, 18);
            }
        }

        [Fact]
        public void Test_StrokePattern()
        {
            using (var image = new MagickImage(MagickColors.Red, 250, 100))
            {
                Assert.Null(image.Settings.StrokePattern);

                image.Settings.StrokeWidth = 40;
                image.Settings.StrokePattern = new MagickImage(Files.Builtin.Logo);

                image.Draw(new DrawableLine(50, 50, 200, 50));

                ColorAssert.Equal(MagickColors.Red, image, 49, 50);
                ColorAssert.Equal(MagickColors.White, image, 50, 50);
                ColorAssert.Equal(MagickColors.White, image, 50, 70);
                ColorAssert.Equal(MagickColors.White, image, 200, 50);
                ColorAssert.Equal(MagickColors.White, image, 200, 70);
                ColorAssert.Equal(MagickColors.Red, image, 201, 50);
            }
        }

        [Fact]
        public void Test_StrokeWidth()
        {
            using (var image = new MagickImage(MagickColors.Purple, 300, 300))
            {
                Assert.Equal(1, image.Settings.StrokeWidth);
                Assert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0), image.Settings.StrokeColor);

                image.Settings.StrokeWidth = 40;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Draw(new DrawableCircle(150, 150, 100, 100));

                ColorAssert.Equal(MagickColors.Black, image, 150, 150);
                ColorAssert.Equal(MagickColors.Orange, image, 201, 150);
                ColorAssert.Equal(MagickColors.Purple, image, 244, 150);
            }
        }
    }
}
