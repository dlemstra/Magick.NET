// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);

            Assert.False(image < null);
            Assert.False(image <= null);
            Assert.True(image > null);
            Assert.True(image >= null);
            Assert.True(null < image);
            Assert.True(null <= image);
            Assert.False(null > image);
            Assert.False(null >= image);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
        {
            using var first = new MagickImage(MagickColors.Red, 2, 1);
            using var second = new MagickImage(MagickColors.Red, 1, 1);

            Assert.False(first < second);
            Assert.False(first <= second);
            Assert.True(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceHasSameSize()
        {
            using var first = new MagickImage(MagickColors.Red, 1, 2);
            using var second = new MagickImage(MagickColors.Red, 2, 1);

            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
        {
            using var first = new MagickImage(MagickColors.Red, 1, 1);
            using var second = new MagickImage(MagickColors.Red, 1, 1);

            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }
    }
}
