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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class SafePixelCollectionTests
    {
        public class TheGetPixelMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthOutsideImage()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            pixels.GetPixel(image.Width + 1, 0);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightOutsideImage()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentOutOfRangeException>("y", () =>
                        {
                            pixels.GetPixel(0, image.Height + 1);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldReturnPixelWhenIndexInsideImage()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var pixel = pixels.GetPixel(55, 68);
                        ColorAssert.Equal(new MagickColor("#a8dff8ff"), pixel.ToColor());
                    }
                }
            }
        }
    }
}
