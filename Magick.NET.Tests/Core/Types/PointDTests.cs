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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class PointDTests
  {
    private const string _Category = "PointD";

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      PointD point = new PointD();
      Assert.AreEqual(0.0, point.X);
      Assert.AreEqual(0.0, point.Y);

      point = new PointD(5);
      Assert.AreEqual(5.0, point.X);
      Assert.AreEqual(5.0, point.Y);

      point = new PointD(5, 10);
      Assert.AreEqual(5.0, point.X);
      Assert.AreEqual(10.0, point.Y);

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PointD(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PointD("");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PointD("1.0x");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PointD("x1.0");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PointD("ax1.0");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PointD("1.0xb");
      });

      point = new PointD("1.0x2.5");
      Assert.AreEqual(1.0, point.X);
      Assert.AreEqual(2.5, point.Y);
      Assert.AreEqual("1x2.5", point.ToString());
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      PointD first = new PointD(50.0);
      PointD second = new PointD(50);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));
    }
  }
}
