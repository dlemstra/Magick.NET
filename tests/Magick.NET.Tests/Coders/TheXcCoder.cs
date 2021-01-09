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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheXcCoder
    {
        [Fact]
        public void ShouldHandleSrgbaColor()
        {
            using (var image = new MagickImage("xc:srgba(255,0,0,1)", 1, 1))
            {
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            }
        }

        [Fact]
        public void ShouldHandleRgbColor()
        {
            using (var image = new MagickImage("xc:rgb(0,50%,0)", 1, 1))
            {
                ColorAssert.Equal(new MagickColor("#000080000000"), image, 0, 0);
            }
        }

        [Fact]
        public void ShouldHandleHslaColor()
        {
            using (var image = new MagickImage("xc:hsla(180,255,127.5,0.5)", 1, 1))
            {
                ColorAssert.Equal(new MagickColor("#0000ffffffff8080"), image, 0, 0);
            }
        }
    }
}