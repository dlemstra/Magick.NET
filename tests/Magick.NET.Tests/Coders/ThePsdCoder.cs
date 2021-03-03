// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ThePsdCoder
    {
        [Fact]
        public void ShouldReadTheCorrectColors()
        {
            using (var image = new MagickImage(Files.Coders.PlayerPSD))
            {
                ColorAssert.Equal(MagickColors.White, image, 0, 0);

                ColorAssert.Equal(MagickColor.FromRgb(15, 43, 255), image, 8, 6);
            }
        }

        [Fact]
        public void ShouldReadTheProfileForAllLayers()
        {
            using (var images = new MagickImageCollection(Files.Coders.LayerStylesSamplePSD))
            {
                Assert.Equal(4, images.Count);

                foreach (var image in images)
                {
                    Assert.NotNull(image.Get8BimProfile());
                }
            }
        }

        [Fact]
        public void ShouldCorrectlyWriteGrayscaleImage()
        {
            using (var input = new MagickImage(Files.Builtin.Wizard))
            {
                input.Quantize(new QuantizeSettings
                {
                    Colors = 10,
                    ColorSpace = ColorSpace.Gray,
                });

                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Psd);

                    memoryStream.Position = 0;
                    using (var output = new MagickImage(memoryStream))
                    {
                        var distortion = output.Compare(input, ErrorMetric.RootMeanSquared);

                        Assert.InRange(distortion, 0.000, 0.0014);
                    }
                }
            }
        }

        [Fact]
        public void ShouldCorrectlyWriteGrayscaleAlphaImage()
        {
            using (var input = new MagickImage(Files.Builtin.Wizard))
            {
                input.Quantize(new QuantizeSettings
                {
                    Colors = 10,
                    ColorSpace = ColorSpace.Gray,
                });

                input.Alpha(AlphaOption.Opaque);

                using (var memoryStream = new MemoryStream())
                {
                    input.Settings.Compression = CompressionMethod.RLE;
                    input.Write(memoryStream, MagickFormat.Psd);

                    memoryStream.Position = 0;
                    using (var output = new MagickImage(memoryStream))
                    {
                        var distortion = output.Compare(input, ErrorMetric.RootMeanSquared);

                        Assert.InRange(distortion, 0.000, 0.001);
                    }
                }
            }
        }
    }
}