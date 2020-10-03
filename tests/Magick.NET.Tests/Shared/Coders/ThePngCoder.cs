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
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public class ThePngCoder
    {
        [Fact]
        public void ShouldThrowExceptionAndNotChangeTheOriginalImageWhenTheImageIsCorrupt()
        {
            using (var image = new MagickImage(MagickColors.Purple, 4, 2))
            {
                Assert.Throws<MagickCoderErrorException>(() =>
                {
                    image.Read(Files.CorruptPNG);
                });

                Assert.Equal(4, image.Width);
                Assert.Equal(2, image.Height);
            }
        }

        [Fact]
        public void ShouldBeAbleToReadPngWithLargeIDAT()
        {
            using (var image = new MagickImage(Files.VicelandPNG))
            {
                Assert.Equal(200, image.Width);
                Assert.Equal(28, image.Height);
            }
        }

        [Fact]
        public void ShouldNotRaiseWarningForValidModificationDateThatBecomes24Hours()
        {
            using (var image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T20:35:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        [Fact]
        public void ShouldNotRaiseWarningForValidModificationDateThatBecomes60Minutes()
        {
            using (var image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T15:30:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        [Fact]
        public void ShouldReadTheExifChunk()
        {
            using (var input = new MagickImage(MagickColors.YellowGreen, 1, 1))
            {
                IExifProfile exifProfile = new ExifProfile();
                exifProfile.SetValue(ExifTag.ImageUniqueID, "Have a nice day");

                input.SetProfile(exifProfile);

                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Png);

                    memoryStream.Position = 0;

                    using (var output = new MagickImage(memoryStream))
                    {
                        exifProfile = output.GetExifProfile();

                        Assert.NotNull(exifProfile);
                    }
                }
            }
        }

        [Fact]
        public void ShouldSetTheAnimationProperties()
        {
            using (var images = new MagickImageCollection(Files.Coders.TestMNG))
            {
                Assert.Equal(8, images.Count);

                foreach (var image in images)
                {
                    Assert.Equal(20, image.AnimationDelay);
                    Assert.Equal(100, image.AnimationTicksPerSecond);
                }
            }
        }

        [Fact]
        public void ShouldWritePng00Correctly()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                using (var stream = new MemoryStream())
                {
                    image.Write(stream, MagickFormat.Png);

                    stream.Position = 0;

                    image.Read(stream);

                    var setting = new QuantizeSettings
                    {
                        ColorSpace = ColorSpace.Gray,
                        DitherMethod = DitherMethod.Riemersma,
                        Colors = 2,
                    };

                    image.Quantize(setting);

                    image.Warning += HandleWarning;

                    image.Write(stream, MagickFormat.Png00);

                    stream.Position = 0;

                    image.Read(stream);

                    Assert.Equal(ColorType.Palette, image.ColorType);
                    ColorAssert.Equal(MagickColors.White, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Black, image, 305, 248);
                }
            }
        }

        private void HandleWarning(object sender, WarningEventArgs e)
            => throw new XunitException("Warning was raised: " + e.Message);
    }
}