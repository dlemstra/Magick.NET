// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public partial class MagickImageTests
    {
        [Fact]
        public void Test_AdaptiveBlur()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.AdaptiveBlur(10, 5);

#if Q8 || Q16
                ColorAssert.Equal(new MagickColor("#a872dfb1f8ddfe8b"), image, 56, 68);
#else
                ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), image, 56, 68);
#endif
            }
        }

        [Fact]
        public void Test_AdaptiveSharpen()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.AdaptiveSharpen(10, 10);
#if Q8 || Q16
                ColorAssert.Equal(new MagickColor("#a95ce07af952"), image, 56, 68);
#else
                ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), image, 56, 68);
#endif
            }
        }

        [Fact]
        public void Test_AdaptiveThreshold()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.AdaptiveThreshold(10, 10);
                ColorAssert.Equal(MagickColors.White, image, 50, 75);
            }
        }

        [Fact]
        public void Test_AddNoise()
        {
            TestHelper.ExecuteInsideLock(() =>
            {
                MagickNET.SetRandomSeed(1337);

                using (var first = new MagickImage(Files.Builtin.Logo))
                {
                    first.AddNoise(NoiseType.Laplacian);
                    ColorAssert.NotEqual(MagickColors.White, first, 46, 62);

                    using (var second = new MagickImage(Files.Builtin.Logo))
                    {
                        second.AddNoise(NoiseType.Laplacian, 2.0);
                        ColorAssert.NotEqual(MagickColors.White, first, 46, 62);
                        Assert.False(first.Equals(second));
                    }
                }

                MagickNET.ResetRandomSeed();
            });
        }

        [Fact]
        public void Test_AffineTransform()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                DrawableAffine affineMatrix = new DrawableAffine(1, 0.5, 0, 0, 0, 0);
                image.AffineTransform(affineMatrix);
                Assert.Equal(482, image.Width);
                Assert.Equal(322, image.Height);
            }
        }

        [Fact]
        public void Test_AnimationDelay()
        {
            using (var image = new MagickImage())
            {
                image.AnimationDelay = 60;
                Assert.Equal(60, image.AnimationDelay);

                image.AnimationDelay = -1;
                Assert.Equal(60, image.AnimationDelay);

                image.AnimationDelay = 0;
                Assert.Equal(0, image.AnimationDelay);
            }
        }

        [Fact]
        public void Test_AnimationIterations()
        {
            using (var image = new MagickImage())
            {
                image.AnimationIterations = 60;
                Assert.Equal(60, image.AnimationIterations);

                image.AnimationIterations = -1;
                Assert.Equal(60, image.AnimationIterations);

                image.AnimationIterations = 0;
                Assert.Equal(0, image.AnimationIterations);
            }
        }

        [Fact]
        public void Test_Annotate()
        {
            using (var image = new MagickImage(MagickColors.Thistle, 200, 50))
            {
                image.Settings.FontPointsize = 20;
                image.Settings.FillColor = MagickColors.Purple;
                image.Settings.StrokeColor = MagickColors.Purple;
                image.Annotate("Magick.NET", Gravity.East);

                ColorAssert.Equal(MagickColors.Purple, image, 197, 17);
                ColorAssert.Equal(MagickColors.Thistle, image, 174, 17);
            }

            using (var image = new MagickImage(MagickColors.GhostWhite, 200, 200))
            {
                image.Settings.FontPointsize = 30;
                image.Settings.FillColor = MagickColors.Orange;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Annotate("Magick.NET", new MagickGeometry(75, 125, 0, 0), Gravity.Undefined, 45);

                ColorAssert.Equal(MagickColors.GhostWhite, image, 104, 83);
                ColorAssert.Equal(MagickColors.Orange, image, 118, 70);
            }
        }

        [Fact]
        public void Test_AutoGamma()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.AutoGamma();

                ColorAssert.Equal(new MagickColor("#00000003017E"), image, 496, 429);
            }
        }

        [Fact]
        public void Test_BlackThreshold()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.BlackThreshold(new Percentage(90));
                ColorAssert.Equal(MagickColors.Black, image, 43, 74);
                ColorAssert.Equal(new MagickColor("#0000f8"), image, 60, 74);
            }
        }

        [Fact]
        public void Test_BackgroundColor()
        {
            using (var image = new MagickImage("xc:red", 1, 1))
            {
                ColorAssert.Equal(new MagickColor("White"), image.BackgroundColor);
            }

            MagickColor red = new MagickColor("Red");

            using (var image = new MagickImage(red, 1, 1))
            {
                ColorAssert.Equal(red, image.BackgroundColor);

                image.Read(new MagickColor("Purple"), 1, 1);

                ColorAssert.Equal(MagickColors.Purple, image.BackgroundColor);
            }
        }

        [Fact]
        public void Test_BitDepth()
        {
            using (var image = new MagickImage(Files.RoseSparkleGIF))
            {
                Assert.Equal(8, image.BitDepth());

                image.Threshold((Percentage)50);
                Assert.Equal(1, image.BitDepth());
            }
        }

        [Fact]
        public void Test_BlueShift()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                ColorAssert.NotEqual(MagickColors.White, image, 180, 80);

                image.BlueShift(2);

#if Q16HDRI
                ColorAssert.NotEqual(MagickColors.White, image, 180, 80);
                image.Clamp();
#endif

                ColorAssert.Equal(MagickColors.White, image, 180, 80);

#if Q8 || Q16
                ColorAssert.Equal(new MagickColor("#ac2cb333c848"), image, 350, 265);
#else
                ColorAssert.Equal(new MagickColor("#ac2cb333c848"), image, 350, 265);
