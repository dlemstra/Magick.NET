// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenHashIsNull()
        {
            Assert.Throws<ArgumentNullException>("hash", () => new PerceptualHash(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHashIsEmpty()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsTooSmall()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash("a0df"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsInvalid()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash("H00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
        }
    }
}
