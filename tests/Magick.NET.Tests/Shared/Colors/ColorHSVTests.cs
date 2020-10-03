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
    public class ColorHSVTests : ColorBaseTests<ColorHSV>
    {
        [Fact]
        public void Test_GetHashCode()
        {
            ColorHSV first = new ColorHSV(0.0, 0.0, 0.0);
            int hashCode = first.GetHashCode();

            first.Hue = first.Saturation = first.Value = 1.0;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }

        [Fact]
        public void Test_IComparable()
        {
            ColorHSV first = new ColorHSV(0.4, 0.3, 0.2);

            AssertIComparable(first);

            ColorHSV second = new ColorHSV(0.1, 0.2, 0.3);

            Test_IComparable_FirstLower(first, second);

            second = new ColorHSV(0.4, 0.3, 0.2);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorHSV first = new ColorHSV(1.0, 0.5, 0.5);

            Test_IEquatable_NullAndSelf(first);

            ColorHSV second = new ColorHSV(1.0, 0.5, 0.5);

            Test_IEquatable_Equal(first, second);

            second = new ColorHSV(0.2, 0.5, 0.5);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorHSV expected = new ColorHSV(1.0, 1.0, 1.0);
            ColorHSV actual = MagickColors.Red;
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, MagickColors.Red);

            Assert.Null(ColorHSV.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorHSV color = new ColorHSV(1.0, 1.0, 1.0);
            AssertToString(color, MagickColors.Red);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorHSV color = new ColorHSV(0, 0, 0);

            color.Hue = 1;
            Assert.Equal(1, color.Hue);
            Assert.Equal(0, color.Saturation);
            Assert.Equal(0, color.Value);

            color.Saturation = 2;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(0, color.Value);

            color.Value = 3;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(3, color.Value);
        }

        [Fact]
        public void Test_HueShift()
        {
            ColorHSV color = new ColorHSV(0.3, 0.2, 0.1);

            color.HueShift(720);
            Assert.InRange(color.Hue, 0.29, 0.3);

            color.HueShift(-720);
            Assert.InRange(color.Hue, 0.29, 0.3);
        }
    }
}
