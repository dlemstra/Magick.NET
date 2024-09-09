// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheHasProfileMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("name", () => image.HasProfile(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("name", () => image.HasProfile(string.Empty));
        }

        [Fact]
        public void ShouldReturnTrueWhenImageHasProfileWithTheSpecifiedName()
        {
            using var image = new MagickImage(Files.InvitationTIF);

            Assert.True(image.HasProfile("icc"));
        }

        [Fact]
        public void ShouldReturnFalseWhenImageDoesNotHaveProfileWithTheSpecifiedName()
        {
            using var image = new MagickImage(Files.InvitationTIF);

            Assert.False(image.HasProfile("foo"));
        }
    }
}
