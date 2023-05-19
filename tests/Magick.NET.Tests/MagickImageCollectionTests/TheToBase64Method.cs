// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheToBase64Method
    {
        [Fact]
        public void ShouldReturnEmptyStringWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Empty(images.ToBase64());
        }

        [Fact]
        public void ShouldReturnBase64StringOfTheImages()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.Builtin.Logo);

            var base64 = images.ToBase64(MagickFormat.Rgb);
            Assert.Equal(1228800, base64.Length);
        }
    }
}
