//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ColorCMYKTests : ColorBaseTests<ColorCMYK>
    {
        [TestMethod]
        public void Test_GetHashCode()
        {
            ColorCMYK first = new ColorCMYK(0, 0, 0, 0);
            int hashCode = first.GetHashCode();

            first.C = Quantum.Max;
            Assert.AreNotEqual(hashCode, first.GetHashCode());
        }

        [TestMethod]
        public void Test_IComparable()
        {
            ColorCMYK first = new ColorCMYK(0, 0, 0, 0);

            Test_IComparable(first);

            ColorCMYK second = new ColorCMYK(Quantum.Max, 0, 0, 0);

            Test_IComparable_FirstLower(first, second);

            second = new ColorCMYK(0, 0, 0, 0);

            Test_IComparable_Equal(first, second);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ColorCMYK first = new ColorCMYK(0, Quantum.Max, 0, 0);

            Test_IEquatable_NullAndSelf(first);

            ColorCMYK second = new ColorCMYK(0, Quantum.Max, 0, 0);

            Test_IEquatable_Equal(first, second);

            second = new ColorCMYK(0, 0, Quantum.Max, 0);

            Test_IEquatable_NotEqual(first, second);
        }

        [TestMethod]
        public void Test_ImplicitOperator()
        {
            ColorCMYK expected = new ColorCMYK(Quantum.Max, 0, 0, 0);
            ColorCMYK actual = new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max);
            Assert.AreEqual(expected, actual);

            MagickColor magickColor = actual;
            Assert.AreEqual(magickColor, new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max));

            Assert.IsNull(ColorCMYK.FromMagickColor(null));
        }

        [TestMethod]
        public void Test_ToString()
        {
            ColorCMYK color = new ColorCMYK(Quantum.Max, 0, 0, 0);
            Test_ToString(color, new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max));
        }

        [TestMethod]
        public void Test_Properties()
        {
            ColorCMYK color = new ColorCMYK(0, 0, 0, 0);

            color.C = 1;
            Assert.AreEqual(1, color.C);
            Assert.AreEqual(0, color.M);
            Assert.AreEqual(0, color.Y);
            Assert.AreEqual(0, color.K);
            Assert.AreEqual(Quantum.Max, color.A);

            color.M = 2;
            Assert.AreEqual(1, color.C);
            Assert.AreEqual(2, color.M);
            Assert.AreEqual(0, color.Y);
            Assert.AreEqual(0, color.K);
            Assert.AreEqual(Quantum.Max, color.A);

            color.Y = 3;
            Assert.AreEqual(1, color.C);
            Assert.AreEqual(2, color.M);
            Assert.AreEqual(3, color.Y);
            Assert.AreEqual(0, color.K);
            Assert.AreEqual(Quantum.Max, color.A);

            color.K = 4;
            Assert.AreEqual(1, color.C);
            Assert.AreEqual(2, color.M);
            Assert.AreEqual(3, color.Y);
            Assert.AreEqual(4, color.K);
            Assert.AreEqual(Quantum.Max, color.A);

            color.A = 5;
            Assert.AreEqual(1, color.C);
            Assert.AreEqual(2, color.M);
            Assert.AreEqual(3, color.Y);
            Assert.AreEqual(4, color.K);
            Assert.AreEqual(5, color.A);
        }

        [TestMethod]
        public void Test_ColorCMYK()
        {
            ColorCMYK first = new ColorCMYK(0, 0, 0, 0);

            MagickColor second = new MagickColor("cmyk(0,0,0,0)");
            Assert.AreEqual(second, first.ToMagickColor());

            second = new MagickColor("#fff");
            Assert.AreNotEqual(second, first.ToMagickColor());

            second = new MagickColor("white");
            Assert.AreNotEqual(second, first.ToMagickColor());

            first = new ColorCMYK(0, 0, Quantum.Max, 0);

            second = new MagickColor("cmyk(0,0,100%,0)");
            Assert.AreEqual(second, first.ToMagickColor());

            first = new ColorCMYK(0, 0, Quantum.Max, 0, 0);

            second = new MagickColor("cmyka(0,0,100%,0,0)");
            Assert.AreEqual(second, first.ToMagickColor());

            first = new ColorCMYK((Percentage)0, (Percentage)100, (Percentage)0, (Percentage)100);
            Assert.AreEqual(0, first.C);
            Assert.AreEqual(Quantum.Max, first.M);
            Assert.AreEqual(0, first.Y);
            Assert.AreEqual(Quantum.Max, first.K);
            Assert.AreEqual(Quantum.Max, first.A);

            first = new ColorCMYK((Percentage)100, (Percentage)0, (Percentage)100, (Percentage)0, (Percentage)100);
            Assert.AreEqual(Quantum.Max, first.C);
            Assert.AreEqual(0, first.M);
            Assert.AreEqual(Quantum.Max, first.Y);
            Assert.AreEqual(0, first.K);
            Assert.AreEqual(Quantum.Max, first.A);

            first = new ColorCMYK("#0ff0");
            Assert.AreEqual(0, first.C);
            Assert.AreEqual(Quantum.Max, first.M);
            Assert.AreEqual(Quantum.Max, first.Y);
            Assert.AreEqual(0, first.K);
            Assert.AreEqual(Quantum.Max, first.A);

            first = new ColorCMYK("#ff00ff00");
            Assert.AreEqual(Quantum.Max, first.C);
            Assert.AreEqual(0, first.M);
            Assert.AreEqual(Quantum.Max, first.Y);
            Assert.AreEqual(0, first.K);
            Assert.AreEqual(Quantum.Max, first.A);

            first = new ColorCMYK("#0000ffff0000ffff");
            Assert.AreEqual(0, first.C);
            Assert.AreEqual(Quantum.Max, first.M);
            Assert.AreEqual(0, first.Y);
            Assert.AreEqual(Quantum.Max, first.K);
            Assert.AreEqual(Quantum.Max, first.A);

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new ColorCMYK("white");
            });

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new ColorCMYK("#ff00ff");
            });

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new ColorCMYK("#ffff0000fffff");
            });

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new ColorCMYK("#ffff0000fffff0000fffff");
            });

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new ColorCMYK("#fff");
            });
        }
    }
}
