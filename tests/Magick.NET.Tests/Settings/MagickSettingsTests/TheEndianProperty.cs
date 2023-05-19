// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheEndianProperty
    {
        [Fact]
        public void ShouldDefaultToUndefined()
        {
            using var image = new MagickImage();

            Assert.Equal(Endian.Undefined, image.Settings.Endian);
        }

        [Fact]
        public void ShouldBeUsedWhenWritingTheImage()
        {
            using var input = new MagickImage(Files.NoisePNG);
            using var memStream = new MemoryStream();
            input.Settings.Endian = Endian.MSB;
            input.Format = MagickFormat.Ipl;
            input.Write(memStream);
            memStream.Position = 0;

            var settings = new MagickReadSettings();
            settings.Format = MagickFormat.Ipl;

            using var output = new MagickImage(memStream, settings);
            Assert.Equal(Endian.MSB, output.Endian);
        }
    }
}
