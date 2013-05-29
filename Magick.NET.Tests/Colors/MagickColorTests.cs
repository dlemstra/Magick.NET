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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickColorTests
	{
		//===========================================================================================
		private const string _Category = "MagickColor";
#if Q8
		//===========================================================================================
		private const byte _Max = byte.MaxValue;
		//===========================================================================================
		private void TestHexColor(string hexValue, byte red, byte green, byte blue, byte alpha)
#elif Q16
		private const ushort _Max = ushort.MaxValue;
		//===========================================================================================
		private void TestHexColor(string hexValue, ushort red, ushort green, ushort blue, ushort alpha)
#else
#error Not implemented!
#endif
		{
			MagickColor color = new MagickColor(hexValue);

			Assert.AreEqual(red, color.R);
			Assert.AreEqual(green, color.G);
			Assert.AreEqual(blue, color.B);
			Assert.AreEqual(alpha, color.A);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickColor(null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickColor("FFFFFF");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickColor("#FFFFF");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickColor("#GGFFF");
			});

			TestHexColor("#F00", _Max, 0, 0, 0);
			TestHexColor("#0F0F", 0, _Max, 0, _Max);
			TestHexColor("#0000FF", 0, 0, _Max, 0);
			TestHexColor("#FF00FFFF", _Max, 0, _Max, _Max);

#if Q8
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickColor("#FFFF0000FFFF");
			});
#elif Q16
			TestHexColor("#0000FFFF0000", 0, _Max, 0, 0);
			TestHexColor("#FFFF00000000FFFF", _Max, 0, 0, _Max);
#else
#error Not implemented!
#endif
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IComparable()
		{
			MagickColor first = new MagickColor(Color.White);

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

			MagickColor second = new MagickColor(Color.Black);

			Assert.AreEqual(1, first.CompareTo(second));
			Assert.IsFalse(first < second);
			Assert.IsFalse(first <= second);
			Assert.IsTrue(first > second);
			Assert.IsTrue(first >= second);

			second = new MagickColor(Color.White);

			Assert.AreEqual(0, first.CompareTo(second));
			Assert.IsFalse(first < second);
			Assert.IsTrue(first <= second);
			Assert.IsFalse(first > second);
			Assert.IsTrue(first >= second);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			MagickColor first = new MagickColor(Color.Red);

			Assert.IsFalse(first == null);
			Assert.IsFalse(first.Equals(null));
			Assert.IsTrue(first.Equals(first));
			Assert.IsTrue(first.Equals((object)first));

			MagickColor second = new MagickColor(_Max, 0, 0);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));

			second = new MagickColor(Color.Green);

			Assert.IsTrue(first != second);
			Assert.IsFalse(first.Equals(second));
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ToColor()
		{
			MagickColor color = new MagickColor(Color.Red);
			Assert.AreEqual(0, color.A);

			Color magickRed = color.ToColor();
			Color red = Color.Red;

			Assert.AreEqual(magickRed.R, red.R);
			Assert.AreEqual(magickRed.G, red.G);
			Assert.AreEqual(magickRed.B, red.B);
			Assert.AreEqual(magickRed.A, red.A);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
