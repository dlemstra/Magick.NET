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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class TheToByteArrayMethod
        {
            [Fact]
            public void ShouldReturnOriginalDataWhenNotParsed()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();

                    var bytes = profile.ToByteArray();
                    Assert.Equal(4706, bytes.Length);
                }
            }

            [Fact]
            public void ShouldPreserveTheThumbnail()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();
                    Assert.NotNull(profile);

                    var bytes = profile.ToByteArray();

                    profile = new ExifProfile(bytes);

                    using (var thumbnail = profile.CreateThumbnail())
                    {
                        Assert.NotNull(thumbnail);
                        Assert.Equal(128, thumbnail.Width);
                        Assert.Equal(85, thumbnail.Height);
                        Assert.Equal(MagickFormat.Jpeg, thumbnail.Format);
                    }
                }
            }
        }
    }
}
