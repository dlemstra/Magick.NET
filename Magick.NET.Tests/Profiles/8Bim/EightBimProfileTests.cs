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
using System.Xml.Linq;
using System.Xml.XPath;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class EightBimProfileTests
	{
		//===========================================================================================
		private const string _Category = "EightBimProfile";
		//===========================================================================================
		private static void TestProfile(EightBimProfile profile)
		{
			Assert.IsNotNull(profile);

			Assert.AreEqual(2, profile.ClippingPaths.Count());

			IXPathNavigable first = profile.ClippingPaths.First();
			XDocument doc = XDocument.Load(first.CreateNavigator().ReadSubtree());

			Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""  45 58 m&#xA;  80 124 l&#xA;  147 147 l&#xA;  45 147 l&#xA;  45 58 l z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));

			IXPathNavigable second = profile.ClippingPaths.Skip(1).First();
			doc = XDocument.Load(second.CreateNavigator().ReadSubtree());

			Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""  52 144 m&#xA;  130 57 l&#xA;  157 121 l&#xA;  131 106 l&#xA;  52 144 l z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ClippingPaths()
		{
			using (MagickImage image = new MagickImage(Files.EightBimTIF))
			{
				EightBimProfile profile = image.Get8BimProfile();
				TestProfile(profile);
				string test = System.Text.Encoding.Default.GetString(profile.ToByteArray());

				using (MagickImage emptyImage = new MagickImage(Files.EightBimTIF))
				{
					emptyImage.Strip();
					Assert.IsNull(emptyImage.GetIptcProfile());
					emptyImage.AddProfile(profile);

					profile = emptyImage.Get8BimProfile();
					TestProfile(profile);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
