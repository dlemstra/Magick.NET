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
  public class DensityTests
  {
    private const string _Category = "Density";

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      Density density = new Density(5);
      Assert.AreEqual(5.0, density.X);
      Assert.AreEqual(5.0, density.Y);
      Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);

      density = new Density(8.5, DensityUnit.PixelsPerCentimeter);
      Assert.AreEqual(8.5, density.X);
      Assert.AreEqual(8.5, density.Y);
      Assert.AreEqual(DensityUnit.PixelsPerCentimeter, density.Units);

      density = new Density(2, 3);
      Assert.AreEqual(2.0, density.X);
      Assert.AreEqual(3.0, density.Y);
      Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);

      density = new Density(2.2, 3.3, DensityUnit.Undefined);
      Assert.AreEqual(2.2, density.X);
      Assert.AreEqual(3.3, density.Y);
      Assert.AreEqual(DensityUnit.Undefined, density.Units);

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new Density(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("1.0x");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("x1.0");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("ax1.0");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("1.0xb");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new Density("1.0x6 magick");
      });

      density = new Density("1.0x2.5");
      Assert.AreEqual(1.0, density.X);
      Assert.AreEqual(2.5, density.Y);
      Assert.AreEqual(DensityUnit.Undefined, density.Units);
      Assert.AreEqual("1x2.5", density.ToString());

      density = new Density("2.5x1.0 cm");
      Assert.AreEqual(2.5, density.X);
      Assert.AreEqual(1.0, density.Y);
      Assert.AreEqual(DensityUnit.PixelsPerCentimeter, density.Units);
      Assert.AreEqual("2.5x1 cm", density.ToString());

      density = new Density("2.5x1.0 inch");
      Assert.AreEqual(2.5, density.X);
      Assert.AreEqual(1.0, density.Y);
      Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);
      Assert.AreEqual("2.5x1 inch", density.ToString());
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      Density first = new Density(50.0);
      Density second = new Density(50);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));

      first = new Density(50.0);
      second = new Density(50, DensityUnit.PixelsPerCentimeter);

      Assert.IsFalse(first == second);
      Assert.IsFalse(first.Equals(second));
      Assert.IsFalse(first.Equals((object)second));

      Assert.IsFalse(first == null);
      Assert.IsFalse(first.Equals(null));
      Assert.IsFalse(first.Equals((object)null));
    }
  }
}
