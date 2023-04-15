// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
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

                        var value = exifProfile.GetValue(ExifTag.ImageUniqueID);
                        Assert.Equal("Have a nice day", value.ToString());
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

        [Fact]
        public void ShouldNotWriteJpegAndPngProperties()
        {
            using var input = new MagickImage(Files.Builtin.Logo);
            input.SetAttribute("foo", "bar");
            input.SetAttribute("jpeg:foo", "bar");
            input.SetAttribute("png:foo", "bar");

            using var memoryStream = new MemoryStream();
            input.Write(memoryStream, MagickFormat.Png);
            memoryStream.Position = 0;

            using var output = new MagickImage(memoryStream);
            Assert.Equal("bar", output.GetAttribute("foo"));
            Assert.Null(output.GetAttribute("jpeg:foo"));
            Assert.Null(output.GetAttribute("png:foo"));
        }

        [Fact]
        public void ShouldWriteDateProperties()
        {
            var dateCreate = "2023-04-15T09:25:37+00:00";
            var dateModify = "2023-04-15T09:25:42+00:00";

            using var input = new MagickImage(Files.DatePNG);
            Assert.Equal(dateCreate, input.GetAttribute("date:create"));
            Assert.Equal(dateModify, input.GetAttribute("date:modify"));

            using var memoryStream = new MemoryStream();
            input.Write(memoryStream, MagickFormat.Png);
            memoryStream.Position = 0;

            using var output = new MagickImage(memoryStream);
            dateCreate = output.GetAttribute("date:create");
            dateModify = output.GetAttribute("date:modify");
            Assert.Equal(dateCreate, dateModify);
            Assert.True(DateTime.TryParseExact(dateCreate, "yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        }

        [Fact]
        public async Task ShouldNotWriteDatePropertiesWhenDateShouldBeExcluded()
        {
            using var temporaryFile = new TemporaryFile(new byte[] { 0 });

            // The date:create property will use the creation time of the file if the property is not there, we are waiting
            // for a second to make sure there is a difference in the timestamp.
            await Task.Delay(1000);

            using var input = new MagickImage(Files.DatePNG);
            input.Settings.SetDefine("png:exclude-chunks", "date");
            input.Write(temporaryFile.FullName);

            using var output = new MagickImage(temporaryFile.FullName);
            var dateCreate = output.GetAttribute("date:create");
            var dateModify = output.GetAttribute("date:modify");
            Assert.NotEqual(dateCreate, dateModify);
            Assert.True(DateTime.TryParseExact(dateCreate, "yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
            Assert.True(DateTime.TryParseExact(dateModify, "yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        }

        private void HandleWarning(object sender, WarningEventArgs e)
            => throw new XunitException("Warning was raised: " + e.Message);
    }
}
