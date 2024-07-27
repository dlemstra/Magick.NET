// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSignatureProperty
    {
        [Fact]
        public void ShouldReturnImageSignatureForEmptyImage()
        {
            using var image = new MagickImage();

            Assert.Equal(0U, image.Width);
            Assert.Equal(0U, image.Height);
            Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", image.Signature);
        }

        [Fact]
        public void ShouldReturnImageSignatureOfImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Equal("1bed3c29f223f8c525206258d9601dd5da0bb572943b04bd6ad09ae5dc786d9d", image.Signature);
        }
    }
}
