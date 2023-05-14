// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCompareToMethod
    {
        [Fact]
        public void ShouldReturnZeroWhenInstancesAreTheSame()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);

            Assert.Equal(0, image.CompareTo(image));
        }

        [Fact]
        public void ShouldReturnOneWhenInstancesIsNull()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);

            Assert.Equal(1, image.CompareTo(null));
        }

        [Fact]
        public void ShouldReturnZeroWhenInstancesAreEqual()
        {
            using var first = new MagickImage(MagickColors.Red, 1, 1);
            using var second = new MagickImage(MagickColors.Red, 1, 1);

            Assert.Equal(0, first.CompareTo(second));
        }

        [Fact]
        public void ShouldReturnOneWhenInstancesAreNotEqual()
        {
            using var first = new MagickImage(MagickColors.Red, 1, 1);
            using var second = new MagickImage(MagickColors.Red, 2, 1);

            Assert.Equal(-1, first.CompareTo(second));
        }
    }
}
