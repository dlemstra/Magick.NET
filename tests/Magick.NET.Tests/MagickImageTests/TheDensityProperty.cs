// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
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
        public class TheDensityProperty
        {
            [Fact]
            public void ShouldNotChangeWhenValueIsNull()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    Assert.Equal(300, image.Density.X);

                    image.Density = null;

                    Assert.Equal(300, image.Density.X);
                }
            }

            [Fact]
            public void ShouldUpdateExifProfile()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        var profile = image.GetExifProfile();
                        var value = profile.GetValue(ExifTag.XResolution);
                        Assert.Equal("300", value.ToString());

                        image.Density = new Density(72);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();
                        var value = profile.GetValue(ExifTag.XResolution);

                        Assert.Equal("72", value.ToString());
                    }
                }
            }

            [Fact]
            public void ShouldSetTheCorrectDimensionsWhenReadingImage()
            {
                using (var image = new MagickImage())
                {
                    Assert.Null(image.Settings.Density);

                    image.Settings.Density = new Density(100);

                    image.Read(Files.Logos.MagickNETSVG);
                    Assert.Equal(new Density(100, DensityUnit.Undefined), image.Density);
                    Assert.Equal(524, image.Width);
                    Assert.Equal(252, image.Height);
                }
            }
        }
    }
}
