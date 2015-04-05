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
		[TestMethod, TestCategory(_Category)]
		public void Test_Channel()
		{
			Assert.Inconclusive("Check again after rebuild of ImageMagick.");

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();
				ChannelPerceptualHash channel = phash.GetChannel(PixelChannel.Red);

#if Q8
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#elif Q16
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#elif Q16HDRI
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#else
#error Not implemented!
#endif
				ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate()
				{
					channel.HclpHuPhash(7);
				});

#if Q8
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#elif Q16
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#elif Q16HDRI
				Assert.AreEqual(0.0, channel.HclpHuPhash(0), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(1), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(2), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(3), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(4), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(5), 0.0001);
				Assert.AreEqual(0.0, channel.HclpHuPhash(6), 0.0001);
#else
#error Not implemented!
#endif
				ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate()
				{
					channel.SrgbHuPhash(7);
				});

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
			Assert.Inconclusive("Check again after rebuild of ImageMagick.");

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();
				string hash = phash.ToString();
				Assert.AreEqual(210, hash.Length);
#if Q8
				Assert.AreEqual("", hash);
#elif Q16
				Assert.AreEqual("", hash);
#elif Q16HDRI
				Assert.AreEqual("", hash);
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
			Assert.Inconclusive("Check again after rebuild of ImageMagick.");

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();

				using (MagickImage other = new MagickImage(Files.MagickNETIconPNG))
				{
					PerceptualHash otherPhash = other.PerceptualHash();
#if Q8
					Assert.AreEqual(0.0, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16
					Assert.AreEqual(0.0, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16HDRI
					Assert.AreEqual(0.0, phash.SumSquaredDistance(otherPhash), 0.01);
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
