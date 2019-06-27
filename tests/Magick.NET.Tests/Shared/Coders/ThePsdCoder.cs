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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ThePsdCoder
    {
        [TestMethod]
        public void ShouldReadTheCorrectColors()
        {
            using (IMagickImage image = new MagickImage(Files.Coders.PlayerPSD))
            {
                ColorAssert.AreEqual(MagickColors.White, image, 0, 0);

                ColorAssert.AreEqual(MagickColor.FromRgb(15, 43, 255), image, 8, 6);
            }
        }

        [TestMethod]
        public void ShouldReadTheProfileForAllLayers()
        {
            using (IMagickImageCollection images = new MagickImageCollection(Files.Coders.LayerStylesSamplePSD))
            {
                Assert.AreEqual(4, images.Count);

                foreach (var image in images)
                {
                    Assert.IsNotNull(image.Get8BimProfile());
                }
            }
        }

        [TestMethod]
        public void ShouldCorrectlyWriteGrayscaleImage()
        {
            using (IMagickImage input = new MagickImage(Files.Builtin.Wizard))
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
                    using (IMagickImage output = new MagickImage(memoryStream))
                    {
                        var distortion = output.Compare(input, ErrorMetric.RootMeanSquared);

                        Assert.AreEqual(0, distortion, 0.002);
                    }
                }
            }
        }

        [TestMethod]
        public void ShouldCorrectlyWriteGrayscaleAlphaImage()
        {
            using (IMagickImage input = new MagickImage(Files.Builtin.Wizard))
            {
                input.Quantize(new QuantizeSettings
                {
                    Colors = 10,
                    ColorSpace = ColorSpace.Gray,
                });

                input.Alpha(AlphaOption.Opaque);

                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Psd);

                    memoryStream.Position = 0;
                    using (IMagickImage output = new MagickImage(memoryStream))
                    {
                        var distortion = output.Compare(input, ErrorMetric.RootMeanSquared);

                        Assert.AreEqual(0, distortion, 0.002);
                    }
                }
            }
        }
    }
}