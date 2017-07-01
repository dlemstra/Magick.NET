//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ColorMonoTests : ColorBaseTests<ColorMono>
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            ColorMono first = new ColorMono(true);
            int hashCode = first.GetHashCode();

            first.IsBlack = false;
            Assert.AreNotEqual(hashCode, first.GetHashCode());
        }

        [TestMethod]
        public void Test_IComparable()
        {
            ColorMono first = new ColorMono(true);

            Test_IComparable(first);

            ColorMono second = new ColorMono(false);

            Test_IComparable_FirstLower(first, second);

            second = new ColorMono(true);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorMono first = new ColorMono(true);

            Test_IEquatable_NullAndSelf(first);

            ColorMono second = new ColorMono(true);

            Test_IEquatable_Equal(first, second);

            second = new ColorMono(false);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorMono expected = new ColorMono(true);
            ColorMono actual = MagickColors.Black;
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual;
            Assert.AreEqual(magickColor, MagickColors.Black);

            Assert.IsNull(ColorMono.FromMagickColor(null));
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorMono color = new ColorMono(true);
            Test_ToString(color, MagickColors.Black);
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorMono color = new ColorMono(true);

            color.IsBlack = false;
            Assert.AreEqual(false, color.IsBlack);
        }

        [TestMethod]
        public void Test_ColorMono()
        {
            ColorMono mono = new ColorMono(false);

            MagickColor white = new MagickColor("#fff");
            Assert.AreEqual(white, mono.ToMagickColor());
            ColorAssert.AreEqual(MagickColors.White, mono.ToMagickColor());

            mono = new ColorMono(true);

            MagickColor black = new MagickColor("#000");
            Assert.AreEqual(black, mono.ToMagickColor());
            ColorAssert.AreEqual(MagickColors.Black, mono.ToMagickColor());

            mono = ColorMono.FromMagickColor(MagickColors.Black);
            Assert.IsTrue(mono.IsBlack);

            mono = ColorMono.FromMagickColor(MagickColors.White);
            Assert.IsFalse(mono.IsBlack);

            ExceptionAssert.ThrowsArgumentException(() =>
            {
                ColorMono.FromMagickColor(MagickColors.Gray);
            }, "color", "Invalid");
        }
    }
}