#endif
            }
        }

        [Fact]
        public void Test_BrightnessContrast()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                ColorAssert.NotEqual(MagickColors.White, image, 340, 295);
                image.BrightnessContrast(new Percentage(50), new Percentage(50));
                image.Clamp();
                ColorAssert.Equal(MagickColors.White, image, 340, 295);
            }
        }

        [Fact]
        public void Test_CannyEdge_HoughLine()
        {
            using (var image = new MagickImage(Files.ConnectedComponentsPNG))
            {
                image.Threshold(new Percentage(50));

                ColorAssert.Equal(MagickColors.Black, image, 150, 365);
                image.Negate();
                ColorAssert.Equal(MagickColors.White, image, 150, 365);

                image.CannyEdge();
                ColorAssert.Equal(MagickColors.Black, image, 150, 365);

                image.Crop(new MagickGeometry(260, 180, 215, 200));

                image.Settings.FillColor = MagickColors.Red;
                image.Settings.StrokeColor = MagickColors.Red;

                image.HoughLine();
                ColorAssert.Equal(MagickColors.Red, image, 105, 25);
            }
        }

        [Fact]
        public void Test_Charcoal()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Charcoal();
                ColorAssert.Equal(MagickColors.White, image, 424, 412);
            }
        }

        [Fact]
        public void Test_Chop()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                image.Chop(new MagickGeometry(new Percentage(50), new Percentage(50)));
                Assert.Equal(240, image.Width);
                Assert.Equal(320, image.Height);
            }
        }

        [Fact]
        public void Test_Channels()
        {
            PixelChannel[] rgb = new PixelChannel[]
            {
                PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue,
            };

            PixelChannel[] rgba = new PixelChannel[]
            {
                PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue, PixelChannel.Alpha,
            };

            PixelChannel[] gray = new PixelChannel[]
            {
                PixelChannel.Gray,
            };

            PixelChannel[] grayAlpha = new PixelChannel[]
            {
                PixelChannel.Gray, PixelChannel.Alpha,
            };

            PixelChannel[] cmyk = new PixelChannel[]
            {
                PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black,
            };

            PixelChannel[] cmyka = new PixelChannel[]
            {
                PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black, PixelChannel.Alpha,
            };

            using (var image = new MagickImage(Files.RoseSparkleGIF))
            {
                Assert.Equal(rgba, image.Channels.ToArray());

                image.Alpha(AlphaOption.Off);

                Assert.Equal(rgb, image.Channels.ToArray());
            }

            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                Assert.Equal(grayAlpha, image.Channels.ToArray());

                using (var redChannel = image.Separate(Channels.Red).First())
                {
                    Assert.Equal(gray, redChannel.Channels.ToArray());

                    redChannel.Alpha(AlphaOption.On);

                    Assert.Equal(grayAlpha, redChannel.Channels.ToArray());
                }
            }

            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                image.ColorSpace = ColorSpace.CMYK;

                Assert.Equal(cmyka, image.Channels.ToArray());

                image.Alpha(AlphaOption.Off);

                Assert.Equal(cmyk, image.Channels.ToArray());
            }
        }

        [Fact]
        public void Test_Chromaticity()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                PrimaryInfo info = new PrimaryInfo(0.5, 1.0, 1.5);

                AssertChromaticity(0.15, 0.06, 0, image.ChromaBluePrimary);
                image.ChromaBluePrimary = info;
                AssertChromaticity(0.5, 1.0, 1.5, image.ChromaBluePrimary);

                AssertChromaticity(0.3, 0.6, 0, image.ChromaGreenPrimary);
                image.ChromaGreenPrimary = info;
                AssertChromaticity(0.5, 1.0, 1.5, image.ChromaGreenPrimary);

                AssertChromaticity(0.64, 0.33, 0, image.ChromaRedPrimary);
                image.ChromaRedPrimary = info;
                AssertChromaticity(0.5, 1.0, 1.5, image.ChromaRedPrimary);

                AssertChromaticity(0.3127, 0.329, 0, image.ChromaWhitePoint);
                image.ChromaWhitePoint = info;
                AssertChromaticity(0.5, 1.0, 1.5, image.ChromaWhitePoint);
            }
        }

        [Fact]
        public void Test_ClassType()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                Assert.Equal(ClassType.Direct, image.ClassType);

                image.ClassType = ClassType.Pseudo;
                Assert.Equal(ClassType.Pseudo, image.ClassType);

                image.ClassType = ClassType.Direct;
                Assert.Equal(ClassType.Direct, image.ClassType);
            }
        }

        [Fact]
        public void Test_Clone()
        {
            using (var first = new MagickImage(Files.SnakewarePNG))
            {
                using (var second = first.Clone())
                {
                    AssertClone(first, second);
                }

                using (var second = new MagickImage(first))
                {
                    AssertClone(first, second);
                }
            }
        }

        [Fact]
        public void Test_Clone_Area()
        {
            using (var icon = new MagickImage(Files.MagickNETIconPNG))
            {
                using (var area = icon.Clone())
                {
                    area.Crop(64, 64, Gravity.Southeast);
                    area.RePage();
                    Assert.Equal(64, area.Width);
                    Assert.Equal(64, area.Height);

                    area.Crop(64, 32, Gravity.North);

                    Assert.Equal(64, area.Width);
                    Assert.Equal(32, area.Height);

                    using (var part = icon.Clone(new MagickGeometry(64, 64, 64, 32)))
                    {
                        AssertCloneArea(area, part);
                    }

                    using (var part = icon.Clone(64, 64, 64, 32))
                    {
                        AssertCloneArea(area, part);
                    }
                }

                using (var area = icon.Clone())
                {
                    area.Crop(32, 64, Gravity.Northwest);

                    Assert.Equal(32, area.Width);
                    Assert.Equal(64, area.Height);

                    using (var part = icon.Clone(32, 64))
                    {
                        AssertCloneArea(area, part);
                    }
                }

                using (var area = icon.Clone(4, 2))
                {
                    Assert.Equal(4, area.Width);
                    Assert.Equal(2, area.Height);

                    Assert.Equal(32, area.ToByteArray(MagickFormat.Rgba).Length);
                }
            }
        }

        [Fact]
        public void Test_Clut()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                using (var clut = CreatePallete())
                {
                    image.Clut(clut, PixelInterpolateMethod.Catrom);
                    ColorAssert.Equal(MagickColors.Green, image, 400, 300);
                }
            }
        }

        [Fact]
        public void Test_Colorize()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                image.Colorize(MagickColors.Purple, new Percentage(50));

                ColorAssert.Equal(new MagickColor("#c0408000c040"), image, 45, 75);
            }
        }

        [Fact]
        public void Test_ColorAlpha()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                MagickColor purple = new MagickColor("purple");

                image.ColorAlpha(purple);

                ColorAssert.NotEqual(purple, image, 45, 75);
                ColorAssert.Equal(purple, image, 100, 60);
            }
        }

        [Fact]
        public void Test_ColorMap()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                Assert.Null(image.GetColormap(0));
            }

            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProGIF))
            {
                ColorAssert.Equal(new MagickColor("#040d14"), image.GetColormap(0));
                image.SetColormap(0, MagickColors.Fuchsia);
                ColorAssert.Equal(MagickColors.Fuchsia, image.GetColormap(0));

                image.SetColormap(65536, MagickColors.Fuchsia);
                Assert.Null(image.GetColormap(65536));
            }
        }

        [Fact]
        public void Test_ColorMatrix()
        {
            using (var image = new MagickImage(Files.Builtin.Rose))
            {
                var matrix = new MagickColorMatrix(3, 0, 0, 1, 0, 1, 0, 1, 0, 0);

                image.ColorMatrix(matrix);

                ColorAssert.Equal(MagickColor.FromRgb(58, 31, 255), image, 39, 25);
            }
        }

        [Fact]
        public void Test_ColorType()
        {
            using (var image = new MagickImage(Files.WireframeTIF))
            {
                Assert.Equal(ColorType.TrueColor, image.ColorType);
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    memStream.Position = 0;
                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(ColorType.Grayscale, result.ColorType);
                    }
                }
            }

            using (var image = new MagickImage(Files.WireframeTIF))
            {
                Assert.Equal(ColorType.TrueColor, image.ColorType);
                image.PreserveColorType();
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Psd;
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
        public void Test_Compare()
        {
            var first = new MagickImage(Files.ImageMagickJPG);

            Assert.Throws<ArgumentNullException>("image", () =>
            {
                first.Compare(null);
            });

            var second = first.Clone();

            var same = first.Compare(second);
            Assert.NotNull(same);
            Assert.Equal(0, same.MeanErrorPerPixel);

            double distortion = first.Compare(second, ErrorMetric.Absolute);
            Assert.Equal(0, distortion);

            first.Threshold(new Percentage(50));
            var different = first.Compare(second);
            Assert.NotNull(different);
            Assert.NotEqual(0, different.MeanErrorPerPixel);

            distortion = first.Compare(second, ErrorMetric.Absolute);
            Assert.NotEqual(0, distortion);

            var difference = new MagickImage();
            distortion = first.Compare(second, ErrorMetric.RootMeanSquared, difference);
            Assert.NotEqual(0, distortion);
            Assert.False(first.Equals(difference));
            Assert.False(second.Equals(difference));

            second.Dispose();

            first.Opaque(MagickColors.Black, MagickColors.Green);
            first.Opaque(MagickColors.White, MagickColors.Green);

            second = first.Clone();
            second.FloodFill(MagickColors.Gray, 0, 0);

            distortion = first.Compare(second, ErrorMetric.Absolute, Channels.Green);
            Assert.Equal(0, distortion);

            distortion = first.Compare(second, ErrorMetric.Absolute, Channels.Red);
            Assert.NotEqual(0, distortion);
        }

        [Fact]
        public void Test_Constructor()
        {
            Assert.Throws<ArgumentException>("data", () =>
            {
                new MagickImage(new byte[0]);
            });

            Assert.Throws<ArgumentNullException>("data", () =>
            {
                new MagickImage((byte[])null);
            });

            Assert.Throws<ArgumentNullException>("file", () =>
            {
                new MagickImage((FileInfo)null);
            });

            Assert.Throws<ArgumentNullException>("stream", () =>
            {
                new MagickImage((Stream)null);
            });

            Assert.Throws<ArgumentNullException>("fileName", () =>
            {
                new MagickImage((string)null);
            });

            var exception = Assert.Throws<MagickBlobErrorException>(() =>
            {
                new MagickImage(Files.Missing);
            });

            Assert.Contains("error/blob.c/OpenBlob", exception.Message);
        }

        [Fact]
        public void Test_Contrast()
        {
            using (var first = new MagickImage(Files.Builtin.Wizard))
            {
                first.Contrast(true);
                first.Contrast(false);

                using (var second = new MagickImage(Files.Builtin.Wizard))
                {
                    Assert.InRange(first.Compare(second, ErrorMetric.RootMeanSquared), 0.0031, 0.0034);
                }
            }
        }

        [Fact]
        public void Test_ContrastStretch()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                image.ContrastStretch(new Percentage(50), new Percentage(80));
                image.Alpha(AlphaOption.Opaque);

                ColorAssert.Equal(MagickColors.Black, image, 160, 300);
                ColorAssert.Equal(MagickColors.Red, image, 325, 175);
            }
        }

        [Fact]
        public void Test_Convolve()
        {
            using (var image = new MagickImage("xc:", 1, 1))
            {
                image.BorderColor = MagickColors.Black;
                image.Border(5);

                Assert.Equal(11, image.Width);
                Assert.Equal(11, image.Height);

                var matrix = new ConvolveMatrix(3, 0, 0.5, 0, 0.5, 1, 0.5, 0, 0.5, 0);
                image.Convolve(matrix);

                MagickColor gray = new MagickColor("#800080008000");
                ColorAssert.Equal(MagickColors.Black, image, 4, 4);
                ColorAssert.Equal(gray, image, 5, 4);
                ColorAssert.Equal(MagickColors.Black, image, 6, 4);
                ColorAssert.Equal(gray, image, 4, 5);
                ColorAssert.Equal(MagickColors.White, image, 5, 5);
                ColorAssert.Equal(gray, image, 6, 5);
                ColorAssert.Equal(MagickColors.Black, image, 4, 6);
                ColorAssert.Equal(gray, image, 5, 6);
                ColorAssert.Equal(MagickColors.Black, image, 6, 6);
            }
        }

        [Fact]
        public void Test_CopyPixels()
        {
            using (var source = new MagickImage(MagickColors.White, 100, 100))
            {
                using (var destination = new MagickImage(MagickColors.Black, 50, 50))
                {
                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null);
                    });

                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null, Channels.Red);
                    });

                    Assert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        destination.CopyPixels(source, null);
                    });

                    Assert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        destination.CopyPixels(source, null, Channels.Green);
                    });

                    Assert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        destination.CopyPixels(source, null, 0, 0);
                    });

                    Assert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        destination.CopyPixels(source, null, 0, 0, Channels.Green);
                    });

                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null, new MagickGeometry(10, 10));
                    });

                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null, new MagickGeometry(10, 10), Channels.Black);
                    });

                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null, new MagickGeometry(10, 10), 0, 0);
                    });

                    Assert.Throws<ArgumentNullException>("source", () =>
                    {
                        destination.CopyPixels(null, new MagickGeometry(10, 10), 0, 0, Channels.Black);
                    });

                    Assert.Throws<MagickOptionErrorException>(() =>
                    {
                        destination.CopyPixels(source, new MagickGeometry(51, 50), new PointD(0, 0));
                    });

                    Assert.Throws<MagickOptionErrorException>(() =>
                    {
                        destination.CopyPixels(source, new MagickGeometry(50, 51), new PointD(0, 0));
                    });

                    Assert.Throws<MagickOptionErrorException>(() =>
                    {
                        destination.CopyPixels(source, new MagickGeometry(50, 50), 1, 0);
                    });

                    Assert.Throws<MagickOptionErrorException>(() =>
                    {
                        destination.CopyPixels(source, new MagickGeometry(50, 50), new PointD(0, 1));
                    });

                    destination.CopyPixels(source, new MagickGeometry(25, 25), 25, 25);

                    ColorAssert.Equal(MagickColors.Black, destination, 0, 0);
                    ColorAssert.Equal(MagickColors.Black, destination, 24, 24);
                    ColorAssert.Equal(MagickColors.White, destination, 25, 25);
                    ColorAssert.Equal(MagickColors.White, destination, 49, 49);

                    destination.CopyPixels(source, new MagickGeometry(25, 25), 0, 25, Channels.Green);

                    ColorAssert.Equal(MagickColors.Black, destination, 0, 0);
                    ColorAssert.Equal(MagickColors.Black, destination, 24, 24);
                    ColorAssert.Equal(MagickColors.White, destination, 25, 25);
                    ColorAssert.Equal(MagickColors.White, destination, 49, 49);
                    ColorAssert.Equal(MagickColors.Lime, destination, 0, 25);
                    ColorAssert.Equal(MagickColors.Lime, destination, 24, 49);
                }
            }
        }

        [Fact]
        public void Test_CropToTiles()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                var tiles = image.CropToTiles(48, 48).ToArray();
                Assert.Equal(140, tiles.Length);

                for (int i = 0; i < tiles.Length; i++)
                {
                    var tile = tiles[i];

                    Assert.Equal(48, tile.Height);

                    if (i == 13 || (i - 13) % 14 == 0)
                        Assert.Equal(16, tile.Width);
                    else
                        Assert.Equal(48, tile.Width);

                    tile.Dispose();
                }
            }
        }

        [Fact]
        public void Test_CycleColormap()
        {
            using (var first = new MagickImage(Files.Builtin.Logo))
            {
                Assert.Equal(256, first.ColormapSize);

                using (var second = first.Clone())
                {
                    second.CycleColormap(128);
                    Assert.NotEqual(first, second);

                    second.CycleColormap(128);
                    Assert.Equal(first, second);

                    second.CycleColormap(256);
                    Assert.Equal(first, second);

                    second.CycleColormap(512);
                    Assert.Equal(first, second);
                }
            }
        }

        [Fact]
        public void Test_Define()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                string option = "optimize-coding";

                image.Settings.SetDefine(MagickFormat.Jpg, option, true);
                Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpg, option));
                Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpeg, option));

                image.Settings.RemoveDefine(MagickFormat.Jpeg, option);
                Assert.Null(image.Settings.GetDefine(MagickFormat.Jpg, option));

                image.Settings.SetDefine(MagickFormat.Jpeg, option, "test");
                Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpg, option));
                Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpeg, option));

                image.Settings.RemoveDefine(MagickFormat.Jpg, option);
                Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, option));

                image.Settings.SetDefine("profile:skip", "ICC");
                Assert.Equal("ICC", image.Settings.GetDefine("profile:skip"));
            }
        }

        [Fact]
        public void Test_Density()
        {
            using (var image = new MagickImage(Files.EightBimTIF))
            {
                Assert.Equal(72, image.Density.X);
                Assert.Equal(72, image.Density.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, image.Density.Units);
            }
        }

        [Fact]
        public void Test_Despeckle()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                MagickColor color = new MagickColor("#d1d1d1d1d1d1");
                ColorAssert.NotEqual(color, image, 130, 123);

                image.Despeckle();
                image.Despeckle();
                image.Despeckle();

                ColorAssert.Equal(color, image, 130, 123);
            }
        }

        [Fact]
        public void Test_DetermineColorType()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                Assert.Equal(ColorType.TrueColorAlpha, image.ColorType);

                ColorType colorType = image.DetermineColorType();
                Assert.Equal(ColorType.GrayscaleAlpha, colorType);
            }
        }

        [Fact]
        public void Test_Dispose()
        {
            var image = new MagickImage();
            image.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                image.HasAlpha = true;
            });
        }

        [Fact]
        public void Test_Drawable()
        {
            using (var image = new MagickImage(MagickColors.Red, 10, 10))
            {
                MagickColor yellow = MagickColors.Yellow;
                image.Draw(new DrawableFillColor(yellow), new DrawableRectangle(0, 0, 10, 10));
                ColorAssert.Equal(yellow, image, 5, 5);
            }
        }

        [Fact]
        public void Test_Encipher_Decipher()
        {
            using (var original = new MagickImage(Files.SnakewarePNG))
            {
                using (var enciphered = original.Clone())
                {
                    enciphered.Encipher("All your base are belong to us");
                    Assert.NotEqual(original, enciphered);

                    using (var deciphered = enciphered.Clone())
                    {
                        deciphered.Decipher("What you say!!");
                        Assert.NotEqual(enciphered, deciphered);
                        Assert.NotEqual(original, deciphered);
                    }

                    using (var deciphered = enciphered.Clone())
                    {
                        deciphered.Decipher("All your base are belong to us");
                        Assert.NotEqual(enciphered, deciphered);
                        Assert.Equal(original, deciphered);
                    }
                }
            }
        }

        [Fact]
        public void Test_Edge()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                ColorAssert.NotEqual(MagickColors.Black, image, 400, 295);
                ColorAssert.NotEqual(MagickColors.Blue, image, 455, 126);

                image.Edge(2);
                image.Clamp();

                ColorAssert.Equal(MagickColors.Black, image, 400, 295);
                ColorAssert.Equal(MagickColors.Blue, image, 455, 126);
            }
        }

        [Fact]
        public void Test_Emboss()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                image.Emboss(4, 2);

