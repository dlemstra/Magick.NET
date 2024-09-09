// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGetArtifactMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("name", () => image.GetArtifact(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("name", () => image.GetArtifact(string.Empty));
        }

        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.GetArtifact("test"));
        }
    }
}
