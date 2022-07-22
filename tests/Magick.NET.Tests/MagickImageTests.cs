// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
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
    public partial class MagickImageTests
    {
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
                var color = MagickColor.FromRgba(255, 255, 255, 159);
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
                    var clippingPath = path.GetClippingPath();

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

                var width = image.Width;
                var height = image.Height;

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
            var settings = new MagickReadSettings();
            settings.Width = 600;
            settings.Height = 60;

            using (var image = new MagickImage("xc:", settings))
            {
                Assert.Throws<ArgumentNullException>("args", () =>
                {
                    image.SparseColor(Channels.Red, SparseColorMethod.Barycentric, null);
                });

                var args = new List<SparseColorArg>();

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
