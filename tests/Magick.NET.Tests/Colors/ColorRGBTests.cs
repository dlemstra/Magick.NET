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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class ColorRGBTests : ColorBaseTests<ColorRGB>
    {
        [Fact]
        public void Test_GetHashCode()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);
            int hashCode = first.GetHashCode();

            first.G = Quantum.Max;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }

        [Fact]
        public void Test_IComparable()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);

            AssertIComparable(first);

            ColorRGB second = new ColorRGB(MagickColors.White);

            Test_IComparable_FirstLower(first, second);

            second = new ColorRGB(MagickColors.Green);

            Test_IComparable_FirstLower(second, first);

            second = new ColorRGB(MagickColors.Blue);

            Test_IComparable_FirstLower(second, first);

            second = new ColorRGB(MagickColors.Red);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);

            Test_IEquatable_NullAndSelf(first);

            ColorRGB second = new ColorRGB(Quantum.Max, 0, 0);

            Test_IEquatable_Equal(first, second);

            second = new ColorRGB(MagickColors.Green);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorRGB expected = new ColorRGB(0, Quantum.Max, Quantum.Max);
            ColorRGB actual = MagickColors.Cyan;
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, MagickColors.Cyan);

            Assert.Null(ColorRGB.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorRGB color = new ColorRGB(0, Quantum.Max, Quantum.Max);
            AssertToString(color, MagickColors.Cyan);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorRGB color = new ColorRGB(0, 0, 0);

            color.R = 1;
            Assert.Equal(1, color.R);
            Assert.Equal(0, color.G);
            Assert.Equal(0, color.B);

            color.G = 2;
            Assert.Equal(1, color.R);
            Assert.Equal(2, color.G);
            Assert.Equal(0, color.B);

            color.B = 3;
            Assert.Equal(1, color.R);
            Assert.Equal(2, color.G);
            Assert.Equal(3, color.B);
        }

        [Fact]
        public void Test_ComplementaryColor()
        {
            ColorRGB color = MagickColors.Red;
            ColorRGB complementary = color.ComplementaryColor();
            ColorAssert.Equal(MagickColors.Aqua, complementary.ToMagickColor());

            color = MagickColors.Lime;
            complementary = color.ComplementaryColor();
            ColorAssert.Equal(MagickColors.Fuchsia, complementary.ToMagickColor());

            color = MagickColors.Black;
            complementary = color.ComplementaryColor();
            ColorAssert.Equal(MagickColors.Black, complementary.ToMagickColor());

            color = MagickColors.White;
            complementary = color.ComplementaryColor();
            ColorAssert.Equal(MagickColors.White, complementary.ToMagickColor());

            color = new MagickColor("#aabbcc");
            complementary = color.ComplementaryColor();
            ColorAssert.Equal(new MagickColor("#ccbbaa"), complementary.ToMagickColor());

            color = new MagickColor(4, 1, 3);
            complementary = color.ComplementaryColor();
            ColorAssert.Equal(new MagickColor(1, 4, 1), complementary.ToMagickColor());

            color = new MagickColor("#9aa01e");
            complementary = color.ComplementaryColor();
#if Q8
            ColorAssert.Equal(new MagickColor("#231ea0"), complementary.ToMagickColor());
#else
            ColorAssert.Equal(new MagickColor("#24231e1ea0a0"), complementary.ToMagickColor());
#endif
        }

        [Fact]
        public void Test_FuzzyEquals()
        {
            ColorRGB first = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.False(first.FuzzyEquals(null, (Percentage)0));

            Assert.True(first.FuzzyEquals(first, (Percentage)0));

            ColorRGB second = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.True(first.FuzzyEquals(second, (Percentage)0));

            QuantumType half = (QuantumType)(Quantum.Max / 2.0);
            second = new ColorRGB(Quantum.Max, half, Quantum.Max);

            Assert.False(first.FuzzyEquals(second, (Percentage)0));
            Assert.False(first.FuzzyEquals(second, (Percentage)10));
            Assert.False(first.FuzzyEquals(second, (Percentage)20));
            Assert.True(first.FuzzyEquals(second, (Percentage)30));
        }
    }
}