#if Q8
                ColorAssert.Equal(new MagickColor("#ff5b43"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#4344ff"), image, 99, 270);
#elif Q16
                ColorAssert.Equal(new MagickColor("#ffff597e4397"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#431f43f0ffff"), image, 99, 270);
#else
                ColorAssert.Equal(new MagickColor("#ffff59624391"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#431843e8ffff"), image, 99, 270);
#endif
            }
        }

        [Fact]
        public void Test_Enhance()
        {
            using (var enhanced = new MagickImage(Files.NoisePNG))
            {
                enhanced.Enhance();

                using (var original = new MagickImage(Files.NoisePNG))
                {
                    Assert.InRange(enhanced.Compare(original, ErrorMetric.RootMeanSquared), 0.0115, 0.0118);
                }
            }
        }

        [Fact]
        public void Test_Equalize()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                image.Equalize();

                ColorAssert.Equal(MagickColors.White, image, 105, 25);
                ColorAssert.Equal(new MagickColor("#0000"), image, 105, 60);
            }
        }

        [Fact]
        public void Test_FlipFlop()
        {
            using (var collection = new MagickImageCollection())
            {
                collection.Add(new MagickImage(MagickColors.DodgerBlue, 10, 10));
                collection.Add(new MagickImage(MagickColors.Firebrick, 10, 10));

                using (var image = collection.AppendVertically())
                {
                    ColorAssert.Equal(MagickColors.DodgerBlue, image, 5, 0);
                    ColorAssert.Equal(MagickColors.Firebrick, image, 5, 10);

                    image.Flip();

                    ColorAssert.Equal(MagickColors.Firebrick, image, 5, 0);
                    ColorAssert.Equal(MagickColors.DodgerBlue, image, 5, 10);
                }

                using (var image = collection.AppendHorizontally())
                {
                    ColorAssert.Equal(MagickColors.DodgerBlue, image, 0, 5);
                    ColorAssert.Equal(MagickColors.Firebrick, image, 10, 5);

                    image.Flop();

                    ColorAssert.Equal(MagickColors.Firebrick, image, 0, 5);
                    ColorAssert.Equal(MagickColors.DodgerBlue, image, 10, 5);
                }
            }
        }

        [Fact]
        public void Test_FontTypeMetrics()
        {
            using (var image = new MagickImage(MagickColors.Transparent, 100, 100))
            {
                image.Settings.Font = "Arial";
                image.Settings.FontPointsize = 15;
                var typeMetric = image.FontTypeMetrics("Magick.NET");
                Assert.NotNull(typeMetric);
                Assert.Equal(14, typeMetric.Ascent);
                Assert.Equal(-4, typeMetric.Descent);
                Assert.Equal(30, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(18, typeMetric.TextHeight);
                Assert.Equal(82, typeMetric.TextWidth);
                Assert.Equal(-2.138671875, typeMetric.UnderlinePosition);
                Assert.Equal(1.0986328125, typeMetric.UnderlineThickness);

                image.Settings.FontPointsize = 150;
                typeMetric = image.FontTypeMetrics("Magick.NET");
                Assert.NotNull(typeMetric);
                Assert.Equal(136, typeMetric.Ascent);
                Assert.Equal(-32, typeMetric.Descent);
                Assert.Equal(300, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(168, typeMetric.TextHeight);
                Assert.Equal(816, typeMetric.TextWidth);
                Assert.Equal(-21.38671875, typeMetric.UnderlinePosition);
                Assert.Equal(10.986328125, typeMetric.UnderlineThickness);
            }
        }

        [Fact]
        public void Test_FormatInfo()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                var info = image.FormatInfo;

                Assert.NotNull(info);
                Assert.Equal(MagickFormat.Png, info.Format);
                Assert.Equal("image/png", info.MimeType);
            }
        }

        [Fact]
        public void Test_Frame()
        {
            int frameSize = 100;

            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                int expectedWidth = frameSize + image.Width + frameSize;
                int expectedHeight = frameSize + image.Height + frameSize;

                image.Frame(frameSize, frameSize);
                Assert.Equal(expectedWidth, image.Width);
                Assert.Equal(expectedHeight, image.Height);
            }

            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                int expectedWidth = frameSize + image.Width + frameSize;
                int expectedHeight = frameSize + image.Height + frameSize;

                image.Frame(frameSize, frameSize, 6, 6);
                Assert.Equal(expectedWidth, image.Width);
                Assert.Equal(expectedHeight, image.Height);
            }

            Assert.Throws<MagickOptionErrorException>(() =>
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.Frame(6, 6, frameSize, frameSize);
                }
            });
        }

        [Fact]
        public void Test_GammaCorrect()
        {
            var first = new MagickImage(Files.InvitationTIF);
            first.GammaCorrect(2.0);

            var second = new MagickImage(Files.InvitationTIF);
            second.GammaCorrect(2.0, Channels.Red);

            Assert.False(first.Equals(second));

            first.Dispose();
            second.Dispose();
        }

        [Fact]
        public void Test_GaussianBlur()
        {
            using (var gaussian = new MagickImage(Files.Builtin.Wizard))
            {
                gaussian.GaussianBlur(5.5, 10.2);

                using (var blur = new MagickImage(Files.Builtin.Wizard))
                {
                    blur.Blur(5.5, 10.2);

                    double distortion = blur.Compare(gaussian, ErrorMetric.RootMeanSquared);
#if Q8
                    Assert.InRange(distortion, 0.00066, 0.00067);
#elif Q16
                    Assert.InRange(distortion, 0.0000033, 0.0000034);
#else
                    Assert.InRange(distortion, 0.0000011, 0.0000012);
#endif
                }
            }
        }

        [Fact]
        public void Test_GetClippingPath()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                string clippingPath = image.GetClippingPath();
                Assert.NotNull(clippingPath);

                clippingPath = image.GetClippingPath("#1");
                Assert.NotNull(clippingPath);
            }
        }

        [Fact]
        public void Test_Grayscale()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Grayscale(PixelIntensityMethod.Brightness);
                Assert.Equal(1, image.ChannelCount);
                Assert.Equal(PixelChannel.Red, image.Channels.First());

                ColorAssert.Equal(MagickColors.White, image, 220, 45);
                ColorAssert.Equal(new MagickColor("#929292"), image, 386, 379);
                ColorAssert.Equal(new MagickColor("#f5f5f5"), image, 405, 158);
            }
        }

        [Fact]
        public void Test_HaldClut()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                using (var clut = CreatePallete())
                {
                    image.HaldClut(clut);

                    ColorAssert.Equal(new MagickColor("#052268042ba5"), image, 228, 276);
                    ColorAssert.Equal(new MagickColor("#144f623a2801"), image, 295, 270);
                }
            }
        }

        [Fact]
        public void Test_HasClippingPath()
        {
            using (var noPath = new MagickImage(Files.MagickNETIconPNG))
            {
                Assert.False(noPath.HasClippingPath);
            }

            using (var hasPath = new MagickImage(Files.InvitationTIF))
            {
                Assert.True(hasPath.HasClippingPath);
            }
        }

        [Fact]
        public void Test_Histogram()
        {
            var image = new MagickImage();
            var histogram = image.Histogram();
            Assert.NotNull(histogram);
            Assert.Empty(histogram);

            image = new MagickImage(Files.RedPNG);
            histogram = image.Histogram();

            Assert.NotNull(histogram);
            Assert.Equal(3, histogram.Count);

            MagickColor red = new MagickColor(Quantum.Max, 0, 0);
            MagickColor alphaRed = new MagickColor(Quantum.Max, 0, 0, 0);
            MagickColor halfAlphaRed = new MagickColor("#FF000080");

            Assert.Equal(3, histogram.Count);
            Assert.Equal(50000, histogram[red]);
            Assert.Equal(30000, histogram[alphaRed]);
            Assert.Equal(40000, histogram[halfAlphaRed]);

            image.Dispose();
        }

        [Fact]
        public void Test_IComparable()
        {
            MagickImage first = new MagickImage(MagickColors.Red, 10, 5);

            Assert.Equal(0, first.CompareTo(first));
            Assert.Equal(1, first.CompareTo(null));
            Assert.False(first < null);
            Assert.False(first <= null);
            Assert.True(first > null);
            Assert.True(first >= null);
            Assert.True(null < first);
            Assert.True(null <= first);
            Assert.False(null > first);
            Assert.False(null >= first);

            MagickImage second = new MagickImage(MagickColors.Green, 5, 5);

            Assert.Equal(1, first.CompareTo(second));
            Assert.False(first < second);
            Assert.False(first <= second);
            Assert.True(first > second);
            Assert.True(first >= second);

            second = new MagickImage(MagickColors.Red, 5, 10);

            Assert.Equal(0, first.CompareTo(second));
            Assert.False(first == second);
            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);

            first.Dispose();
            second.Dispose();
        }

        [Fact]
        public void Test_IEquatable()
        {
            MagickImage first = new MagickImage(MagickColors.Red, 10, 10);

            Assert.False(first == null);
            Assert.False(first.Equals(null));
            Assert.True(first.Equals(first));
            Assert.True(first.Equals((object)first));

            MagickImage second = new MagickImage(MagickColors.Red, 10, 10);

            Assert.True(first == second);
            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));

            second = new MagickImage(MagickColors.Green, 10, 10);

            Assert.True(first != second);
            Assert.False(first.Equals(second));

            first.Dispose();
            second.Dispose();

            first = null;
            Assert.True(first == null);
            Assert.False(first != null);
        }

        [Fact]
        public void Test_Implode()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                ColorAssert.Equal(new MagickColor("#00000000"), image, 69, 45);

                image.Implode(0.5, PixelInterpolateMethod.Blend);

                ColorAssert.Equal(new MagickColor("#a8dff8"), image, 69, 45);

                image.Implode(-0.5, PixelInterpolateMethod.Background);

                ColorAssert.Equal(new MagickColor("#00000000"), image, 69, 45);
            }
        }

        [Fact]
        public void Test_Interlace()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                Assert.Equal(Interlace.NoInterlace, image.Interlace);

                image.Interlace = Interlace.Png;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    memStream.Position = 0;
                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(Interlace.Png, result.Interlace);
                    }
                }
            }
        }

        [Fact]
        public void Test_Kuwahara()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                image.Kuwahara(13.4, 2.5);
                image.ColorType = ColorType.Bilevel;

                ColorAssert.Equal(MagickColors.White, image, 216, 120);
                ColorAssert.Equal(MagickColors.Black, image, 39, 138);
            }
        }

        [Fact]
        public void Test_LevelColors()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod);
                ColorAssert.Equal(new MagickColor("#ffffbed54bc4"), image, 42, 75);
                ColorAssert.Equal(new MagickColor("#ffffffff0809"), image, 62, 75);
            }

            using (var first = new MagickImage(Files.MagickNETIconPNG))
            {
                first.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
                first.InverseLevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
                first.Alpha(AlphaOption.Background);

                using (var second = new MagickImage(Files.MagickNETIconPNG))
                {
                    second.Alpha(AlphaOption.Background);
#if Q8 || Q16
                    Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
#else
                    Assert.InRange(first.Compare(second, ErrorMetric.RootMeanSquared), 0.0, 0.00000001);
#endif
                }
            }
        }

        [Fact]
        public void Test_LinearStretch()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Scale(100, 100);

                image.LinearStretch((Percentage)1, (Percentage)1);
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Histogram;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (var histogram = new MagickImage(memStream))
                    {
#if Q8
                        ColorAssert.Equal(MagickColors.Red, histogram, 65, 38);
                        ColorAssert.Equal(MagickColors.Lime, histogram, 135, 0);
                        ColorAssert.Equal(MagickColors.Blue, histogram, 209, 81);
#else
                        ColorAssert.Equal(MagickColors.Red, histogram, 34, 183);
                        ColorAssert.Equal(MagickColors.Lime, histogram, 122, 193);
                        ColorAssert.Equal(MagickColors.Blue, histogram, 210, 194);
#endif
                    }
                }

                image.LinearStretch((Percentage)10, (Percentage)90);
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Format = MagickFormat.Histogram;
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (var histogram = new MagickImage(memStream))
                    {
#if Q8
                        ColorAssert.Equal(MagickColors.Red, histogram, 96, 174);
                        ColorAssert.Equal(MagickColors.Lime, histogram, 212, 168);
                        ColorAssert.Equal(MagickColors.Blue, histogram, 194, 190);
#elif Q16
                        ColorAssert.Equal(MagickColors.Red, histogram, 221, 183);
                        ColorAssert.Equal(MagickColors.Lime, histogram, 11, 181);
                        ColorAssert.Equal(MagickColors.Blue, histogram, 45, 194);
#else
                        ColorAssert.Equal(MagickColors.Red, histogram, 221, 183);
                        ColorAssert.Equal(MagickColors.Lime, histogram, 12, 180);
                        ColorAssert.Equal(MagickColors.Blue, histogram, 45, 194);
#endif
                    }
                }
            }
        }

        [Fact]
        public void Test_LocalContrast()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                image.LocalContrast(5.0, (Percentage)75);
                image.Clamp();

                ColorAssert.Equal(MagickColors.Black, image, 81, 28);
                ColorAssert.Equal(MagickColors.Black, image, 245, 181);
                ColorAssert.Equal(MagickColors.White, image, 200, 135);
                ColorAssert.Equal(MagickColors.White, image, 200, 135);
            }
        }

        [Fact]
        public void Test_Magnify()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.Magnify();
                Assert.Equal(256, image.Width);
                Assert.Equal(256, image.Height);
            }
        }

        [Fact]
        public void MeanShift_WithSize1_DoesNotChangeImage()
        {
            using (var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
            {
                using (var output = input.Clone())
                {
                    output.MeanShift(1);

                    Assert.Equal(0.0, output.Compare(input, ErrorMetric.RootMeanSquared));
                }
            }
        }

        [Fact]
        public void MeanShift_WithSizeLargerThan1_ChangesImage()
        {
            using (var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
            {
                using (var output = input.Clone())
                {
                    output.MeanShift(2, new Percentage(80));

                    Assert.InRange(output.Compare(input, ErrorMetric.RootMeanSquared), 0.019, 0.020);
                }
            }
        }

        [Fact]
        public void Test_MatteColor()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.MatteColor = MagickColors.PaleGoldenrod;
                image.Frame();

                ColorAssert.Equal(MagickColors.PaleGoldenrod, image, 10, 10);
                ColorAssert.Equal(MagickColors.PaleGoldenrod, image, 680, 520);
            }
        }

        [Fact]
        public void Test_Minify()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.Minify();
                Assert.Equal(64, image.Width);
                Assert.Equal(64, image.Height);
            }
        }

        [Fact]
        public void Test_Morphology()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                Assert.Throws<MagickOptionErrorException>(() =>
                {
                    image.Morphology(MorphologyMethod.Smooth, "Magick");
                });

                image.Morphology(MorphologyMethod.Dilate, Kernel.Square, "1");

                image.Morphology(MorphologyMethod.Convolve, "3: 0.3,0.6,0.3 0.6,1.0,0.6 0.3,0.6,0.3");

                MorphologySettings settings = new MorphologySettings();
                settings.Method = MorphologyMethod.Convolve;
                settings.ConvolveBias = new Percentage(50);
                settings.Kernel = Kernel.DoG;
                settings.KernelArguments = "0x2";

                image.Read(Files.Builtin.Logo);

                Assert.Throws<ArgumentNullException>("settings", () =>
                {
                    image.Morphology(null);
                });

                image.Morphology(settings);

                QuantumType half = (QuantumType)((Quantum.Max / 2.0) + 0.5);
                ColorAssert.Equal(new MagickColor(half, half, half), image, 120, 160);
            }
        }

        [Fact]
        public void Test_MotionBlur()
        {
            using (var motionBlurred = new MagickImage(Files.Builtin.Logo))
            {
                motionBlurred.MotionBlur(4.0, 5.4, 10.6);

                using (var original = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.InRange(motionBlurred.Compare(original, ErrorMetric.RootMeanSquared), 0.11019, 0.11020);
                }
            }
        }

        [Fact]
        public void Test_Normalize()
        {
            TestHelper.ExecuteInsideLock(() =>
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(new MagickImage("gradient:gray70-gray30", 100, 100));
                    images.Add(new MagickImage("gradient:blue-navy", 50, 100));

                    using (var colorRange = images.AppendHorizontally())
                    {
                        ColorAssert.Equal(new MagickColor("gray70"), colorRange, 0, 0);
                        ColorAssert.Equal(new MagickColor("blue"), colorRange, 101, 0);

                        ColorAssert.Equal(new MagickColor("gray30"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("navy"), colorRange, 101, 99);

                        colorRange.Normalize();

                        ColorAssert.Equal(new MagickColor("white"), colorRange, 0, 0);
                        ColorAssert.Equal(new MagickColor("blue"), colorRange, 101, 0);

#if Q8
                        ColorAssert.Equal(new MagickColor("gray40"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("#0000b3"), colorRange, 101, 99);
#else
                        ColorAssert.Equal(new MagickColor("#662e662e662e"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("#00000000b317"), colorRange, 101, 99);
#endif
                    }
                }
            });
        }

        [Fact]
        public void Test_OilPaint()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.OilPaint(2, 5);
                ColorAssert.Equal(new MagickColor("#6a7e85"), image, 180, 98);
            }
        }

        [Fact]
        public void Test_Opaque()
        {
            using (var image = new MagickImage(MagickColors.Red, 10, 10))
            {
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);

                image.Opaque(MagickColors.Red, MagickColors.Yellow);
                ColorAssert.Equal(MagickColors.Yellow, image, 0, 0);

                image.InverseOpaque(MagickColors.Yellow, MagickColors.Red);
                ColorAssert.Equal(MagickColors.Yellow, image, 0, 0);

                image.InverseOpaque(MagickColors.Red, MagickColors.Red);
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            }
        }

        [Fact]
        public void Test_Perceptible()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Perceptible(Quantum.Max * 0.4);

                ColorAssert.Equal(new MagickColor("#f79868"), image, 300, 210);
                ColorAssert.Equal(new MagickColor("#666692"), image, 410, 405);
            }
        }

        [Fact]
        public void Test_Polaroid()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.BorderColor = MagickColors.Red;
                image.BackgroundColor = MagickColors.Fuchsia;
                image.Settings.FontPointsize = 20;
                image.Polaroid("Magick.NET", 10, PixelInterpolateMethod.Bilinear);
                image.Clamp();

                ColorAssert.Equal(MagickColors.Black, image, 104, 163);
                ColorAssert.Equal(MagickColors.Red, image, 72, 156);
