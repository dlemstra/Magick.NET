// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public partial class MagickImageTests
    {
        public class TheFxMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenExpressionIsNull()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.Throws<ArgumentNullException>("expression", () => image.Fx(null));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenExpressionIsEmpty()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.Throws<ArgumentException>("expression", () => image.Fx(string.Empty));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenExpressionIsInvalid()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.Throws<MagickOptionErrorException>(() => image.Fx("foobar"));
                }
            }

            [Fact]
            public void ShouldEvaluateTheExpression()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Fx("b");

                    ColorAssert.Equal(MagickColors.Black, image, 183, 83);
                    ColorAssert.Equal(MagickColors.White, image, 140, 400);

                    image.Fx("1/2", Channels.Green);

                    ColorAssert.Equal(new MagickColor("#000080000000"), image, 183, 83);
                    ColorAssert.Equal(new MagickColor("#ffff8000ffff"), image, 140, 400);

                    image.Fx("1/4", Channels.Alpha);

                    ColorAssert.Equal(new MagickColor("#000080000000"), image, 183, 83);
                    ColorAssert.Equal(new MagickColor("#ffff8000ffff"), image, 140, 400);

                    image.HasAlpha = true;
                    image.Fx("1/4", Channels.Alpha);

                    ColorAssert.Equal(new MagickColor("#0000800000004000"), image, 183, 83);
                    ColorAssert.Equal(new MagickColor("#ffff8000ffff4000"), image, 140, 400);
                }
            }

            [Fact]
            public void ShouldEvaluateExpressionMethod()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Fx("rand()");
                }
            }
        }
    }
}
