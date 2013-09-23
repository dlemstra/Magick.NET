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
	public class IptcProfileTests
	{
		//===========================================================================================
		private const string _Category = "IptcProfile";
		//===========================================================================================
		private static void TestProfile(IptcProfile profile)
		{
			Assert.IsNotNull(profile);

			Assert.AreEqual(18, profile.Values.Count());

			foreach (IptcValue value in profile.Values)
			{
				Assert.IsNotNull(value.Value);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Values()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				IptcProfile profile = image.GetIptcProfile();
				TestProfile(profile);

				using (MagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
				{
					Assert.IsNull(emptyImage.GetIptcProfile());
					emptyImage.AddProfile(profile);

					profile = emptyImage.GetIptcProfile();
					TestProfile(profile);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
