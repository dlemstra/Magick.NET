// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheCompressionProperty
    {
        [Fact]
        public void ShouldHaveUndefinedAsTheDefaultValue()
        {
            using var image = new MagickImage();

            Assert.Equal(CompressionMethod.Undefined, image.Settings.Compression);
        }

        [Fact]
        public void ShouldBeUsedWhenWritingTheImage()
        {
            using var input = new MagickImage();
            input.Read(Files.Builtin.Wizard);
            input.Settings.Compression = CompressionMethod.NoCompression;

            var bytes = input.ToByteArray(MagickFormat.Bmp);

            using var output = new MagickImage(bytes);

            Assert.Equal(CompressionMethod.NoCompression, output.Compression);
        }
    }
}