#if Q8
                ColorAssert.Equal(new MagickColor("#ff00ffbc"), image, 146, 196);
#else
                ColorAssert.Equal(new MagickColor("#ffff0000ffffbb9a"), image, 146, 196);
#endif
            }
        }

        [Fact]
        public void Test_Posterize()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Posterize(5);

#if Q8
                ColorAssert.Equal(new MagickColor("#4080bf"), image, 300, 150);
                ColorAssert.Equal(new MagickColor("#404080"), image, 495, 270);
                ColorAssert.Equal(new MagickColor("#404040"), image, 445, 255);
#else
                ColorAssert.Equal(new MagickColor("#40008000bfff"), image, 300, 150);
                ColorAssert.Equal(new MagickColor("#400040008000"), image, 495, 270);
                ColorAssert.Equal(new MagickColor("#400040004000"), image, 445, 255);
#endif
            }
        }

        [Fact]
        public void Test_Profile()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                Assert.NotNull(profile);
                image.RemoveProfile(profile.Name);
                profile = image.GetIptcProfile();
                Assert.Null(profile);

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    memStream.Position = 0;

                    using (var newImage = new MagickImage(memStream))
                    {
                        profile = newImage.GetIptcProfile();
                        Assert.Null(profile);
                    }
                }
            }
        }

        [Fact]
        public void Test_ProfileNames()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var names = image.ProfileNames;
                Assert.NotNull(names);
                Assert.Equal("8bim,exif,icc,iptc,xmp", string.Join(",", names));
            }

            using (var image = new MagickImage(Files.RedPNG))
            {
                var names = image.ProfileNames;
                Assert.NotNull(names);
                Assert.Empty(names);
            }
        }

        [Fact]
        public void Test_Progress()
        {
            var progress = new Percentage(0);
            var cancel = false;
            EventHandler<ProgressEventArgs> progressEvent = (sender, arguments) =>
            {
                Assert.NotNull(sender);
                Assert.NotNull(arguments);
                Assert.NotNull(arguments.Origin);
                Assert.False(arguments.Cancel);

                progress = arguments.Progress;
                if (cancel)
                    arguments.Cancel = true;
            };

            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Progress += progressEvent;

                image.Flip();
                Assert.Equal(100, (int)progress);
            }

            cancel = true;

            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Progress += progressEvent;

                image.Flip();

                Assert.True(progress <= (Percentage)1);
                Assert.False(image.IsDisposed);
            }
        }

        [Fact]
        public void Test_Quantize()
        {
            QuantizeSettings settings = new QuantizeSettings();
            settings.Colors = 8;

            Assert.Equal(DitherMethod.Riemersma, settings.DitherMethod);
            settings.DitherMethod = null;
            Assert.Null(settings.DitherMethod);
            settings.DitherMethod = DitherMethod.No;
            Assert.Equal(DitherMethod.No, settings.DitherMethod);
            settings.MeasureErrors = true;
            Assert.True(settings.MeasureErrors);

            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var errorInfo = image.Quantize(settings);
#if Q8
                Assert.InRange(errorInfo.MeanErrorPerPixel, 7.066, 7.067);
#else
                Assert.InRange(errorInfo.MeanErrorPerPixel, 1827.8, 1827.9);
#endif
                Assert.InRange(errorInfo.NormalizedMaximumError, 0.352, 0.354);
                Assert.InRange(errorInfo.NormalizedMeanError, 0.001, 0.002);
            }
        }

        [Fact]
        public void Test_RandomThreshold()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.RandomThreshold((QuantumType)(Quantum.Max / 4), (QuantumType)(Quantum.Max / 2));

                ColorAssert.Equal(MagickColors.Black, image, 52, 52);
                ColorAssert.Equal(MagickColors.White, image, 75, 52);
                ColorAssert.Equal(MagickColors.Red, image, 31, 90);
                ColorAssert.Equal(MagickColors.Lime, image, 69, 90);
                ColorAssert.Equal(MagickColors.Blue, image, 120, 90);
            }
        }

        [Fact]
        public void Test_Raise_Lower()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Raise(30);

                ColorAssert.Equal(new MagickColor("#6ee29508b532"), image, 29, 30);
                ColorAssert.Equal(new MagickColor("#2f2054867aac"), image, 570, 265);
            }

            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Lower(30);

                ColorAssert.Equal(new MagickColor("#2da153c773f1"), image, 29, 30);
                ColorAssert.Equal(new MagickColor("#706195c7bbed"), image, 570, 265);
            }
        }

        [Fact]
        public void Test_RegionMask()
        {
            using (var red = new MagickImage("xc:red", 100, 100))
            {
                using (var green = new MagickImage("xc:green", 100, 100))
                {
                    green.RegionMask(new MagickGeometry(10, 10, 50, 50));

                    green.Composite(red, CompositeOperator.SrcOver);

                    ColorAssert.Equal(MagickColors.Green, green, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, green, 10, 10);
                    ColorAssert.Equal(MagickColors.Green, green, 60, 60);

                    green.RemoveRegionMask();

                    green.Composite(red, CompositeOperator.SrcOver);

                    ColorAssert.Equal(MagickColors.Red, green, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, green, 10, 10);
                    ColorAssert.Equal(MagickColors.Red, green, 60, 60);
                }
            }
        }

        [Fact]
        public void Test_Resample()
        {
            using (var image = new MagickImage("xc:red", 100, 100))
            {
                image.Resample(new PointD(300));

                Assert.Equal(300, image.Density.X);
                Assert.Equal(300, image.Density.Y);
                Assert.NotEqual(100, image.Width);
                Assert.NotEqual(100, image.Height);
            }
        }

        [Fact]
        public void Test_Roll()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.Roll(40, 60);

                MagickColor blue = new MagickColor("#a8dff8");
                ColorAssert.Equal(blue, image, 66, 103);
                ColorAssert.Equal(blue, image, 120, 86);
                ColorAssert.Equal(blue, image, 0, 82);
            }
        }

        [Fact]
        public void Test_Rotate()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                Assert.Equal(640, image.Width);
                Assert.Equal(480, image.Height);

                image.Rotate(90);

                Assert.Equal(480, image.Width);
                Assert.Equal(640, image.Height);
            }
        }

        [Fact]
        public void Test_RotationalBlur()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.RotationalBlur(20);

