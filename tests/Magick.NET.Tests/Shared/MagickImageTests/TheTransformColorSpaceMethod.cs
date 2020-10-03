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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheTransformColorSpaceMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenTheImageHasNoProfile()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var result = image.TransformColorSpace(ColorProfile.AdobeRGB1998);

                    Assert.False(result);
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenTheImageHasProfile()
            {
                using (var image = new MagickImage(Files.PictureJPG))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB);

                    Assert.True(result);
                }
            }

            [Fact]
            public void ShouldReturnFalseWhenSourceProfileColorSpaceIsIncorrect()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var result = image.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.AdobeRGB1998);

                    Assert.False(result);
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrect()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);

                    Assert.True(result);
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrectAndTheImageHasNoProfile()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);

                    Assert.True(result);
                }
            }

            [Fact]
            public void ShouldNotChangeTheColorSpaceWhenSourceColorSpaceIsIncorrect()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.Equal(ColorSpace.sRGB, image.ColorSpace);

                    image.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.USWebCoatedSWOP);
                    Assert.Equal(ColorSpace.sRGB, image.ColorSpace);
                }
            }

            [Fact]
            public void ShouldChangeTheColorSpace()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.Equal(ColorSpace.sRGB, image.ColorSpace);

                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
                    Assert.Equal(ColorSpace.CMYK, image.ColorSpace);
                }
            }

            [Fact]
            public void ShouldClampPixels()
            {
                using (var image = new MagickImage(MagickColors.White, 1, 1))
                {
                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);
#if Q8 || Q16
                    ColorAssert.Equal(new MagickColor("#ffffff"), image, 1, 1);
#else
                    ColorAssert.Equal(new MagickColor(65538f, 65531f, 65542f, 65535f), image, 1, 1);
#endif
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedMode()
            {
                using (var quantumImage = new MagickImage(Files.PictureJPG))
                {
                    quantumImage.TransformColorSpace(ColorProfile.USWebCoatedSWOP);

                    using (var highResImage = new MagickImage(Files.PictureJPG))
                    {
                        highResImage.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorTransformMode.HighRes);

                        var difference = quantumImage.Compare(highResImage, ErrorMetric.RootMeanSquared);

#if Q16HDRI
                        Assert.Equal(0.0, difference);
#else
                        Assert.NotEqual(0.0, difference);
#endif
                    }
                }
            }
        }
    }
}
