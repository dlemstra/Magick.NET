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
  //==============================================================================================
  [TestClass]
  public class PerceptualHashTests
  {
    private const string _Category = "PerceptualHash";

    private void TestChannel(ChannelPerceptualHash channel, int index, double srgbHuPhash, double hclpHuPhash)
    {
      Assert.AreEqual(srgbHuPhash, channel.SrgbHuPhash(index), 0.0001);
      Assert.AreEqual(hclpHuPhash, channel.HclpHuPhash(index), 0.0001);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Channel()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        PerceptualHash phash = image.PerceptualHash();
        ChannelPerceptualHash channel = phash.GetChannel(PixelChannel.Red);

        ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
        {
          channel.HclpHuPhash(7);
        });

        ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
        {
          channel.SrgbHuPhash(7);
        });

#if Q8
        TestChannel(channel, 0, 0.6980, 0.0959);
        TestChannel(channel, 1, 3.4390, 0.6548);
        TestChannel(channel, 2, 3.9123, 0.9158);
        TestChannel(channel, 3, 4.2921, 2.3586);
        TestChannel(channel, 4, 8.7567, 4.8847);
        TestChannel(channel, 5, 8.2505, 2.8316);
        TestChannel(channel, 6, 8.4397, 3.9994);
#elif Q16
        TestChannel(channel, 0, 0.6979, 0.2578);
        TestChannel(channel, 1, 3.4385, 1.0557);
        TestChannel(channel, 2, 3.9123, 1.3807);
        TestChannel(channel, 3, 4.2920, 2.8467);
        TestChannel(channel, 4, 8.7555, 5.2023);
        TestChannel(channel, 5, 8.2995, 3.5194);
        TestChannel(channel, 6, 8.4397, 5.0468);
#elif Q16HDRI
        TestChannel(channel, 0, 0.6979, 0.2927);
        TestChannel(channel, 1, 3.4385, 1.1754);
        TestChannel(channel, 2, 3.9123, 1.4998);
        TestChannel(channel, 3, 4.2920, 3.0736);
        TestChannel(channel, 4, 8.7556, 5.4465);
        TestChannel(channel, 5, 8.2997, 3.7136);
        TestChannel(channel, 6, 8.4398, 5.6025);
#else
#error Not implemented!
#endif

        channel = phash.GetChannel(PixelChannel.Green);
#if Q8
        TestChannel(channel, 0, 0.6942, -0.0601);
        TestChannel(channel, 1, 3.3992,  0.3088);
        TestChannel(channel, 2, 4.1171,  0.6081);
        TestChannel(channel, 3, 4.4847,  0.7557);
        TestChannel(channel, 4, 8.8179,  1.7217);
        TestChannel(channel, 5, 6.4828,  0.9410);
        TestChannel(channel, 6, 9.2143,  1.5061);
#elif Q16
        TestChannel(channel, 0, 0.6942, -0.0601);
        TestChannel(channel, 1, 3.3988,  0.3089);
        TestChannel(channel, 2, 4.1168,  0.6083);
        TestChannel(channel, 3, 4.4844,  0.7555);
        TestChannel(channel, 4, 8.8174,  1.7219);
        TestChannel(channel, 5, 6.4821,  0.9408);
        TestChannel(channel, 6, 9.2147,  1.5058);
#elif Q16HDRI
        TestChannel(channel, 0, 0.6942, -0.0601);
        TestChannel(channel, 1, 3.3988,  0.3089);
        TestChannel(channel, 2, 4.1168,  0.6083);
        TestChannel(channel, 3, 4.4844,  0.7555);
        TestChannel(channel, 4, 8.8174,  1.7219);
        TestChannel(channel, 5, 6.4821,  0.9408);
        TestChannel(channel, 6, 9.2148,  1.5058);
#else
#error Not implemented!
#endif

        channel = phash.GetChannel(PixelChannel.Blue);
#if Q8
        TestChannel(channel, 0,  0.7223, 0.6984);
        TestChannel(channel, 1,  3.8298, 3.4611);
        TestChannel(channel, 2,  5.1307, 4.1312);
        TestChannel(channel, 3,  5.0216, 4.4867);
        TestChannel(channel, 4, 10.4775, 8.8669);
        TestChannel(channel, 5,  6.9452, 6.6106);
        TestChannel(channel, 6, 10.1394, 9.0727);
#elif Q16
        TestChannel(channel, 0,  0.7222, 0.6984);
        TestChannel(channel, 1,  3.8295, 3.4608);
        TestChannel(channel, 2,  5.1309, 4.1314);
        TestChannel(channel, 3,  5.0213, 4.4866);
        TestChannel(channel, 4, 10.4778, 8.8663);
        TestChannel(channel, 5,  6.9448, 6.6093);
        TestChannel(channel, 6, 10.1388, 9.0737);
#elif Q16HDRI
        TestChannel(channel, 0,  0.7222, 0.6984);
        TestChannel(channel, 1,  3.8295, 3.4609);
        TestChannel(channel, 2,  5.1309, 4.1314);
        TestChannel(channel, 3,  5.0213, 4.4866);
        TestChannel(channel, 4, 10.4778, 8.8663);
        TestChannel(channel, 5,  6.9448, 6.6094);
        TestChannel(channel, 6, 10.1388, 9.0737);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new PerceptualHash(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PerceptualHash("");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PerceptualHash("a0df");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new PerceptualHash("H00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
      });
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToString()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        PerceptualHash phash = image.PerceptualHash();
        string hash = phash.ToString();
        Assert.AreEqual(210, hash.Length);
#if Q8
        Assert.AreEqual("81b4488655898d38a7a9622356203b620f8a257faffcd823c685c228bed086e9c89c3b81b1f884c98a0d38af2f622728fd3d623fedeb01a78a7aed9381d8684342824c283ad681c378959a8c86b8c429628ee61b216279b81b49887338a1608af44622a3619d362371", hash);
#elif Q16
        Assert.AreEqual("81b4488651898d38a7a8622346206c620f8a64ba8293d835f086f338cb378897b8c52581b1e884c58a0d18af2d622718fd35623ffdeaeda78b3aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
#elif Q16HDRI
        Assert.AreEqual("81b4488651898d48a7a8622346206c620f8a725f82deb83a96878108d4c2891118dad981b1e884c58a0d18af2d622718fd35623ffdeaeda78b4aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
#else
#error Not implemented!
#endif
        PerceptualHash clone = new PerceptualHash(hash);
        Assert.AreEqual(0.0, phash.SumSquaredDistance(clone), 0.001);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_SumSquaredDistance()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        PerceptualHash phash = image.PerceptualHash();

        using (MagickImage other = new MagickImage(Files.MagickNETIconPNG))
        {
          other.HasAlpha = false;

          PerceptualHash otherPhash = other.PerceptualHash();
#if Q8
          Assert.AreEqual(312.06, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16
          Assert.AreEqual(311.40, phash.SumSquaredDistance(otherPhash), 0.02);
#elif Q16HDRI
          Assert.AreEqual(311.27, phash.SumSquaredDistance(otherPhash), 0.02);
#else
#error Not implemented!
#endif
        }
      }
    }
  }
}
