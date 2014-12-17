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

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Coders
{
	//==============================================================================================
	[TestClass]
	public class PSDTests : CodersTests
	{
		//===========================================================================================
		private const string _Category = "PSDTests";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Colors()
		{
			using (MagickImage image = LoadImage("Player.psd"))
			{
				using (PixelCollection pixels = image.GetReadOnlyPixels())
				{
					Pixel pixel = pixels.GetPixel(0, 0);
					ColorAssert.AreEqual(MagickColor.Transparent, pixel.ToColor());

					pixel = pixels.GetPixel(8, 6);
					ColorAssert.AreEqual(Color.FromArgb(15, 43, 255), pixel.ToColor());
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}