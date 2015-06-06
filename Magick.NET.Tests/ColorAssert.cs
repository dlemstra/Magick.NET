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
using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	public static class ColorAssert
	{
		//===========================================================================================
		public static void AreEqual(Color expected, Color actual)
		{
			Assert.AreEqual(expected.R, actual.R, "R is not equal");
			Assert.AreEqual(expected.G, actual.G, "G is not equal");
			Assert.AreEqual(expected.B, actual.B, "B is not equal");
			Assert.AreEqual(expected.A, actual.A, "A is not equal");
		}
		//===========================================================================================
		public static void AreEqual(Color expected, MagickColor actual)
		{
			Assert.IsNotNull(actual);
			AreEqual(expected, (Color)actual);
		}
		//===========================================================================================
		public static void AreNotEqual(Color notExpected, Color actual)
		{
			if (notExpected.R == actual.R && notExpected.G == actual.G &&
				 notExpected.B == actual.B && notExpected.A == actual.A)
				Assert.Fail("Colors are the same");
		}
		//===========================================================================================
		public static void AreEqual(Color expected, Pixel actual)
		{
			AreEqual(expected, actual.ToColor());
		}
		//===========================================================================================
		public static void AreEqual(Pixel expected, Pixel actual)
		{
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);

			AreEqual(expected.ToColor(), actual.ToColor());
		}
		//===========================================================================================
		public static void AreNotEqual(Pixel expected, Pixel actual)
		{
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);

			AreNotEqual(expected.ToColor(), actual.ToColor());
		}
		//===========================================================================================
		public static void IsNotTransparent(Color color)
		{
			Assert.AreEqual(255, color.A);
		}
		//===========================================================================================
		public static void IsTransparent(float alpha)
		{
			Assert.AreEqual(0, alpha);
		}
		//===========================================================================================
		public static void IsNotTransparent(float alpha)
		{
			Assert.AreEqual(Quantum.Max, alpha);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
