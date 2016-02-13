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
    private const string _Category = "ColorYUV";

    [TestMethod, TestCategory(_Category)]
    public void Test_IComparable()
    {
      ColorYUV first = new ColorYUV(0.2, 0.3, 0.4);

      Test_IComparable(first);

      ColorYUV second = new ColorYUV(0.2, 0.4, 0.5);

      Test_IComparable_FirstLower(first, second);

      second = new ColorYUV(0.2, 0.3, 0.4);

      Test_IComparable_Equal(first, second);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      ColorYUV first = new ColorYUV(0.1, -0.2, -0.3);

      Test_IEquatable_NullAndSelf(first);

      ColorYUV second = new ColorYUV(0.1, -0.2, -0.3);

      Test_IEquatable_Equal(first, second);

      second = new ColorYUV(0.1, -0.2, -0.4);

      Test_IEquatable_NotEqual(first, second);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new ColorYUV(1.01, -0.5, 0.5);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new ColorYUV(1.0, -0.51, 0.5);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new ColorYUV(1.0, -0.5, 0.51);
      });
    }
  }
}
