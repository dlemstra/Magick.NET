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
  public class PerceptualHashTests
  {
    private const string _Category = "PerceptualHash";

    private void TestChannel(ChannelPerceptualHash channel, int index, double srgbHuPhashWithOpenCL, double srgbHuPhashWithoutOpenCL, double hclpHuPhashWithOpenCL, double hclpHuPhashWithoutOpenCL)
    {
      OpenCLValue.Assert(srgbHuPhashWithOpenCL, srgbHuPhashWithoutOpenCL, channel.SrgbHuPhash(index), 0.0001);
      OpenCLValue.Assert(hclpHuPhashWithOpenCL, hclpHuPhashWithoutOpenCL, channel.HclpHuPhash(index), 0.0001);
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
        TestChannel(channel, 0, 0.6980, 0.6980, 0.0959, 0.0993);
        TestChannel(channel, 1, 3.4390, 3.4390, 0.6548, 0.6673);
        TestChannel(channel, 2, 3.9123, 3.9123, 0.9158, 0.9181);
        TestChannel(channel, 3, 4.2921, 4.2920, 2.3586, 2.3443);
        TestChannel(channel, 4, 8.7567, 8.7573, 4.8847, 4.9037);
        TestChannel(channel, 5, 8.2505, 8.2147, 2.8316, 2.8253);
        TestChannel(channel, 6, 8.4397, 8.4394, 3.9994, 3.9786);
#elif Q16
        TestChannel(channel, 0, 0.6979, 0.6979, 0.2578, 0.2574);
        TestChannel(channel, 1, 3.4385, 3.4385, 1.0557, 1.0505);
        TestChannel(channel, 2, 3.9123, 3.9123, 1.3807, 1.3839);
        TestChannel(channel, 3, 4.2920, 4.2920, 2.8467, 2.8534);
        TestChannel(channel, 4, 8.7555, 8.7555, 5.2023, 5.1805);
        TestChannel(channel, 5, 8.2995, 8.2995, 3.5194, 3.5081);
        TestChannel(channel, 6, 8.4397, 8.4397, 5.0468, 5.0770);
#elif Q16HDRI
        TestChannel(channel, 0, 0.6979, 0.6979, 0.2927, 0.2932);
        TestChannel(channel, 1, 3.4385, 3.4385, 1.1754, 1.1742);
        TestChannel(channel, 2, 3.9123, 3.9123, 1.4998, 1.5044);
        TestChannel(channel, 3, 4.2920, 4.2920, 3.0736, 3.0744);
        TestChannel(channel, 4, 8.7556, 8.7555, 5.4465, 5.4449);
        TestChannel(channel, 5, 8.2997, 8.2994, 3.7136, 3.7108);
        TestChannel(channel, 6, 8.4398, 8.4397, 5.6025, 5.6170);
#else
#error Not implemented!
#endif

        channel = phash.GetChannel(PixelChannel.Green);
#if Q8
        TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
        TestChannel(channel, 1, 3.3992, 3.3994,  0.3088,  0.3090);
        TestChannel(channel, 2, 4.1171, 4.1171,  0.6081,  0.6081);
        TestChannel(channel, 3, 4.4847, 4.4847,  0.7557,  0.7561);
        TestChannel(channel, 4, 8.8179, 8.8181,  1.7217,  1.7225);
        TestChannel(channel, 5, 6.4828, 6.4830,  0.9410,  0.9414);
        TestChannel(channel, 6, 9.2143, 9.2141,  1.5061,  1.5066);
#elif Q16
        TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
        TestChannel(channel, 1, 3.3988, 3.3988, 0.3089, 0.3089);
        TestChannel(channel, 2, 4.1168, 4.1168, 0.6083, 0.6083);
        TestChannel(channel, 3, 4.4844, 4.4844, 0.7555, 0.7555);
        TestChannel(channel, 4, 8.8174, 8.8174, 1.7219, 1.7219);
        TestChannel(channel, 5, 6.4821, 6.4821, 0.9408, 0.9408);
        TestChannel(channel, 6, 9.2147, 9.2147, 1.5058, 1.5058);
#elif Q16HDRI
        TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
        TestChannel(channel, 1, 3.3988, 3.3988,  0.3089,  0.3089);
        TestChannel(channel, 2, 4.1168, 4.1168,  0.6083,  0.6083);
        TestChannel(channel, 3, 4.4844, 4.4844,  0.7555,  0.7555);
        TestChannel(channel, 4, 8.8174, 8.8174,  1.7219,  1.7219);
        TestChannel(channel, 5, 6.4821, 6.4821,  0.9408,  0.9408);
        TestChannel(channel, 6, 9.2148, 9.2147,  1.5058,  1.5058);
#else
#error Not implemented!
#endif

        channel = phash.GetChannel(PixelChannel.Blue);
#if Q8
        TestChannel(channel, 0,  0.7223,  0.7223, 0.6984, 0.6984);
        TestChannel(channel, 1,  3.8298,  3.8298, 3.4611, 3.4611);
        TestChannel(channel, 2,  5.1307,  5.1306, 4.1312, 4.1311);
        TestChannel(channel, 3,  5.0216,  5.0218, 4.4867, 4.4867);
        TestChannel(channel, 4, 10.4775, 10.4768, 8.8669, 8.8668);
        TestChannel(channel, 5,  6.9452,  6.9454, 6.6106, 6.6108);
        TestChannel(channel, 6, 10.1394, 10.1398, 9.0727, 9.0722);
#elif Q16
        TestChannel(channel, 0, 0.7222, 0.7222, 0.6984, 0.6984);
        TestChannel(channel, 1, 3.8295, 3.8295, 3.4608, 3.4608);
        TestChannel(channel, 2, 5.1309, 5.1309, 4.1314, 4.1314);
        TestChannel(channel, 3, 5.0213, 5.0213, 4.4866, 4.4866);
        TestChannel(channel, 4, 10.4778, 10.4778, 8.8663, 8.8663);
        TestChannel(channel, 5, 6.9448, 6.9448, 6.6093, 6.6093);
        TestChannel(channel, 6, 10.1388, 10.1388, 9.0737, 9.0737);
#elif Q16HDRI
        TestChannel(channel, 0,  0.7222,  0.7222, 0.6984, 0.6984);
        TestChannel(channel, 1,  3.8295,  3.8295, 3.4609, 3.4608);
        TestChannel(channel, 2,  5.1309,  5.1309, 4.1314, 4.1314);
        TestChannel(channel, 3,  5.0213,  5.0213, 4.4866, 4.4866);
        TestChannel(channel, 4, 10.4778, 10.4778, 8.8663, 8.8663);
        TestChannel(channel, 5,  6.9448,  6.9448, 6.6094, 6.6093);
        TestChannel(channel, 6, 10.1388, 10.1388, 9.0737, 9.0737);
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
        OpenCLValue.Assert("81b4488655898d38a7a9622356203b620f8a257faffcd823c685c228bed086e9c89c3b81b1f884c98a0d38af2f622728fd3d623fedeb01a78a7aed9381d8684342824c283ad681c378959a8c86b8c429628ee61b216279b81b49887338a1608af44622a3619d362371",
                           "81b4488656898d38a7a96223562017620f7a26cf81a12823dd85b948bf8d86e5d89b6b81b1f884cb8a0d38af2f622728fd3f623fedeac7a78beaed8d81d8984349824c783ada81c378959b8c86a8c42b628ed61b216279c81b49887348a15f8af43622a3619d362370", hash);
#elif Q16
        OpenCLValue.Assert("81b4488651898d38a7a8622346206c620f8a64ba8293d835f086f338cb378897b8c52581b1e884c58a0d18af2d622718fd35623ffdeaeda78b3aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372",
                           "81b4488651898d38a7a8622346206c620f8a648f8290a8361086f778ca5d889098c65381b1e884c58a0d18af2d622718fd35623ffdeaeda78b3aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
#elif Q16HDRI
        OpenCLValue.Assert("81b4488651898d48a7a8622346206c620f8a725f82deb83a96878108d4c2891118dad981b1e884c58a0d18af2d622718fd35623ffdeaeda78b4aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372",
                           "81b4488651898d38a7a8622346206b620f8a728e82dde83ac4878188d4b2890f58db6b81b1e884c58a0d18af2d622718fd35623ffdeaeda78b4aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
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
          Assert.AreEqual(3, other.ChannelCount);

          PerceptualHash otherPhash = other.PerceptualHash();
#if Q8
          OpenCLValue.Assert(312.06, 311.71, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16
          OpenCLValue.Assert(311.40, 311.45, phash.SumSquaredDistance(otherPhash), 0.02);
#elif Q16HDRI
          OpenCLValue.Assert(311.27, 311.27, phash.SumSquaredDistance(otherPhash), 0.02);
#else
#error Not implemented!
#endif
        }
      }
    }
  }
}
