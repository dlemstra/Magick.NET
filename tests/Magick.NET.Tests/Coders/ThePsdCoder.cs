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