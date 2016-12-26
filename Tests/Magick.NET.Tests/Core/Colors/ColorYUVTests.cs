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
  public class ColorYUVTests : ColorBaseTests<ColorYUV>
  {
    [TestMethod]
    public void Test_GetHashCode()
    {
      ColorYUV first = new ColorYUV(0.0, 0.0, 0.0);
      int hashCode = first.GetHashCode();

      first.Y = 1.0;
      Assert.AreNotEqual(hashCode, first.GetHashCode());
    }

    [TestMethod]
    public void Test_IComparable()
    {
      ColorYUV first = new ColorYUV(0.2, 0.3, 0.4);

      Test_IComparable(first);

      ColorYUV second = new ColorYUV(0.2, 0.4, 0.5);

      Test_IComparable_FirstLower(first, second);

      second = new ColorYUV(0.2, 0.3, 0.4);

      Test_IComparable_Equal(first, second);
    }

    [TestMethod]
    public void Test_IEquatable()
    {
      ColorYUV first = new ColorYUV(0.1, -0.2, -0.3);

      Test_IEquatable_NullAndSelf(first);

      ColorYUV second = new ColorYUV(0.1, -0.2, -0.3);

      Test_IEquatable_Equal(first, second);

      second = new ColorYUV(0.1, -0.2, -0.4);

      Test_IEquatable_NotEqual(first, second);
    }

    [TestMethod]
    public void Test_ImplicitOperator()
    {
      ColorYUV expected = new ColorYUV(0.413189, 0.789, 1.0156);
      ColorYUV actual = MagickColors.Fuchsia;
      Assert.AreEqual(actual, expected);

      Assert.IsNull(ColorYUV.FromMagickColor(null));
    }

    [TestMethod]
    public void Test_ToString()
    {
      ColorYUV color = new ColorYUV(0.413189, 0.789, 1.0156);
      Test_ToString(color, MagickColors.Fuchsia);
    }

    [TestMethod]
    public void Test_Properties()
    {
      ColorYUV color = new ColorYUV(0, 0, 0);

      color.Y = 1;
      Assert.AreEqual(1, color.Y);
      Assert.AreEqual(0, color.U);
      Assert.AreEqual(0, color.V);

      color.U = 2;
      Assert.AreEqual(1, color.Y);
      Assert.AreEqual(2, color.U);
      Assert.AreEqual(0, color.V);

      color.V = 3;
      Assert.AreEqual(1, color.Y);
      Assert.AreEqual(2, color.U);
      Assert.AreEqual(3, color.V);
    }
  }
}
