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

namespace Magick.NET.Tests.Types
{
    [TestClass]
    public class RationalTests
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Rational rational = new Rational(7, 55);
            Assert.AreEqual(7U, rational.Numerator);
            Assert.AreEqual(55U, rational.Denominator);

            rational = new Rational(755, 100);
            Assert.AreEqual(151U, rational.Numerator);
            Assert.AreEqual(20U, rational.Denominator);

            rational = new Rational(755, 100, false);
            Assert.AreEqual(755U, rational.Numerator);
            Assert.AreEqual(100U, rational.Denominator);

            rational = new Rational(-7.55);
            Assert.AreEqual(151U, rational.Numerator);
            Assert.AreEqual(20U, rational.Denominator);

            rational = new Rational(7);
            Assert.AreEqual(7U, rational.Numerator);
            Assert.AreEqual(1U, rational.Denominator);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            Rational first = new Rational(3, 2);

            Assert.IsFalse(first.Equals(null));
            Assert.IsTrue(first.Equals(first));
            Assert.IsTrue(first.Equals((object)first));

            Rational second = new Rational(3, 2);

            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first.Equals((object)second));

            second = new Rational(2, 3);

            Assert.IsTrue(first != second);
            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void Test_Fraction()
        {
            Rational first = new Rational(1.0 / 1600);
            Rational second = new Rational(1.0 / 1600, true);
            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void Test_ToDouble()
        {
            Rational rational = new Rational(0, 0);
            Assert.AreEqual(double.NaN, rational.ToDouble());

            rational = new Rational(2, 0);
            Assert.AreEqual(double.PositiveInfinity, rational.ToDouble());
        }

        [TestMethod]
        public void Test_ToString()
        {
            Rational rational = new Rational(0, 0);
            Assert.AreEqual("Indeterminate", rational.ToString());

            rational = new Rational(double.PositiveInfinity);
            Assert.AreEqual("PositiveInfinity", rational.ToString());

            rational = new Rational(double.NegativeInfinity);
            Assert.AreEqual("PositiveInfinity", rational.ToString());

            rational = new Rational(0, 1);
            Assert.AreEqual("0", rational.ToString());

            rational = new Rational(2, 1);
            Assert.AreEqual("2", rational.ToString());

            rational = new Rational(1, 2);
            Assert.AreEqual("1/2", rational.ToString());
        }
    }
}
