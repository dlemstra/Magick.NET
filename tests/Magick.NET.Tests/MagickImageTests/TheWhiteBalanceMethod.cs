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
    public partial class MagickImageTests
    {
        public class TheWhiteBalanceMethod
        {
            [Fact]
            public void ShouldWhiteBalanceTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Rose))
                {
                    image.WhiteBalance();
#if Q8
                    ColorAssert.Equal(new MagickColor("#dd4946"), image, 45, 25);
#elif Q16
                    ColorAssert.Equal(new MagickColor("#de494a714699"), image, 45, 25);
#else
                    ColorAssert.Equal(new MagickColor("#de494a714698"), image, 45, 25);
#endif
                }
            }

            [Fact]
            public void ShouldUseTheVibrance()
            {
                using (var image = new MagickImage(Files.Builtin.Rose))
                {
                    image.WhiteBalance(new Percentage(70));
#if Q8
                    ColorAssert.Equal(new MagickColor("#00a13b"), image, 45, 25);
#elif Q16
                    ColorAssert.Equal(new MagickColor("#0000a2043c3d"), image, 45, 25);
#else
                    image.Clamp();
                    ColorAssert.Equal(new MagickColor("#0000a2033c3c"), image, 45, 25);
#endif
                }
            }
        }
    }
}
