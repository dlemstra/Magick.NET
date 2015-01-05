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
			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();
				string hash = phash.ToString();
				Assert.AreEqual(210, hash.Length);
				Assert.AreEqual("81b4488651898d38a7a8622346206c620f8a64918290c8360f86f748ca668890f8c64681b1e884c58a0d18af2d622718fd35623ffdeaeda78b3aeda581d8484344824c083ad281c37895978c86d8c425628ee61b216279b81b48887318a1628af42622a2619d162372", hash);

				PerceptualHash clone = new PerceptualHash(hash);
				Assert.AreEqual(0.0, phash.SumSquaredDistance(clone), 0.001);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SumSquaredDistance()
		{
			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				PerceptualHash phash = image.PerceptualHash();

				using (MagickImage other = new MagickImage(Files.MagickNETIconPNG))
				{
					PerceptualHash otherPhash = other.PerceptualHash();
					Assert.AreEqual(578.61, phash.SumSquaredDistance(otherPhash), 0.01);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
