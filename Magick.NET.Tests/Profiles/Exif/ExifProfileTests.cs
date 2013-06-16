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
	public class ExifProfileTests
	{
		//===========================================================================================
		private const string _Category = "ExifProfile";
		//===========================================================================================
		private static void TestProfile(ExifProfile profile)
		{
			Assert.IsNotNull(profile);

			Assert.AreEqual(44, profile.Values.Count());

			foreach (ExifValue value in profile.Values)
			{
				Assert.IsNotNull(value.Value);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Thumbnail()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				Assert.IsNotNull(profile);

				using (MagickImage thumbnail = profile.CreateThumbnail())
				{
					Assert.IsNotNull(thumbnail);
					Assert.AreEqual(128, thumbnail.Width);
					Assert.AreEqual(85, thumbnail.Height);
					Assert.AreEqual(MagickFormat.Jpeg, thumbnail.Format);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Values()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				TestProfile(profile);

				using (MagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
				{
					Assert.IsNull(emptyImage.GetExifProfile());
					emptyImage.AddProfile(profile);

					profile = emptyImage.GetExifProfile();
					TestProfile(profile);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
