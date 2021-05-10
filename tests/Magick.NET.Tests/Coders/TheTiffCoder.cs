// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
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
                var readSettings = new MagickReadSettings(new TiffReadDefines
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

        [Fact]
        public void ShouldBeAbleToReadImageWithLargeScanLine()
        {
            using (var image = new MagickImage(MagickColors.Green, 1000, 1))
            {
                image.Settings.Compression = CompressionMethod.Zip;

                var data = image.ToByteArray(MagickFormat.Tiff);
                Assert.NotNull(data);

                image.Read(data);
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