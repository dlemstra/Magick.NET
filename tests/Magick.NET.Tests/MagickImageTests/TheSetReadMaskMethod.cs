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
    public partial class MagickImageTests
    {
        public class TheSetReadMaskMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        image.SetReadMask(null);
                    });
                }
            }

            [Fact]
            public void ShouldSetMaskForWholeImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var imageMask = new MagickImage(MagickColors.White, 10, 15))
                    {
                        image.SetReadMask(imageMask);

                        using (var mask = image.GetReadMask())
                        {
                            Assert.NotNull(mask);
                            Assert.Equal(640, mask.Width);
                            Assert.Equal(480, mask.Height);
                            ColorAssert.Equal(MagickColors.White, mask, 9, 14);
                            ColorAssert.Equal(MagickColors.Black, mask, 10, 15);
                        }
                    }
                }
            }
        }
    }
}