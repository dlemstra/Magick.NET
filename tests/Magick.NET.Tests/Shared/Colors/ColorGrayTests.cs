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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ColorGrayTests : ColorBaseTests<ColorGray>
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            ColorGray first = new ColorGray(0);
            int hashCode = first.GetHashCode();

            first.Shade = 1;
            Assert.AreNotEqual(hashCode, first.GetHashCode());
        }

        [TestMethod]
        public void Test_IComparable()
        {
            ColorGray first = new ColorGray(0.5);

            Test_IComparable(first);

            ColorGray second = new ColorGray(0.6);

            Test_IComparable_FirstLower(first, second);

            second = new ColorGray(0.5);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorGray first = new ColorGray(0.2);

            Test_IEquatable_NullAndSelf(first);

            ColorGray second = new ColorGray(0.2);

            Test_IEquatable_Equal(first, second);

            second = new ColorGray(0.4);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorGray expected = new ColorGray(1.0);
            ColorGray actual = MagickColors.White;
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual.ToMagickColor();
            Assert.AreEqual(magickColor, MagickColors.White);

            Assert.IsNull(ColorGray.FromMagickColor(null));
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorGray color = new ColorGray(1.0);
            Test_ToString(color, MagickColors.White);
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorGray color = new ColorGray(0);

            color.Shade = 1;
            Assert.AreEqual(1, color.Shade);

            color.Shade = -0.99;
            Assert.AreEqual(1, color.Shade);

            color.Shade = 1.01;
            Assert.AreEqual(1, color.Shade);
        }

        [TestMethod]
        public void Test_ColorGray()
        {
            ColorGray gray = new ColorGray(1);

            MagickColor white = new MagickColor("#fff");
            Assert.AreEqual(white, gray.ToMagickColor());
            ColorAssert.AreEqual(MagickColors.White, gray.ToMagickColor());

            gray = new ColorGray(0);

            MagickColor black = new MagickColor("#000");
            Assert.AreEqual(black, gray.ToMagickColor());
            ColorAssert.AreEqual(MagickColors.Black, gray.ToMagickColor());
        }

        [TestMethod]
        public void Test_Constructor()
        {
            ExceptionAssert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(1.01);
            });

            ExceptionAssert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(-0.01);
            });
        }
    }
}
