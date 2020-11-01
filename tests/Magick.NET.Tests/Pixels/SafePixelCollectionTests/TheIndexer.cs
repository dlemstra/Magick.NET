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
        public class TheIndexer
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthOutOfRange()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            var pixel = pixels[image.Width + 1, 0];
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightOutOfRange()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        Assert.Throws<ArgumentOutOfRangeException>("y", () =>
                        {
                            var pixel = pixels[0, image.Height + 1];
                        });
                    }
                }
            }

            [Fact]
            public void ShouldReturnPixelWhenIndexIsCorrect()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var pixel = pixels[300, 100];

                        ColorAssert.Equal(MagickColors.Red, pixel.ToColor());
                    }
                }
            }
        }
    }
}