#if Q8
                ColorAssert.Equal(new MagickColor("#fbfbfb2b"), image, 10, 10);
                ColorAssert.Equal(new MagickColor("#8b0303"), image, 13, 67);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#167516", "#167616")), image, 63, 67);
                ColorAssert.Equal(new MagickColor("#3131fc"), image, 125, 67);
#else
                ColorAssert.Equal(new MagickColor("#fbf7fbf7fbf72aab"), image, 10, 10);
                ColorAssert.Equal(new MagickColor("#8b2102990299"), image, 13, 67);
                ColorAssert.Equal(new MagickColor("#159275F21592"), image, 63, 67);
                ColorAssert.Equal(new MagickColor("#31853185fd47"), image, 125, 67);
#endif
            }

            using (var image = new MagickImage(Files.TestPNG))
            {
                image.RotationalBlur(20, Channels.RGB);

#if Q8
                ColorAssert.Equal(new MagickColor("#fbfbfb80"), image, 10, 10);
                ColorAssert.Equal(new MagickColor("#8b0303"), image, 13, 67);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#167516", "#167616")), image, 63, 67);
                ColorAssert.Equal(new MagickColor("#3131fc"), image, 125, 67);
#else
                ColorAssert.Equal(new MagickColor("#fbf7fbf7fbf78000"), image, 10, 10);
                ColorAssert.Equal(new MagickColor("#8b2102990299"), image, 13, 67);
                ColorAssert.Equal(new MagickColor("#159275f21592"), image, 63, 67);
                ColorAssert.Equal(new MagickColor("#31853185fd47"), image, 125, 67);
#endif
            }
        }

        [Fact]
        public void Test_Sample()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Sample(400, 400);
                Assert.Equal(400, image.Width);
                Assert.Equal(300, image.Height);
            }
        }

        [Fact]
        public void Test_Scale()
        {
            using (var image = new MagickImage(Files.CirclePNG))
            {
                MagickColor color = MagickColor.FromRgba(255, 255, 255, 159);
                ColorAssert.Equal(color, image, image.Width / 2, image.Height / 2);

                image.Scale((Percentage)400);
                ColorAssert.Equal(color, image, image.Width / 2, image.Height / 2);
            }
        }

        [Fact]
        public void Test_Segment()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.Segment();

                ColorAssert.Equal(new MagickColor("#008300"), image, 77, 30);
                ColorAssert.Equal(new MagickColor("#f9f9f9"), image, 79, 30);
                ColorAssert.Equal(new MagickColor("#00c2fe"), image, 128, 62);
            }
        }

        [Fact]
        public void Test_SelectiveBlur()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                image.SelectiveBlur(5.0, 2.0, Quantum.Max / 2);

                using (var original = new MagickImage(Files.NoisePNG))
                {
                    Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.07775, 0.07779);
                }
            }
        }

        [Fact]
        public void Test_SepiaTone()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.SepiaTone();

