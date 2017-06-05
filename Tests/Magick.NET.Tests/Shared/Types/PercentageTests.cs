//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
  public class PercentageTests
  {
    [TestMethod]
    public void Test_Constructor()
    {
      Percentage percentage = new Percentage();
      Assert.AreEqual("0%", percentage.ToString());

      percentage = new Percentage(50);
      Assert.AreEqual("50%", percentage.ToString());

      percentage = new Percentage(200.0);
      Assert.AreEqual("200%", percentage.ToString());

      percentage = new Percentage(-25);
      Assert.AreEqual("-25%", percentage.ToString());
    }

    [TestMethod]
    public void Test_IComparable()
    {
      Percentage first = new Percentage(100);

      Assert.AreEqual(0, first.CompareTo(first));

      Percentage second = new Percentage(100);

      Assert.AreEqual(0, first.CompareTo(second));
      Assert.IsTrue(first == second);
      Assert.IsFalse(first < second);
      Assert.IsTrue(first <= second);
      Assert.IsFalse(first > second);
      Assert.IsTrue(first >= second);

      second = new Percentage(101);

      Assert.AreEqual(-1, first.CompareTo(second));
      Assert.IsFalse(first == second);
      Assert.IsTrue(first < second);
      Assert.IsTrue(first <= second);
      Assert.IsFalse(first > second);
      Assert.IsFalse(first >= second);

      second = new Percentage(50);

      Assert.AreEqual(1, first.CompareTo(second));
      Assert.IsFalse(first == second);
      Assert.IsFalse(first < second);
      Assert.IsFalse(first <= second);
      Assert.IsTrue(first > second);
      Assert.IsTrue(first >= second);
    }

    [TestMethod]
    public void Test_IEquatable()
    {
      Percentage first = new Percentage(50.0);
      Percentage second = new Percentage(50);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));
    }

    [TestMethod]
    public void Test_Multiplication()
    {
      Percentage percentage = new Percentage();
      Assert.AreEqual(0, 10 * percentage);

      percentage = new Percentage(50);
      Assert.AreEqual(5, 10 * percentage);

      percentage = new Percentage(200);
      Assert.AreEqual(20.0, 10.0 * percentage);

      percentage = new Percentage(25);
      Assert.AreEqual(2.5, 10.0 * percentage);
    }
  }
}
