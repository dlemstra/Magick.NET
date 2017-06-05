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
  public class DrawableDensityTests
  {
    private IMagickImage CreateImage(int? density)
    {
      IMagickImage image = new MagickImage(MagickColors.Purple, 500, 500);
      DrawableFontPointSize pointSize = new DrawableFontPointSize(20);
      DrawableText text = new DrawableText(250, 250, "Magick.NET");

      if (!density.HasValue)
        image.Draw(pointSize, text);
      else
        image.Draw(pointSize, new DrawableDensity(density.Value), text);

      image.Trim();

      return image;
    }

    [TestMethod]
    public void Test_ImageSize()
    {
      using (IMagickImage image = CreateImage(null))
      {
        Assert.AreEqual(107, image.Width);
        Assert.AreEqual(19, image.Height);
      }

      using (IMagickImage image = CreateImage(97))
      {
        Assert.AreEqual(146, image.Width);
        Assert.AreEqual(24, image.Height);
      }
    }

    [TestMethod]
    public void Test_Constructor()
    {
      DrawableDensity density = new DrawableDensity(new PointD(4, 2));
      Assert.AreEqual(4, density.Density.X);
      Assert.AreEqual(2, density.Density.Y);
    }
  }
}
