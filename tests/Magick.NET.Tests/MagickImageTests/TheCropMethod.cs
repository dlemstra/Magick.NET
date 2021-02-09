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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheCropMethod
        {
            [Fact]
            public void ShouldSetImageToCorrectDimensions()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(40, 50);

                    Assert.Equal(40, image.Width);
                    Assert.Equal(50, image.Height);
                }
            }

            [Fact]
            public void ShouldUseUndefinedGravityAsTheDefault()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(150, 40);

                    Assert.Equal(150, image.Width);
                    Assert.Equal(40, image.Height);

                    ColorAssert.Equal(new MagickColor("#fecd08ff"), image, 146, 25);
                }
            }

            [Fact]
            public void ShouldUseCenterGravity()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(50, 40, Gravity.Center);

                    Assert.Equal(50, image.Width);
                    Assert.Equal(40, image.Height);

                    ColorAssert.Equal(new MagickColor("#223e92ff"), image, 25, 20);
                }
            }

            [Fact]
            public void ShouldUseEastGravity()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(50, 40, Gravity.East);

                    Assert.Equal(50, image.Width);
                    Assert.Equal(40, image.Height);
                    ColorAssert.Equal(MagickColors.White, image, 25, 20);
                }
            }

            [Fact]
            public void ShouldUseAspectRatioOfMagickGeometry()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("3:2"));

                    Assert.Equal(640, image.Width);
                    Assert.Equal(427, image.Height);
                    ColorAssert.Equal(MagickColors.White, image, 222, 0);
                }
            }

            [Fact]
            public void ShouldUseAspectRatioOfMagickGeometryAndGravity()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("3:2"), Gravity.South);

                    Assert.Equal(640, image.Width);
                    Assert.Equal(427, image.Height);
                    ColorAssert.Equal(MagickColors.Red, image, 222, 0);
                }
            }

            [Fact]
            public void ShouldUseOffsetFromMagickGeometryAndGravity()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry(10, 10, 100, 100), Gravity.Center);

                    Assert.Equal(100, image.Width);
                    Assert.Equal(100, image.Height);
                    ColorAssert.Equal(MagickColors.White, image, 99, 99);
                }
            }

            [Fact]
            public void ShouldUseUndefinedGravityAsTheDefaultForMagickGeometry()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("150x40"));

                    Assert.Equal(150, image.Width);
                    Assert.Equal(40, image.Height);

                    ColorAssert.Equal(new MagickColor("#fecd08ff"), image, 146, 25);
                }
            }
        }
    }
}
