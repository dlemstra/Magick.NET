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
