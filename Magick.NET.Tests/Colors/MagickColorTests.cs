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

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickColorTests : InitializeTests
	{
		//===========================================================================================
		private const string _Category = "MagickColor";
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

#if Q8
			MagickColor second = new MagickColor(byte.MaxValue, 0, 0);
#elif Q16
			MagickColor second = new MagickColor(ushort.MaxValue, 0, 0);
#else
			Not implemented!
#endif

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
			MagickColor first = new MagickColor(Color.Red);
			Color magickRed = first.ToColor();
			Color red = Color.Red;

			Assert.IsTrue(
				magickRed.A == red.A &&
				magickRed.R == red.R &&
				magickRed.G == red.G &&
				magickRed.B == red.B
				);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
