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
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        [TestClass]
        public class TheRemoveValueMethod
        {
            [TestMethod]
            public void ShouldRemoveTheValueAndReturnTrueWhenValueWasFound()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var result = profile.RemoveValue(IptcTag.Title);

                    Assert.IsTrue(result);

                    var value = profile.GetValue(IptcTag.Title);
                    Assert.IsNull(value);
                }
            }

            [TestMethod]
            public void ShouldRemoveAllValues()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");

                var result = profile.RemoveValue(IptcTag.Byline);

                Assert.IsTrue(result);
                Assert.AreEqual(0, profile.Values.Count());
            }

            [TestMethod]
            public void ShouldOnlyRemoveTheValueWithTheSpecifiedValue()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");

                var result = profile.RemoveValue(IptcTag.Byline, "test2");

                Assert.IsTrue(result);
                Assert.IsTrue(profile.Values.Contains(new IptcValue(IptcTag.Byline, Encoding.UTF8.GetBytes("test"))));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenProfileDoesNotContainTag()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var result = profile.RemoveValue(IptcTag.ReferenceNumber);

                    Assert.IsFalse(result);
                }
            }
        }
    }
}
