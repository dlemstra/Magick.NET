//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
using System.Drawing;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickImageInfoTests
	{
		//===========================================================================================
		private const string _Category = "MagickImageInfo";
		//===========================================================================================
		private MagickImageInfo CreateMagickImageInfo(Color color, int width, int height)
		{
			using (MemoryStream memStream = new MemoryStream())
			{
				using (MagickImage image = new MagickImage(color, width, height))
				{
					image.Format = MagickFormat.Png;
					image.Write(memStream);
					memStream.Position = 0;

					return new MagickImageInfo(memStream);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickImageInfo imageInfo = new MagickImageInfo(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageInfo imageInfo = new MagickImageInfo((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageInfo imageInfo = new MagickImageInfo((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageInfo imageInfo = new MagickImageInfo((string)null);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IComparable()
		{
			MagickImageInfo first = CreateMagickImageInfo(Color.Red, 10, 5);

			Assert.AreEqual(0, first.CompareTo(first));
			Assert.AreEqual(1, first.CompareTo(null));
			Assert.IsFalse(first < null);
			Assert.IsFalse(first <= null);
			Assert.IsTrue(first > null);
			Assert.IsTrue(first >= null);
			Assert.IsTrue(null < first);
			Assert.IsTrue(null <= first);
			Assert.IsFalse(null > first);
			Assert.IsFalse(null >= first);

			MagickImageInfo second = CreateMagickImageInfo(Color.Green, 5, 5);

			Assert.AreEqual(1, first.CompareTo(second));
			Assert.IsFalse(first < second);
			Assert.IsFalse(first <= second);
			Assert.IsTrue(first > second);
			Assert.IsTrue(first >= second);

			second = CreateMagickImageInfo(Color.Red, 5, 10);

			Assert.AreEqual(0, first.CompareTo(second));
			Assert.IsFalse(first == second);
			Assert.IsFalse(first < second);
			Assert.IsTrue(first <= second);
			Assert.IsFalse(first > second);
			Assert.IsTrue(first >= second);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			MagickImageInfo first = CreateMagickImageInfo(Color.Red, 10, 10);

			Assert.IsFalse(first == null);
			Assert.IsFalse(first.Equals(null));
			Assert.IsTrue(first.Equals(first));
			Assert.IsTrue(first.Equals((object)first));

			MagickImageInfo second = CreateMagickImageInfo(Color.Red, 10, 10);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));

			second = CreateMagickImageInfo(Color.Green, 10, 10);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Read()
		{
			MagickImage image = new MagickImage();

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				image.Read(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((string)null);
			});

			image.Dispose();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
