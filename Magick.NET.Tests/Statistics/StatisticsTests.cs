//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
  public class StatisticsTests
  {
    private const string _Category = "Statistics";

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      MagickImage image = new MagickImage(Files.SnakewarePNG);

      Statistics first = image.Statistics();

      Assert.IsFalse(first == null);
      Assert.IsFalse(first.Equals(null));
      Assert.IsTrue(first.Equals(first));
      Assert.IsTrue(first.Equals((object)first));

      Statistics second = image.Statistics();

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));

      image = new MagickImage(Files.MagickNETIconPNG);

      second = image.Statistics();

      Assert.IsTrue(first != second);
      Assert.IsFalse(first.Equals(second));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Statistics()
    {
      MagickImage image = new MagickImage(Files.SnakewarePNG);

      Statistics statistics = image.Statistics();

      ChannelStatistics red = statistics.GetChannel(PixelChannel.Red);

      Assert.IsNotNull(red);
#if Q8
      Assert.AreEqual(7, red.Depth);
      Assert.AreEqual(0.98, red.Entropy, 0.01);
      Assert.AreEqual(-1.89, red.Kurtosis, 0.01);
      Assert.AreEqual(2, red.Maximum);
      Assert.AreEqual(0.83, red.Mean, 0.01);
      Assert.AreEqual(0, red.Minimum);
      Assert.AreEqual(0.32, red.Skewness, 0.01);
      Assert.AreEqual(0.98, red.StandardDeviation, 0.01);
      Assert.AreEqual(0.83, red.Sum, 0.01);
      Assert.AreEqual(3.35, red.SumCubed, 0.01);
      Assert.AreEqual(6.71, red.SumFourthPower, 0.01);
      Assert.AreEqual(1.67, red.SumSquared, 0.01);
#elif Q16 || Q16HDRI
      Assert.AreEqual(8, red.Depth);
      Assert.AreEqual(0.98, red.Entropy, 0.01);
      Assert.AreEqual(-1.89, red.Kurtosis, 0.01);
      Assert.AreEqual(514, red.Maximum);
      Assert.AreEqual(215.79, red.Mean, 0.01);
      Assert.AreEqual(0, red.Minimum);
      Assert.AreEqual(0.32, red.Skewness, 0.01);
      Assert.AreEqual(253.67, red.StandardDeviation, 0.01);
      Assert.AreEqual(215.79, red.Sum, 0.01);
      Assert.AreEqual(57013088.69, red.SumCubed, 0.01);
      Assert.AreEqual(29304727586.71, red.SumFourthPower, 0.01);
      Assert.AreEqual(110920.40, red.SumSquared, 0.01);
#else
#error Not implemented!
#endif

      Assert.IsNotNull(statistics.Composite());
      Assert.IsNotNull(statistics.GetChannel(PixelChannel.Alpha));

      Assert.IsNull(statistics.GetChannel(PixelChannel.Green));
      Assert.IsNull(statistics.GetChannel(PixelChannel.Blue));
      Assert.IsNull(statistics.GetChannel(PixelChannel.Black));
    }
  }
}
