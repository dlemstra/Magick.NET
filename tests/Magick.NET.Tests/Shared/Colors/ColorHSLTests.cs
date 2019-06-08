// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class ColorHSLTests : ColorBaseTests<ColorHSL>
    {
        [TestMethod]
        public void Test_IComparable()
        {
            ColorHSL first = new ColorHSL(0.4, 0.3, 0.2);

            Test_IComparable(first);

            ColorHSL second = new ColorHSL(0.1, 0.2, 0.3);

            Test_IComparable_FirstLower(first, second);

            second = new ColorHSL(0.4, 0.3, 0.2);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorHSL first = new ColorHSL(1.0, 0.5, 0.5);

            Test_IEquatable_NullAndSelf(first);

            ColorHSL second = new ColorHSL(1.0, 0.5, 0.5);

            Test_IEquatable_Equal(first, second);

            second = new ColorHSL(0.2, 0.5, 1.0);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorHSL expected = new ColorHSL(1.0, 1.0, 1.0);
            ColorHSL actual = MagickColors.White;
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual;
            Assert.AreEqual(magickColor, MagickColors.White);

            Assert.IsNull(ColorHSL.FromMagickColor(null));
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorHSL color = new ColorHSL(1.0, 1.0, 1.0);
            Test_ToString(color, MagickColors.White);
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorHSL color = new ColorHSL(0, 0, 0);

            color.Hue = 1;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(0, color.Saturation);
            Assert.AreEqual(0, color.Lightness);

            color.Saturation = 2;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(2, color.Saturation);
            Assert.AreEqual(0, color.Lightness);

            color.Lightness = 3;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(2, color.Saturation);
            Assert.AreEqual(3, color.Lightness);
        }

        [TestMethod]
        public void Test_ColorHSL()
        {
            ColorHSL color = new MagickColor("#010203");
            ColorAssert.AreEqual(new MagickColor("#010203"), color);

            color = new MagickColor("#aabbcc");
            ColorAssert.AreEqual(new MagickColor("#aabbcc"), color);

            color = new MagickColor("#e0d8d9");
            ColorAssert.AreEqual(new MagickColor("#e0d8d9"), color);

            color = new MagickColor("#e0d9d8");
            ColorAssert.AreEqual(new MagickColor("#e0d9d8"), color);

            color = new MagickColor("#bbccbb");
#if Q8
            ColorAssert.AreEqual(new MagickColor("#bacbba"), color);
#elif Q16 || Q16HDRI
            ColorAssert.AreEqual(new MagickColor("#bbbacccbbbba"), color);
#else
#error Not implemented!
#endif

            color = new MagickColor("#bbaacc");
            ColorAssert.AreEqual(new MagickColor("#bbaacc"), color);
        }
    }
}
