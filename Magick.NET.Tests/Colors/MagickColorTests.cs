//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
		//===========================================================================================
		private void TestColor(string hexValue, float red, float green, float blue, bool isTransparent)
		{
			TestColor(hexValue, red, green, blue, isTransparent, 0.01);
		}
		//===========================================================================================
		private void TestColor(string hexValue, float red, float green, float blue, bool isTransparent, double delta)
		{
			MagickColor color = new MagickColor(hexValue);

			Assert.AreEqual(red, color.R, delta);
			Assert.AreEqual(green, color.G, delta);
			Assert.AreEqual(blue, color.B, delta);

			if (isTransparent)
				ColorAssert.IsTransparent(color.A);
			else
				ColorAssert.IsNotTransparent(color.A);
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

			TestColor("#F00", Quantum.Max, 0, 0, false);
			TestColor("#0F00", 0, Quantum.Max, 0, true);
			TestColor("#0000FF", 0, 0, Quantum.Max, false);
			TestColor("#FF00FF00", Quantum.Max, 0, Quantum.Max, true);

#if Q8
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickColor("#FFFF0000FFFF");
			});
#elif Q16 || Q16HDRI
			TestColor("#0000FFFF0000", 0, Quantum.Max, 0, false);
			TestColor("#FFFF000000000000", Quantum.Max, 0, 0, true);
#else
#error Not implemented!
#endif

			float half = (float)Quantum.Max * 0.5f;
			TestColor("gray(50%) ", half, half, half, false, 1);
			TestColor("rgba(100%, 0%, 0%, 0.0)", Quantum.Max, 0, 0, true);
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

			MagickColor second = new MagickColor(Quantum.Max, 0, 0);

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
			Assert.AreEqual(Quantum.Max, color.A);

			ColorAssert.AreEqual(Color.Red, color);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ToString()
		{
			MagickColor color = new MagickColor(Color.Red);
#if Q8
			Assert.AreEqual("#FF0000FF", color.ToString());
#elif Q16 || Q16HDRI
			Assert.AreEqual("#FFFF00000000FFFF", color.ToString());
#else
#error Not implemented!
#endif
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Transparent()
		{
			MagickColor transparent = MagickColor.Transparent;

			ColorAssert.IsTransparent(transparent.A);
			ColorAssert.AreEqual(Color.Transparent, transparent);

			transparent = new MagickColor("transparent");

			ColorAssert.IsTransparent(transparent.A);
			ColorAssert.AreEqual(Color.Transparent, transparent);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
