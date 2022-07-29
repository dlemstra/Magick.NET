// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
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
                using (var memStream = new MemoryStream())
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
                var bytes = image.ToByteArray(MagickFormat.Dds);

                image.Read(bytes);
                Assert.Equal(CompressionMethod.DXT5, image.Compression);
                Assert.Equal(MagickFormat.Dds, image.Format);

                bytes = image.ToByteArray(MagickFormat.Jpg);

                image.Read(bytes);
                Assert.Equal(MagickFormat.Jpeg, image.Format);

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
            var red = new MagickColor("red");
            var transparentRed = new MagickColor("red");
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
        public void Test_WhiteThreshold()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.WhiteThreshold(new Percentage(10));
                ColorAssert.Equal(MagickColors.White, image, 43, 74);
                ColorAssert.Equal(MagickColors.White, image, 60, 74);
            }
        }
    }
}
