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
    public class TheJpegCoder
    {
        [Fact]
        public void ShouldDecodeCorrectly()
        {
            using (var image = new MagickImage(Files.WhiteJPG))
            {
                using (var pixels = image.GetPixels())
                {
                    var color = pixels.GetPixel(0, 0).ToColor();

                    Assert.Equal(Quantum.Max, color.R);
                    Assert.Equal(Quantum.Max, color.G);
                    Assert.Equal(Quantum.Max, color.B);
                    Assert.Equal(Quantum.Max, color.A);
                }
            }
        }

        [Fact]
        public void ShouldReadImageProfile()
        {
            using (var image = new MagickImage(Files.CMYKJPG))
            {
                image.SetProfile(ColorProfile.USWebCoatedSWOP);
                using (var memoryStream = new MemoryStream())
                {
                    image.Write(memoryStream);

                    memoryStream.Position = 0;
                    image.Read(memoryStream);

                    var profile = image.GetColorProfile();
                    Assert.NotNull(profile);
                }
            }
        }
    }
}
