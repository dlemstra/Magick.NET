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
  public partial class ColorRGBTests : ColorBaseTests<ColorRGB>
  {
    [TestMethod]
    public void Test_IComparable()
    {
      ColorRGB first = new ColorRGB(MagickColors.Red);

      Test_IComparable(first);

      ColorRGB second = new ColorRGB(MagickColors.White);

      Test_IComparable_FirstLower(first, second);

      second = new ColorRGB(MagickColors.Green);

      Test_IComparable_FirstLower(second, first);

      second = new ColorRGB(MagickColors.Blue);

      Test_IComparable_FirstLower(second, first);

      second = new ColorRGB(MagickColors.Red);

      Test_IComparable_Equal(first, second);
    }

    [TestMethod]
    public void Test_IEquatable()
    {
      ColorRGB first = new ColorRGB(MagickColors.Red);

      Test_IEquatable_NullAndSelf(first);

      ColorRGB second = new ColorRGB(Quantum.Max, 0, 0);

      Test_IEquatable_Equal(first, second);

      second = new ColorRGB(MagickColors.Green);

      Test_IEquatable_NotEqual(first, second);
    }
  }
}
