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
        public class TheSetEncodingMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenEncodingIsNull()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();

                    Assert.Throws<ArgumentNullException>("encoding", () =>
                    {
                        profile.SetEncoding(null);
                    });
                }
            }

            [Fact]
            public void ShouldSetTheEncodingOfTheValues()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var firstValue = profile.Values.First();
                    Assert.NotEqual(Encoding.ASCII, firstValue.Encoding);

                    profile.SetEncoding(Encoding.ASCII);
                    Assert.Equal(Encoding.ASCII, firstValue.Encoding);
                }
            }
        }
    }
}
