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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class ExifValueTests
	{
		//===========================================================================================
		private const string _Category = "ExifValue";
		//===========================================================================================
		private static ExifValue GetExifValue()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				Assert.IsNotNull(profile);

				return profile.Values.First();
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			ExifValue first = GetExifValue();
			ExifValue second = GetExifValue();

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Properties()
		{
			ExifValue value = GetExifValue();

			Assert.AreEqual(ExifDataType.Ascii, value.DataType);
			Assert.AreEqual(ExifTag.ImageDescription, value.Tag);
			Assert.AreEqual(false, value.IsArray);
			Assert.AreEqual("Communications", value.ToString());
			Assert.AreEqual("Communications", value.Value);
			Assert.AreEqual(value.Value, value.ToString());
		}
		//===========================================================================================
	}
	//==============================================================================================
}
