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
            using var file = new TemporaryFile("test.map");

            file.Write(image, MagickFormat.Map);
            image.Read(file.File.FullName, image.Width, image.Height);

            Assert.Equal(MagickFormat.Map, image.Format);
        }

        [Fact]
        public void CannotBeReadFromFileWithoutMapExtensions()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var file = new TemporaryFile("test");

            file.Write(image, MagickFormat.Map);

            Assert.Throws<MagickMissingDelegateErrorException>(() => image.Read(file.File.FullName, image.Width, image.Height));
        }
    }
}
