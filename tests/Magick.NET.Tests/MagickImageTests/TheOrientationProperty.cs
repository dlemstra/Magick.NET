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

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheOrientationProperty
        {
            [Fact]
            public void ShouldOverwriteTheExifOrientation()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();
                    var exifOrientation = profile.GetValue(ExifTag.Orientation).Value;
                    Assert.Equal((ushort)1, exifOrientation);

                    Assert.Equal(OrientationType.TopLeft, image.Orientation);

                    profile.SetValue(ExifTag.Orientation, (ushort)6); // RightTop
                    image.SetProfile(profile);

                    image.Orientation = OrientationType.LeftBotom;

                    using (var stream = new MemoryStream())
                    {
                        image.Write(stream);

                        stream.Position = 0;
                        using (var output = new MagickImage(stream))
                        {
                            profile = output.GetExifProfile();
                            exifOrientation = profile.GetValue(ExifTag.Orientation).Value;
                            Assert.Equal((ushort)8, exifOrientation);

                            Assert.Equal(OrientationType.LeftBotom, image.Orientation);
                        }
                    }
                }
            }
        }
    }
}
