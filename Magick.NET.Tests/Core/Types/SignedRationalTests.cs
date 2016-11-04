//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace Magick.NET.Tests
{
  [TestClass]
  public class SignedRationalTests
  {
    private const string _Category = "SignedRational";

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      SignedRational rational = new SignedRational(7, -55);
      Assert.AreEqual(7, rational.Numerator);
      Assert.AreEqual(-55, rational.Denominator);

      rational = new SignedRational(-755, 100);
      Assert.AreEqual(-151, rational.Numerator);
      Assert.AreEqual(20, rational.Denominator);

      rational = new SignedRational(-755, -100, false);
      Assert.AreEqual(-755, rational.Numerator);
      Assert.AreEqual(-100, rational.Denominator);

      rational = new SignedRational(-151, -20);
      Assert.AreEqual(-151, rational.Numerator);
      Assert.AreEqual(-20, rational.Denominator);

      rational = new SignedRational(-7.55);
      Assert.AreEqual(-151, rational.Numerator);
      Assert.AreEqual(20, rational.Denominator);

      rational = new SignedRational(7);
      Assert.AreEqual(7, rational.Numerator);
      Assert.AreEqual(1, rational.Denominator);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      SignedRational first = new SignedRational(3, 2);

      Assert.IsFalse(first.Equals(null));
      Assert.IsTrue(first.Equals(first));
      Assert.IsTrue(first.Equals((object)first));

      SignedRational second = new SignedRational(3, 2);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));

      second = new SignedRational(2, 3);

      Assert.IsTrue(first != second);
      Assert.IsFalse(first.Equals(second));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Fraction()
    {
      SignedRational first = new SignedRational(1.0 / 1600);
      SignedRational second = new SignedRational(1.0 / 1600, true);
      Assert.IsFalse(first.Equals(second));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToDouble()
    {
      SignedRational rational = new SignedRational(0, 0);
      Assert.AreEqual(double.NaN, rational.ToDouble());

      rational = new SignedRational(2, 0);
      Assert.AreEqual(double.PositiveInfinity, rational.ToDouble());

      rational = new SignedRational(-2, 0);
      Assert.AreEqual(double.NegativeInfinity, rational.ToDouble());
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToString()
    {
      SignedRational rational = new SignedRational(0, 0);
      Assert.AreEqual("Indeterminate", rational.ToString());

      rational = new SignedRational(double.PositiveInfinity);
      Assert.AreEqual("PositiveInfinity", rational.ToString());

      rational = new SignedRational(double.NegativeInfinity);
      Assert.AreEqual("NegativeInfinity", rational.ToString());

      rational = new SignedRational(0, 1);
      Assert.AreEqual("0", rational.ToString());

      rational = new SignedRational(2, 1);
      Assert.AreEqual("2", rational.ToString());

      rational = new SignedRational(1, 2);
      Assert.AreEqual("1/2", rational.ToString());
    }
  }
}
