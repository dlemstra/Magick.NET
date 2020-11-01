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
    public partial class ColorHSLTests : ColorBaseTests<ColorHSL>
    {
        [Fact]
        public void Test_IComparable()
        {
            ColorHSL first = new ColorHSL(0.4, 0.3, 0.2);

            AssertIComparable(first);

            ColorHSL second = new ColorHSL(0.1, 0.2, 0.3);

            Test_IComparable_FirstLower(first, second);

            second = new ColorHSL(0.4, 0.3, 0.2);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorHSL first = new ColorHSL(1.0, 0.5, 0.5);

            Test_IEquatable_NullAndSelf(first);

            ColorHSL second = new ColorHSL(1.0, 0.5, 0.5);

            Test_IEquatable_Equal(first, second);

            second = new ColorHSL(0.2, 0.5, 1.0);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorHSL expected = new ColorHSL(1.0, 1.0, 1.0);
            ColorHSL actual = MagickColors.White;
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, MagickColors.White);

            Assert.Null(ColorHSL.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorHSL color = new ColorHSL(1.0, 1.0, 1.0);
            AssertToString(color, MagickColors.White);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorHSL color = new ColorHSL(0, 0, 0);

            color.Hue = 1;
            Assert.Equal(1, color.Hue);
            Assert.Equal(0, color.Saturation);
            Assert.Equal(0, color.Lightness);

            color.Saturation = 2;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(0, color.Lightness);

            color.Lightness = 3;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(3, color.Lightness);
        }

        [Fact]
        public void Test_ColorHSL()
        {
            ColorHSL color = new MagickColor("#010203");
            ColorAssert.Equal(new MagickColor("#010203"), color.ToMagickColor());

            color = new MagickColor("#aabbcc");
            ColorAssert.Equal(new MagickColor("#aabbcc"), color.ToMagickColor());

            color = new MagickColor("#e0d8d9");
            ColorAssert.Equal(new MagickColor("#e0d8d9"), color.ToMagickColor());

            color = new MagickColor("#e0d9d8");
            ColorAssert.Equal(new MagickColor("#e0d9d8"), color.ToMagickColor());

            color = new MagickColor("#bbccbb");
#if Q8
            ColorAssert.Equal(new MagickColor("#bacbba"), color.ToMagickColor());
#else
            ColorAssert.Equal(new MagickColor("#bbbacccbbbba"), color.ToMagickColor());
#endif

            color = new MagickColor("#bbaacc");
            ColorAssert.Equal(new MagickColor("#bbaacc"), color.ToMagickColor());
        }
    }
}
