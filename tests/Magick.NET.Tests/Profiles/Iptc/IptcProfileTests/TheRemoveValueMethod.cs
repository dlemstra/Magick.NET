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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        public class TheRemoveValueMethod
        {
            [Fact]
            public void ShouldRemoveTheValueAndReturnTrueWhenValueWasFound()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var result = profile.RemoveValue(IptcTag.Title);

                    Assert.True(result);

                    var value = profile.GetValue(IptcTag.Title);
                    Assert.Null(value);
                }
            }

            [Fact]
            public void ShouldReturnFalseWhenProfileDoesNotContainTag()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var result = profile.RemoveValue(IptcTag.ReferenceNumber);

                    Assert.False(result);
                }
            }
        }
    }
}
