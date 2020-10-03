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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class SafePixelCollectionTests
    {
        public class TheSetIntAreaMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentNullException>("values", () =>
                        {
                            pixels.SetIntArea(10, 10, 1000, 1000, null);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayHasInvalidSize()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetIntArea(10, 10, 1000, 1000, new int[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayHasTooManyValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new int[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                            pixels.SetIntArea(10, 10, 113, 108, values);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = new int[113 * 108 * image.ChannelCount];
                        pixels.SetIntArea(10, 10, 113, 108, values);

                        ColorAssert.Equal(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsSpecifiedAndGeometryIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            pixels.SetIntArea(null, new int[] { 0 });
                        });
                    }
                }
            }

            [Fact]
            public void ShouldChangePixelsWhenGeometryAndArrayAreSpecified()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = new int[113 * 108 * image.ChannelCount];
                        pixels.SetIntArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.Equal(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }
        }
    }
}