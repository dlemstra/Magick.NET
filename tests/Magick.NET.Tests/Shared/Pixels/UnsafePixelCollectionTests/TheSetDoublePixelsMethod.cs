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
    public partial class UnsafePixelCollectionTests
    {
        public class TheSetDoublePixelsMethod
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenArrayIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetDoublePixels((double[])null);
                    }
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArrayHasInvalidSize()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetDoublePixels(new double[] { 0, 0, 0, 0 });
                    }
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArrayIsTooLong()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var values = new double[(image.Width * image.Height * image.ChannelCount) + 1];
                        pixels.SetDoublePixels(values);
                    }
                }
            }

            [Fact]
            public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var values = new double[image.Width * image.Height * image.ChannelCount];
                        pixels.SetDoublePixels(values);

                        ColorAssert.Equal(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }
        }
    }
}