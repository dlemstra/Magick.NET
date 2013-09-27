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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class ColorMatrixTests
	{
		//===========================================================================================
		private const string _Category = "ColorMatrix";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new ColorMatrix(-1);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new ColorMatrix(7);
			});

			new ColorMatrix(1);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Value()
		{
			ColorMatrix matrix = new ColorMatrix(2);

			matrix.SetValue(0, 0, 1.5);
			Assert.AreEqual(1.5, matrix.GetValue(0, 0));

			Assert.AreEqual(0.0, matrix.GetValue(3, 1));
			Assert.AreEqual(0.0, matrix.GetValue(1, 3));
		}
		//===========================================================================================
	}
	//==============================================================================================
}
