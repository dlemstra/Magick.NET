// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Linq;
using System.Xml.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class EightBimProfileTests
    {
        [TestClass]
        public class TheClipPathsMethod
        {
            [TestMethod]
            public void ShouldReturnTheClippingPaths()
            {
                using (IMagickImage image = new MagickImage(Files.EightBimTIF))
                {
                    var profile = image.Get8BimProfile();

                    Assert.IsNotNull(profile);

                    Assert.AreEqual(2, profile.ClipPaths.Count());

                    var first = profile.ClipPaths.First();
                    Assert.AreEqual("Path 1", first.Name);
                    XDocument doc = XDocument.Load(first.Path.CreateNavigator().ReadSubtree());

                    Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""M 45 58&#xA;L 80 124&#xA;L 147 147&#xA;L 45 147&#xA;L 45 58 Z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));

                    var second = profile.ClipPaths.Skip(1).First();
                    Assert.AreEqual("Path 2", second.Name);
                    doc = XDocument.Load(second.Path.CreateNavigator().ReadSubtree());

                    Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""M 52 144&#xA;L 130 57&#xA;L 157 121&#xA;L 131 106&#xA;L 52 144 Z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));
                }
            }

            [TestMethod]
            public void ShouldReturnEmptyListWhenSizeIsUnknown()
            {
                using (IMagickImage image = new MagickImage(Files.EightBimTIF))
                {
                    var profile = image.Get8BimProfile();

                    profile = new EightBimProfile(profile.ToByteArray());

                    Assert.IsNotNull(profile);

                    Assert.AreEqual(0, profile.ClipPaths.Count());
                }
            }
        }
    }
}