#if Q8
                ColorAssert.Equal(new MagickColor("#472400"), image, 243, 45);
                ColorAssert.Equal(new MagickColor("#522e00"), image, 394, 394);
                ColorAssert.Equal(new MagickColor("#e4bb7c"), image, 477, 373);
#elif Q16
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#45be23e80000", "#475f24bf0000")), image, 243, 45);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#50852d680000", "#52672e770000")), image, 394, 394);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#e273b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#else
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#45be23e90001", "#475f24bf0000")), image, 243, 45);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#50862d690001", "#52672e770000")), image, 394, 394);
                ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#e274b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#endif
            }
        }

        [Fact]
        public void Test_SetAttenuate()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.SetAttenuate(5.6);
                Assert.Equal("5.6", image.GetArtifact("attenuate"));
            }
        }

        [Fact]
        public void Test_SetClippingPath()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                Assert.False(image.HasClippingPath);

                using (var path = new MagickImage(Files.InvitationTIF))
                {
                    string clippingPath = path.GetClippingPath();

                    image.SetClippingPath(clippingPath);

                    Assert.True(image.HasClippingPath);

                    image.SetClippingPath(clippingPath, "test");

                    Assert.NotNull(image.GetClippingPath("test"));
                    Assert.Null(image.GetClippingPath("#2"));
                }
            }
        }

        [Fact]
        public void Test_Shade()
        {
            using (var image = new MagickImage())
            {
                image.Settings.FontPointsize = 90;
                image.Read("label:Magick.NET");

                image.Shade();

                ColorAssert.Equal(new MagickColor("#7fff7fff7fff"), image, 64, 48);
                ColorAssert.Equal(MagickColors.Black, image, 118, 48);
                ColorAssert.Equal(new MagickColor("#7fff7fff7fff"), image, 148, 48);
            }

            using (var image = new MagickImage())
            {
                image.Settings.FontPointsize = 90;
                image.Read("label:Magick.NET");

                image.Shade(10, 20, false, Channels.Composite);

                ColorAssert.Equal(new MagickColor("#000000000000578e"), image, 64, 48);
                ColorAssert.Equal(new MagickColor("#0000000000000000"), image, 118, 48);
                ColorAssert.Equal(new MagickColor("#578e578e578e578e"), image, 148, 48);
            }
        }

        [Fact]
        public void Test_Shadow()
        {
            using (var image = new MagickImage())
            {
                image.Settings.BackgroundColor = MagickColors.Transparent;
                image.Settings.FontPointsize = 60;
                image.Read("label:Magick.NET");

                int width = image.Width;
                int height = image.Height;

                image.Shadow(2, 2, 5, new Percentage(50), MagickColors.Red);

                Assert.Equal(width + 20, image.Width);
                Assert.Equal(height + 20, image.Height);

                using (var pixels = image.GetPixels())
                {
                    var pixel = pixels.GetPixel(90, 9);
                    Assert.Equal(0, pixel.ToColor().A);

                    pixel = pixels.GetPixel(34, 55);
#if Q8
                    Assert.Equal(68, pixel.ToColor().A);
#else
                    Assert.InRange(pixel.ToColor().A, 17057, 17058);
#endif
                }
            }
        }

        [Fact]
        public void Test_Shave()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Shave(20, 40);

                Assert.Equal(600, image.Width);
                Assert.Equal(400, image.Height);
            }
        }

        [Fact]
        public void Test_Shear()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.BackgroundColor = MagickColors.Firebrick;
                image.VirtualPixelMethod = VirtualPixelMethod.Background;
                image.Shear(20, 40);

