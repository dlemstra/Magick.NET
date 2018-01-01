// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    [TestClass]
    public partial class ColorRGBTests : ColorBaseTests<ColorRGB>
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);
            int hashCode = first.GetHashCode();

            first.G = Quantum.Max;
            Assert.AreNotEqual(hashCode, first.GetHashCode());
        }

        [TestMethod]
        public void Test_IComparable()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);

            Test_IComparable(first);

            ColorRGB second = new ColorRGB(MagickColors.White);

            Test_IComparable_FirstLower(first, second);

            second = new ColorRGB(MagickColors.Green);

            Test_IComparable_FirstLower(second, first);

            second = new ColorRGB(MagickColors.Blue);

            Test_IComparable_FirstLower(second, first);

            second = new ColorRGB(MagickColors.Red);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorRGB first = new ColorRGB(MagickColors.Red);

            Test_IEquatable_NullAndSelf(first);

            ColorRGB second = new ColorRGB(Quantum.Max, 0, 0);

            Test_IEquatable_Equal(first, second);

            second = new ColorRGB(MagickColors.Green);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorRGB expected = new ColorRGB(0, Quantum.Max, Quantum.Max);
            ColorRGB actual = MagickColors.Cyan;
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual;
            Assert.AreEqual(magickColor, MagickColors.Cyan);

            Assert.IsNull(ColorRGB.FromMagickColor(null));
            Assert.IsNull((MagickColor)(ColorRGB)null);
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorRGB color = new ColorRGB(0, Quantum.Max, Quantum.Max);
            Test_ToString(color, MagickColors.Cyan);
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorRGB color = new ColorRGB(0, 0, 0);

            color.R = 1;
            Assert.AreEqual(1, color.R);
            Assert.AreEqual(0, color.G);
            Assert.AreEqual(0, color.B);

            color.G = 2;
            Assert.AreEqual(1, color.R);
            Assert.AreEqual(2, color.G);
            Assert.AreEqual(0, color.B);

            color.B = 3;
            Assert.AreEqual(1, color.R);
            Assert.AreEqual(2, color.G);
            Assert.AreEqual(3, color.B);
        }

        [TestMethod]
        public void Test_ComplementaryColor()
        {
            ColorRGB color = MagickColors.Red;
            ColorRGB complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(MagickColors.Aqua, complementary);

            color = MagickColors.Lime;
            complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(MagickColors.Fuchsia, complementary);

            color = MagickColors.Black;
            complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(MagickColors.Black, complementary);

            color = MagickColors.White;
            complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(MagickColors.White, complementary);

            color = new MagickColor("#aabbcc");
            complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(new MagickColor("#ccbbaa"), complementary);

            color = new MagickColor(4, 1, 3);
            complementary = color.ComplementaryColor();
            ColorAssert.AreEqual(new MagickColor(1, 4, 1), complementary);

            color = new MagickColor("#9aa01e");
            complementary = color.ComplementaryColor();
#if Q8
            ColorAssert.AreEqual(new MagickColor("#231ea0"), complementary);
#elif Q16 || Q16HDRI
            ColorAssert.AreEqual(new MagickColor("#24231e1ea0a0"), complementary);
#else
#error Not implemented!
#endif
        }

        [TestMethod]
        public void Test_FuzzyEquals()
        {
            ColorRGB first = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.IsFalse(first.FuzzyEquals(null, (Percentage)0));

            Assert.IsTrue(first.FuzzyEquals(first, (Percentage)0));

            ColorRGB second = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.IsTrue(first.FuzzyEquals(second, (Percentage)0));

            QuantumType half = (QuantumType)(Quantum.Max / 2.0);
            second = new ColorRGB(Quantum.Max, half, Quantum.Max);

            Assert.IsFalse(first.FuzzyEquals(second, (Percentage)0));
            Assert.IsFalse(first.FuzzyEquals(second, (Percentage)10));
            Assert.IsFalse(first.FuzzyEquals(second, (Percentage)20));
            Assert.IsTrue(first.FuzzyEquals(second, (Percentage)30));
        }
    }
}
