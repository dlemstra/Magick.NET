// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
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

        [Fact]
        public void ShouldReturnBase64EncodedStringUsingTheSpecifiedDefines()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.Builtin.Logo);

            var defines = new TiffWriteDefines
            {
                PreserveCompression = true,
            };
            var base64 = images.ToBase64(defines);

            Assert.NotNull(base64);
            Assert.Equal(39952, base64.Length);

            var bytes = Convert.FromBase64String(base64);

            Assert.NotNull(bytes);
        }
    }
}