#if Q8
                ColorAssert.Equal(MagickColors.Firebrick, image, 45, 6);
                ColorAssert.Equal(new MagickColor("#807b7bff"), image, 98, 86);
                ColorAssert.Equal(MagickColors.Firebrick, image, 158, 181);
#else
                ColorAssert.Equal(MagickColors.Firebrick, image, 45, 6);
                ColorAssert.Equal(new MagickColor("#80a27ac17ac1ffff"), image, 98, 86);
                ColorAssert.Equal(MagickColors.Firebrick, image, 158, 181);
#endif
            }
        }

        [Fact]
        public void Test_Signature()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(0, image.Width);
                Assert.Equal(0, image.Height);
                Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", image.Signature);
            }
        }

        [Fact]
        public void Test_SparseColors()
        {
            MagickReadSettings settings = new MagickReadSettings();
            settings.Width = 600;
            settings.Height = 60;

            using (var image = new MagickImage("xc:", settings))
            {
                Assert.Throws<ArgumentNullException>("args", () =>
                {
                    image.SparseColor(Channels.Red, SparseColorMethod.Barycentric, null);
                });

                List<SparseColorArg> args = new List<SparseColorArg>();

                Assert.Throws<ArgumentException>("args", () =>
                {
                    image.SparseColor(Channels.Blue, SparseColorMethod.Barycentric, args);
                });

                using (var pixels = image.GetPixels())
                {
                    ColorAssert.Equal(pixels.GetPixel(0, 0).ToColor(), pixels.GetPixel(599, 59).ToColor());
                }

                Assert.Throws<ArgumentNullException>("color", () =>
                {
                    args.Add(new SparseColorArg(0, 0, null));
                });

                args.Add(new SparseColorArg(0, 0, MagickColors.SkyBlue));
                args.Add(new SparseColorArg(-600, 60, MagickColors.SkyBlue));
                args.Add(new SparseColorArg(600, 60, MagickColors.Black));

                image.SparseColor(SparseColorMethod.Barycentric, args);

                using (var pixels = image.GetPixels())
                {
                    ColorAssert.NotEqual(pixels.GetPixel(0, 0).ToColor(), pixels.GetPixel(599, 59).ToColor());
                }

                Assert.Throws<ArgumentException>("channels", () =>
                {
                    image.SparseColor(Channels.Black, SparseColorMethod.Barycentric, args);
                });
            }
        }

        [Fact]
        public void Test_Sketch()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Resize(400, 400);

                image.Sketch();
                image.ColorType = ColorType.Bilevel;

                ColorAssert.Equal(MagickColors.White, image, 63, 100);
                ColorAssert.Equal(MagickColors.White, image, 150, 175);
            }
        }

        [Fact]
        public void Test_Solarize()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Solarize();

                ColorAssert.Equal(MagickColors.Black, image, 125, 125);
                ColorAssert.Equal(new MagickColor("#007f7f"), image, 122, 143);
                ColorAssert.Equal(new MagickColor("#2e6935"), image, 435, 240);
            }
        }

        [Fact]
        public void Test_Splice()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                image.BackgroundColor = MagickColors.Fuchsia;
                image.Splice(new MagickGeometry(105, 50, 10, 20));

                Assert.Equal(296, image.Width);
                Assert.Equal(87, image.Height);
                ColorAssert.Equal(MagickColors.Fuchsia, image, 105, 50);
                ColorAssert.Equal(new MagickColor("#0000"), image, 115, 70);
            }
        }

        [Fact]
        public void Test_Spread()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Spread(10);

                using (var original = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.120, 0.123);
                }
            }
        }

        [Fact]
        public void Test_Statistic()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                image.Statistic(StatisticType.Minimum, 10, 1);

                ColorAssert.Equal(MagickColors.Black, image, 42, 119);
                ColorAssert.Equal(new MagickColor("#eeeeeeeeeeee"), image, 90, 120);
                ColorAssert.Equal(new MagickColor("#999999999999"), image, 90, 168);
            }
        }

        [Fact]
        public void Test_Stereo()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Flop();

                using (var rightImage = new MagickImage(Files.Builtin.Logo))
                {
                    image.Stereo(rightImage);

                    ColorAssert.Equal(new MagickColor("#2222ffffffff"), image, 250, 375);
                    ColorAssert.Equal(new MagickColor("#ffff3e3e9292"), image, 380, 375);
                }
            }
        }

        [Fact]
        public void Test_Swirl()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Alpha(AlphaOption.Deactivate);

                ColorAssert.Equal(MagickColors.Red, image, 287, 74);
                ColorAssert.NotEqual(MagickColors.White, image, 363, 333);

                image.Swirl(60);

                ColorAssert.NotEqual(MagickColors.Red, image, 287, 74);
                ColorAssert.Equal(MagickColors.White, image, 363, 333);
            }
        }

        [Fact]
        public void Test_SubImageSearch()
        {
            using (var images = new MagickImageCollection())
            {
                images.Add(new MagickImage(MagickColors.Green, 2, 2));
                images.Add(new MagickImage(MagickColors.Red, 2, 2));

                using (var combined = images.AppendHorizontally())
                {
                    using (var searchResult = combined.SubImageSearch(new MagickImage(MagickColors.Red, 1, 1), ErrorMetric.RootMeanSquared))
                    {
                        Assert.NotNull(searchResult);
                        Assert.NotNull(searchResult.SimilarityImage);
                        Assert.NotNull(searchResult.BestMatch);
                        Assert.Equal(0.0, searchResult.SimilarityMetric);
                        Assert.Equal(2, searchResult.BestMatch.X);
                        Assert.Equal(0, searchResult.BestMatch.Y);
                        Assert.Equal(1, searchResult.BestMatch.Width);
                        Assert.Equal(1, searchResult.BestMatch.Height);
                    }
                }
            }
        }

        [Fact]
        public void Test_Texture()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                using (var canvas = new MagickImage(MagickColors.Fuchsia, 300, 300))
                {
                    canvas.Texture(image);

                    ColorAssert.Equal(MagickColors.Fuchsia, canvas, 72, 68);
                    ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), canvas, 299, 48);
                    ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), canvas, 160, 299);
                }
            }
        }

        [Fact]
        public void Test_Tile()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                using (var checkerboard = new MagickImage(Files.Patterns.Checkerboard))
                {
                    image.Opaque(MagickColors.White, MagickColors.Transparent);
                    image.Tile(checkerboard, CompositeOperator.DstOver);

                    ColorAssert.Equal(new MagickColor("#66"), image, 578, 260);
                }
            }
        }

        [Fact]
        public void Test_Tint()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Settings.FillColor = MagickColors.Gold;
                image.Tint("1x2");
                image.Clamp();

                ColorAssert.Equal(new MagickColor("#dee500000000"), image, 400, 205);
                ColorAssert.Equal(MagickColors.Black, image, 400, 380);
            }
        }

        [Fact]
        public void Test_Threshold()
        {
            using (var image = new MagickImage(Files.ImageMagickJPG))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Threshold(new Percentage(80));
                    image.Settings.Compression = CompressionMethod.Group4;
                    image.Format = MagickFormat.Pdf;
                    image.Write(memStream);
                }
            }
        }

        [Fact]
        public void Test_Thumbnail()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                image.Thumbnail(100, 100);
                Assert.Equal(100, image.Width);
                Assert.Equal(23, image.Height);
            }
        }

        [Fact]
        public void Test_ToByteArray()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                byte[] bytes = image.ToByteArray(MagickFormat.Dds);

                image.Read(bytes);
                Assert.Equal(CompressionMethod.DXT5, image.Compression);
                Assert.Equal(MagickFormat.Dds, image.Format);

                bytes = image.ToByteArray(MagickFormat.Jpg);

                image.Read(bytes);
                Assert.Equal(MagickFormat.Jpg, image.Format);

                bytes = image.ToByteArray(MagickFormat.Dds);

                image.Read(bytes);
                Assert.Equal(CompressionMethod.DXT1, image.Compression);
                Assert.Equal(MagickFormat.Dds, image.Format);
            }
        }

        [Fact]
        public void Test_ToString()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                Assert.Equal("Gif 480x640 8-bit sRGB", image.ToString());
            }

            using (var image = new MagickImage(Files.TestPNG))
            {
                Assert.Equal("Png 150x100 16-bit sRGB", image.ToString());
            }
        }

        [Fact]
        public void Test_TotalColors()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(0, image.TotalColors);

                image.Read(Files.Builtin.Logo);
                Assert.NotEqual(0, image.TotalColors);
            }
        }

        [Fact]
        public void Test_Transparent()
        {
            MagickColor red = new MagickColor("red");
            MagickColor transparentRed = new MagickColor("red");
            transparentRed.A = 0;

            using (var image = new MagickImage(Files.RedPNG))
            {
                ColorAssert.Equal(red, image, 0, 0);

                image.Transparent(red);

                ColorAssert.Equal(transparentRed, image, 0, 0);
                ColorAssert.NotEqual(transparentRed, image, image.Width - 1, 0);
            }

            using (var image = new MagickImage(Files.RedPNG))
            {
                ColorAssert.Equal(red, image, 0, 0);

                image.InverseTransparent(red);

                ColorAssert.NotEqual(transparentRed, image, 0, 0);
                ColorAssert.Equal(transparentRed, image, image.Width - 1, 0);
            }
        }

        [Fact]
        public void Test_TransparentChroma()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.TransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

                ColorAssert.Equal(new MagickColor("#3962396239620000"), image, 50, 50);
                ColorAssert.Equal(new MagickColor("#0000"), image, 32, 80);
                ColorAssert.Equal(new MagickColor("#f6def6def6deffff"), image, 132, 42);
                ColorAssert.Equal(new MagickColor("#0000808000000000"), image, 74, 79);
            }

            using (var image = new MagickImage(Files.TestPNG))
            {
                image.InverseTransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

                ColorAssert.Equal(new MagickColor("#396239623962ffff"), image, 50, 50);
                ColorAssert.Equal(new MagickColor("#000f"), image, 32, 80);
                ColorAssert.Equal(new MagickColor("#f6def6def6de0000"), image, 132, 42);
                ColorAssert.Equal(new MagickColor("#000080800000ffff"), image, 74, 79);
            }
        }

        [Fact]
        public void Test_Transpose()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Transpose();

                Assert.Equal(480, image.Width);
                Assert.Equal(640, image.Height);

                ColorAssert.Equal(MagickColors.Red, image, 61, 292);
                ColorAssert.Equal(new MagickColor("#f5f5eeee3636"), image, 104, 377);
                ColorAssert.Equal(new MagickColor("#eded1f1f2424"), image, 442, 391);
            }
        }

        [Fact]
        public void Test_Transverse()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Transverse();

                Assert.Equal(480, image.Width);
                Assert.Equal(640, image.Height);

                ColorAssert.Equal(MagickColors.Red, image, 330, 508);
                ColorAssert.Equal(new MagickColor("#f5f5eeee3636"), image, 288, 474);
                ColorAssert.Equal(new MagickColor("#cdcd20202727"), image, 30, 123);
            }
        }

        [Fact]
        public void Test_UniqueColors()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                using (var uniqueColors = image.UniqueColors())
                {
                    Assert.Equal(1, uniqueColors.Height);
                    Assert.Equal(256, uniqueColors.Width);
                }
            }
        }

        [Fact]
        public void Test_UnsharpMask()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
                image.UnsharpMask(7.0, 3.0);

                using (var original = new MagickImage(Files.NoisePNG))
                {
#if Q8 || Q16
                    Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.06476, 0.06478);
