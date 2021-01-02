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
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheOrderedDitherMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenThresholdMapIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("thresholdMap", () => image.OrderedDither(null));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenThresholdMapIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentException>("thresholdMap", () => image.OrderedDither(string.Empty));
                }
            }

            [Fact]
            public void ShouldPerformAnOrderedDither()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        image.OrderedDither("h4x4a");

                        ColorAssert.Equal(MagickColors.Yellow, image, 299, 212);
                        ColorAssert.Equal(MagickColors.Red, image, 314, 228);
                        ColorAssert.Equal(MagickColors.Black, image, 448, 159);
                    }
                });
            }
        }
    }
}
