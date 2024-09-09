// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGetAttributeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("name", () => image.GetAttribute(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("name", () => image.GetAttribute(string.Empty));
        }

        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.GetAttribute("test"));
        }
    }
}
