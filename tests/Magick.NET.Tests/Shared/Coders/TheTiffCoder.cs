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
using ImageMagick.Formats.Tiff;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheTiffCoder
    {
        [Fact]
        public void ShouldIgnoreTheSpecifiedTags()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefine(MagickFormat.Tiff, "ignore-tags", "32934");
                image.Read(Files.Coders.IgnoreTagTIF);
            }

            using (var image = new MagickImage())
            {
                var readSettings = new MagickReadSettings(new TiffReadDefines()
                {
                    IgnoreTags = new string[] { "32934" },
                });

                image.Read(Files.Coders.IgnoreTagTIF, readSettings);
            }
        }

        [Fact]
        public void ShouldBeAbleToReadAndWriteIptcValues()
        {
            using (var input = new MagickImage(Files.MagickNETIconPNG))
            {
                var profile = input.GetIptcProfile();
                Assert.Null(profile);

                profile = new IptcProfile();
                profile.SetValue(IptcTag.Headline, "Magick.NET");
                profile.SetValue(IptcTag.CopyrightNotice, "Copyright.NET");

                input.SetProfile(profile);

                using (MemoryStream memStream = new MemoryStream())
                {
                    input.Format = MagickFormat.Tiff;
                    input.Write(memStream);

                    memStream.Position = 0;
                    using (var output = new MagickImage(memStream))
                    {
                        profile = output.GetIptcProfile();
                        Assert.NotNull(profile);
                        TestValue(profile, IptcTag.Headline, "Magick.NET");
                        TestValue(profile, IptcTag.CopyrightNotice, "Copyright.NET");
                    }
                }
            }
        }

        [Fact]
        public void ShouldBeAbleToWriteLzwPTiffToStream()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                image.Settings.Compression = CompressionMethod.LZW;
                using (var stream = new MemoryStream())
                {
                    image.Write(stream, MagickFormat.Ptif);
                }
            }
        }

        [Fact]
        public void ShouldBeAbleToUseGroup4Compression()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.Settings.Compression = CompressionMethod.Group4;
                using (var stream = new MemoryStream())
                {
                    input.Write(stream, MagickFormat.Tiff);

                    stream.Position = 0;
                    using (var output = new MagickImage(stream))
                    {
                        Assert.Equal(CompressionMethod.Group4, output.Compression);
                    }
                }
            }
        }

        [Fact]
        public void ShouldBeAbleToUseFaxCompression()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.Settings.Compression = CompressionMethod.Fax;
                using (var stream = new MemoryStream())
                {
                    input.Write(stream, MagickFormat.Tiff);

                    stream.Position = 0;
                    using (var output = new MagickImage(stream))
                    {
                        Assert.Equal(CompressionMethod.Fax, output.Compression);
                    }
                }
            }
        }

        [Fact]
        public void ShouldBeAbleToReadImageWithInfiniteRowsPerStrip()
        {
            using (var image = new MagickImage(Files.Coders.RowsPerStripTIF))
            {
                Assert.Equal(MagickFormat.Tiff, image.Format);
            }
        }

        private static void TestValue(IIptcProfile profile, IptcTag tag, string expectedValue)
        {
            var value = profile.GetValue(tag);
            Assert.NotNull(value);
            Assert.Equal(expectedValue, value.Value);
        }
    }
}