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

namespace Magick.NET.Tests
{
    [TestClass]
    public class ColorHSVTests : ColorBaseTests<ColorHSV>
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            ColorHSV first = new ColorHSV(0.0, 0.0, 0.0);
            int hashCode = first.GetHashCode();

            first.Hue = first.Saturation = first.Value = 1.0;
            Assert.AreNotEqual(hashCode, first.GetHashCode());
        }

        [TestMethod]
        public void Test_IComparable()
        {
            ColorHSV first = new ColorHSV(0.4, 0.3, 0.2);

            Test_IComparable(first);

            ColorHSV second = new ColorHSV(0.1, 0.2, 0.3);

            Test_IComparable_FirstLower(first, second);

            second = new ColorHSV(0.4, 0.3, 0.2);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorHSV first = new ColorHSV(1.0, 0.5, 0.5);

            Test_IEquatable_NullAndSelf(first);

            ColorHSV second = new ColorHSV(1.0, 0.5, 0.5);

            Test_IEquatable_Equal(first, second);

            second = new ColorHSV(0.2, 0.5, 0.5);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorHSV expected = new ColorHSV(1.0, 1.0, 1.0);
            ColorHSV actual = MagickColors.Red;
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual;
            Assert.AreEqual(magickColor, MagickColors.Red);

            Assert.IsNull(ColorHSV.FromMagickColor(null));
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorHSV color = new ColorHSV(1.0, 1.0, 1.0);
            Test_ToString(color, MagickColors.Red);
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorHSV color = new ColorHSV(0, 0, 0);

            color.Hue = 1;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(0, color.Saturation);
            Assert.AreEqual(0, color.Value);

            color.Saturation = 2;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(2, color.Saturation);
            Assert.AreEqual(0, color.Value);

            color.Value = 3;
            Assert.AreEqual(1, color.Hue);
            Assert.AreEqual(2, color.Saturation);
            Assert.AreEqual(3, color.Value);
        }

        [TestMethod]
        public void Test_HueShift()
        {
            ColorHSV color = new ColorHSV(0.3, 0.2, 0.1);

            color.HueShift(720);
            Assert.AreEqual(0.3, color.Hue, 0.00001);

            color.HueShift(-720);
            Assert.AreEqual(0.3, color.Hue, 0.00001);
        }
    }
}
