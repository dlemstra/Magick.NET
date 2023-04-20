// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheMapCoder
    {
        [Fact]
        public void CanBeReadFromFileWithMapExtensions()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var tempFile = new TemporaryFile("test.map");
            image.Write(tempFile.File, MagickFormat.Map);
            image.Read(tempFile.File, image.Width, image.Height);

            Assert.Equal(MagickFormat.Map, image.Format);
        }

        [Fact]
        public void CannotBeReadFromFileWithoutMapExtensions()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var tempFile = new TemporaryFile("test");
            image.Write(tempFile.File, MagickFormat.Map);

            Assert.Throws<MagickMissingDelegateErrorException>(() => image.Read(tempFile.File, image.Width, image.Height));
        }
    }
}
