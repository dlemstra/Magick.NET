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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class PerceptualHashTests
	{
		//===========================================================================================
		private const string _Category = "PerceptualHash";
		//===========================================================================================
		private void TestChannel(ChannelPerceptualHash channel, int index, double srgbHuPhash, double hclpHuPhash)
		{
			Assert.AreEqual(srgbHuPhash, channel.SrgbHuPhash(index), 0.0001);
			Assert.AreEqual(hclpHuPhash, channel.HclpHuPhash(index), 0.0001);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Channel()
		{
#if NET20
			Assert.Inconclusive("VS2008 compiler produces different results, no clue why this is happening");
#endif

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();
				ChannelPerceptualHash channel = phash.GetChannel(PixelChannel.Red);

				ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate()
				{
					channel.HclpHuPhash(7);
				});

				ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate()
				{
					channel.SrgbHuPhash(7);
				});

#if Q8
				TestChannel(channel, 0, 0.6980, 0.0993);
				TestChannel(channel, 1, 3.4390, 0.6673);
				TestChannel(channel, 2, 3.9123, 0.9181);
				TestChannel(channel, 3, 4.2920, 2.3443);
				TestChannel(channel, 4, 8.7573, 4.9037);
				TestChannel(channel, 5, 8.2147, 2.8253);
				TestChannel(channel, 6, 8.4394, 3.9786);
#elif Q16
				TestChannel(channel, 0, 0.6979, 0.2574);
				TestChannel(channel, 1, 3.4385, 1.0505);
				TestChannel(channel, 2, 3.9123, 1.3839);
				TestChannel(channel, 3, 4.2920, 2.8534);
				TestChannel(channel, 4, 8.7555, 5.1805);
				TestChannel(channel, 5, 8.2995, 3.5081);
				TestChannel(channel, 6, 8.4397, 5.0770);
#elif Q16HDRI
				TestChannel(channel, 0, 0.6979, 0.2932);
				TestChannel(channel, 1, 3.4385, 1.1742);
				TestChannel(channel, 2, 3.9123, 1.5044);
				TestChannel(channel, 3, 4.2920, 3.0744);
				TestChannel(channel, 4, 8.7555, 5.4449);
				TestChannel(channel, 5, 8.2994, 3.7108);
				TestChannel(channel, 6, 8.4397, 5.6170);
#else
#error Not implemented!
#endif

				channel = phash.GetChannel(PixelChannel.Green);
#if Q8
				TestChannel(channel, 0, 0.6942, -0.06010);
				TestChannel(channel, 1, 3.3994, 0.30909);
				TestChannel(channel, 2, 4.1171, 0.60813);
				TestChannel(channel, 3, 4.4847, 0.75613);
				TestChannel(channel, 4, 8.8181, 1.72254);
				TestChannel(channel, 5, 6.4830, 0.94147);
				TestChannel(channel, 6, 9.2141, 1.50663);
#elif Q16
				TestChannel(channel, 0, 0.6942, -0.0601);
				TestChannel(channel, 1, 3.3988, 0.3089);
				TestChannel(channel, 2, 4.1168, 0.6083);
				TestChannel(channel, 3, 4.4844, 0.7555);
				TestChannel(channel, 4, 8.8174, 1.7219);
				TestChannel(channel, 5, 6.4821, 0.9408);
				TestChannel(channel, 6, 9.2147, 1.5058);
#elif Q16HDRI
				TestChannel(channel, 0, 0.6942, -0.06014);
				TestChannel(channel, 1, 3.3988, 0.30899);
				TestChannel(channel, 2, 4.1168, 0.60836);
				TestChannel(channel, 3, 4.4844, 0.75556);
				TestChannel(channel, 4, 8.8174, 1.72197);
				TestChannel(channel, 5, 6.4821, 0.94082);
				TestChannel(channel, 6, 9.2147, 1.50582);
#else
#error Not implemented!
#endif

				channel = phash.GetChannel(PixelChannel.Blue);
#if Q8
				TestChannel(channel, 0, 0.7223, 0.6984);
				TestChannel(channel, 1, 3.8298, 3.4611);
				TestChannel(channel, 2, 5.1306, 4.1311);
				TestChannel(channel, 3, 5.0218, 4.4867);
				TestChannel(channel, 4, 10.4768, 8.8668);
				TestChannel(channel, 5, 6.9454, 6.6108);
				TestChannel(channel, 6, 10.1398, 9.0722);
#elif Q16
				TestChannel(channel, 0, 0.7222, 0.6984);
				TestChannel(channel, 1, 3.8295, 3.4608);
				TestChannel(channel, 2, 5.1309, 4.1314);
				TestChannel(channel, 3, 5.0213, 4.4866);
				TestChannel(channel, 4, 10.4778, 8.8663);
				TestChannel(channel, 5, 6.9448, 6.6093);
				TestChannel(channel, 6, 10.1388, 9.0737);
#elif Q16HDRI
				TestChannel(channel, 0, 0.7222, 0.6984);
				TestChannel(channel, 1, 3.8295, 3.4608);
				TestChannel(channel, 2, 5.1309, 4.1314);
				TestChannel(channel, 3, 5.0213, 4.4866);
				TestChannel(channel, 4, 10.4778, 8.8663);
				TestChannel(channel, 5, 6.9448, 6.6093);
				TestChannel(channel, 6, 10.1388, 9.0737);
#else
#error Not implemented!
#endif
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new PerceptualHash(null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new PerceptualHash("");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new PerceptualHash("a0df");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new PerceptualHash("H00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ToString()
		{
#if NET20
			Assert.Inconclusive("VS2008 compiler produces different results, no clue why this is happening");
#endif

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();
				string hash = phash.ToString();
				Assert.AreEqual(210, hash.Length);
#if Q8
				Assert.AreEqual("81b4488656898d38a7a96223562017620f7a26cf81a12823dd85b948bf8d86e5d89b6b81b1f884cb8a0d38af2f622728fd3f623fedeac7a78beaed8d81d8984349824c783ada81c378959b8c86a8c42b628ed61b216279c81b49887348a15f8af43622a3619d362370", hash);
#elif Q16
				Assert.AreEqual("81b4488651898d38a7a8622346206c620f8a648f8290a8361086f778ca5d889098c65381b1e884c58a0d18af2d622718fd35623ffdeaeda78b3aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
#elif Q16HDRI
				Assert.AreEqual("81b4488651898d38a7a8622346206b620f8a728e82dde83ac4878188d4b2890f58db6b81b1e884c58a0d18af2d622718fd35623ffdeaeda78b4aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);
#else
#error Not implemented!
#endif
				PerceptualHash clone = new PerceptualHash(hash);
				Assert.AreEqual(0.0, phash.SumSquaredDistance(clone), 0.001);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SumSquaredDistance()
		{
#if NET20
			Assert.Inconclusive("VS2008 compiler produces different results, no clue why this is happening");
#endif

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();

				using (MagickImage other = new MagickImage(Files.MagickNETIconPNG))
				{
					PerceptualHash otherPhash = other.PerceptualHash();
#if Q8
					Assert.AreEqual(248.53, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16
					Assert.AreEqual(199.53, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16HDRI
					Assert.AreEqual(183.73, phash.SumSquaredDistance(otherPhash), 0.01);
#else
#error Not implemented!
#endif
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