#else
                    Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.10234, 0.10235);
#endif
                }
            }
        }

        [Fact]
        public void Test_Vignette()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.BackgroundColor = MagickColors.Aqua;
                image.Vignette();

                ColorAssert.Equal(new MagickColor("#6480ffffffff"), image, 292, 0);
                ColorAssert.Equal(new MagickColor("#91acffffffff"), image, 358, 479);
            }
        }

        [Fact]
        public void Test_VirtualPixelMethod()
        {
            using (var image = new MagickImage())
            {
                Assert.Equal(VirtualPixelMethod.Undefined, image.VirtualPixelMethod);
                image.VirtualPixelMethod = VirtualPixelMethod.Random;
                Assert.Equal(VirtualPixelMethod.Random, image.VirtualPixelMethod);
            }
        }

        [Fact]
        public void Test_Wave()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.Wave();

                using (var original = new MagickImage(Files.TestPNG))
                {
                    Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.62619, 0.62623);
                }
            }
        }

        [Fact]
        public void Test_WaveletDenoise()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
#if Q8
                var color = new MagickColor("#dd");
#elif Q16
                var color = new MagickColor(OpenCLValue.Get("#dea4dea4dea4", "#deb5deb5deb5"));
#else
                var color = new MagickColor(OpenCLValue.Get("#dea5dea5dea5", "#deb5deb5deb5"));
#endif

                ColorAssert.NotEqual(color, image, 130, 123);

                image.ColorType = ColorType.TrueColor;
                image.WaveletDenoise((Percentage)25);

                ColorAssert.Equal(color, image, 130, 123);
            }
        }

        [Fact]
        public void Test_Warning()
        {
            int count = 0;
            EventHandler<WarningEventArgs> warningDelegate = (sender, arguments) =>
            {
                Assert.NotNull(sender);
                Assert.NotNull(arguments);
                Assert.NotNull(arguments.Message);
                Assert.NotEqual(string.Empty, arguments.Message);
                Assert.NotNull(arguments.Exception);

                count++;
            };

            using (var image = new MagickImage())
            {
                image.Warning += warningDelegate;
                image.Read(Files.EightBimTIF);

                Assert.NotEqual(0, count);

                int expectedCount = count;
                image.Warning -= warningDelegate;
                image.Read(Files.EightBimTIF);

                Assert.Equal(expectedCount, count);
            }
        }

        [Fact]
        public void Test_WhiteThreshold()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.WhiteThreshold(new Percentage(10));
                ColorAssert.Equal(MagickColors.White, image, 43, 74);
                ColorAssert.Equal(MagickColors.White, image, 60, 74);
            }
        }

        [Fact]
        public void Test_Write()
        {
            Assert.Throws<ArgumentNullException>("file", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write((FileInfo)null);
                }
            });

            Assert.Throws<ArgumentNullException>("defines", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write(new FileInfo("foo"), null);
                }
            });

            Assert.Throws<ArgumentNullException>("fileName", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write((string)null);
                }
            });

            Assert.Throws<ArgumentException>("fileName", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write(string.Empty);
                }
            });

            Assert.Throws<ArgumentNullException>("defines", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write("foo", null);
                }
            });

            Assert.Throws<ArgumentNullException>("stream", () =>
            {
                using (var image = new MagickImage())
                {
                    image.Write((Stream)null);
                }
            });

            Assert.Throws<ArgumentNullException>("defines", () =>
            {
                using (var image = new MagickImage())
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        image.Write(memStream, null);
                    }
                }
            });

            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                long fileSize = new FileInfo(Files.SnakewarePNG).Length;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);

                    Assert.Equal(fileSize, memStream.Length);

                    memStream.Position = 0;

                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(image.Width, result.Width);
                        Assert.Equal(image.Height, result.Height);
                        Assert.Equal(MagickFormat.Png, result.Format);
                    }
                }
            }

            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                MagickFormat format = MagickFormat.Bmp;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream, format);

                    memStream.Position = 0;

                    using (var result = new MagickImage(memStream))
                    {
                        Assert.Equal(image.Width, result.Width);
                        Assert.Equal(image.Height, result.Height);
                        Assert.Equal(format, result.Format);
                    }
                }
            }

            string fileName = Path.GetTempFileName();
            try
            {
                var file = new FileInfo(Files.SnakewarePNG);
                using (var image = new MagickImage(file))
                {
                    image.Write(fileName);

                    FileInfo output = new FileInfo(fileName);
                    Assert.Equal(file.Length, output.Length);
                }
            }
            finally
            {
                Cleanup.DeleteFile(fileName);
            }

            fileName = Path.GetTempFileName();
            try
            {
                var file = new FileInfo(Files.SnakewarePNG);
                using (var image = new MagickImage(file))
                {
                    FileInfo output = new FileInfo(fileName);
                    image.Write(output);

                    Assert.Equal(file.Length, output.Length);
                }
            }
            finally
            {
                Cleanup.DeleteFile(fileName);
            }
        }

        private static void AssertChromaticity(double expectedX, double expectedY, double expectedZ, IPrimaryInfo info)
        {
            Assert.InRange(info.X, expectedX, expectedX + 0.001);
            Assert.InRange(info.Y, expectedY, expectedY + 0.001);
            Assert.InRange(info.Z, expectedZ, expectedZ + 0.001);
        }

        private static void AssertClone(IMagickImage<QuantumType> first, IMagickImage<QuantumType> second)
        {
            Assert.Equal(first, second);
            second.Format = MagickFormat.Jp2;
            Assert.Equal(MagickFormat.Png, first.Format);
            Assert.Equal(MagickFormat.Jp2, second.Format);
            second.Dispose();
            Assert.Equal(MagickFormat.Png, first.Format);
        }

        private static void AssertCloneArea(IMagickImage<QuantumType> area, IMagickImage<QuantumType> part)
        {
            Assert.Equal(area.Width, part.Width);
            Assert.Equal(area.Height, part.Height);

            Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
        }

        private IMagickImage<QuantumType> CreatePallete()
        {
            using (var images = new MagickImageCollection())
            {
                images.Add(new MagickImage(MagickColors.Red, 1, 1));
                images.Add(new MagickImage(MagickColors.Blue, 1, 1));
                images.Add(new MagickImage(MagickColors.Green, 1, 1));

                return images.AppendHorizontally();
            }
        }
    }
}
