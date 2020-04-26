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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        [TestClass]
        public class TheGetValueMethod
        {
            [TestMethod]
            public void ShouldReturnNullWhenImageDoesNotContainValue()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.ReferenceNumber);

                    Assert.IsNull(value);
                }
            }

            [TestMethod]
            public void ShouldReturnTheValue()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.Title);

                    Assert.IsNotNull(value);
                    Assert.AreEqual(value.Value, "Communications");
                }
            }

            [TestMethod]
            public void ShouldReturnAllValues()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");
                profile.SetValue(IptcTag.Caption, "test");

                var result = profile.GetAllValues(IptcTag.Byline);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}
