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
	public sealed class PixelCollectionTests
	{
		//===========================================================================================
		private const string _Category = "PixelCollection";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Dimensions()
		{
			using (MagickImage image = new MagickImage(Color.Red, 5, 10))
			{
				PixelCollection pixels = image.GetReadOnlyPixels();
				Assert.AreEqual(5, pixels.Width);
				Assert.AreEqual(10, pixels.Height);
				Assert.AreEqual(5 * 10 * 4, pixels.GetValues().Length);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_GetValue()
		{
			using (MagickImage image = new MagickImage(Color.Red, 5, 10))
			{
				PixelCollection pixels = image.GetReadOnlyPixels();

				var values = pixels.GetValue(0, 0);
				Assert.AreEqual(4, values.Length);

				MagickColor color = new MagickColor(values[0], values[1], values[2], values[3]);
				ColorAssert.AreEqual(Color.Red, color);
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
