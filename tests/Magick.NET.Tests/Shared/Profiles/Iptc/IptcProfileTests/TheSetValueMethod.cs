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

using System;
using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        [TestClass]
        public class TheSetValueMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenEncodingIsNull()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();

                    ExceptionAssert.Throws<ArgumentNullException>("encoding", () =>
                    {
                        profile.SetValue(IptcTag.Title, null, string.Empty);
                    });
                }
            }

            [TestMethod]
            public void ShouldChangeTheValue()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.Title);

                    profile.SetValue(IptcTag.Title, "Magick.NET Title");

                    Assert.AreEqual("Magick.NET Title", value.Value);

                    value = profile.GetValue(IptcTag.Title);

                    Assert.AreEqual("Magick.NET Title", value.Value);
                }
            }

            [TestMethod]
            public void ShouldAddValueThatDoesNotExist()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.ReferenceNumber);

                    Assert.IsNull(value);

                    profile.SetValue(IptcTag.Title, "Magick.NET ReferenceNümber");

                    value = profile.GetValue(IptcTag.Title);

                    Assert.AreEqual("Magick.NET ReferenceNümber", value.Value);
                }
            }
        }
    }
}
