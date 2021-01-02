// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        public class TheSetValueMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenEncodingIsNull()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();

                    Assert.Throws<ArgumentNullException>("encoding", () =>
                    {
                        profile.SetValue(IptcTag.Title, null, string.Empty);
                    });
                }
            }

            [Fact]
            public void ShouldChangeTheValue()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.Title);

                    profile.SetValue(IptcTag.Title, "Magick.NET Title");

                    Assert.Equal("Magick.NET Title", value.Value);

                    value = profile.GetValue(IptcTag.Title);

                    Assert.Equal("Magick.NET Title", value.Value);
                }
            }

            [Fact]
            public void ShouldAddValueThatDoesNotExist()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.ReferenceNumber);

                    Assert.Null(value);

                    profile.SetValue(IptcTag.Title, "Magick.NET ReferenceNümber");

                    value = profile.GetValue(IptcTag.Title);

                    Assert.Equal("Magick.NET ReferenceNümber", value.Value);
                }
            }
        }
    }
}
