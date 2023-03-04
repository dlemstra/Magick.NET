// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var image = new MagickImage(MagickColors.Red, 1, 1);

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
                var first = new MagickImage(MagickColors.Red, 2, 1);
                var second = new MagickImage(MagickColors.Red, 1, 1);

                Assert.False(first < second);
                Assert.False(first <= second);
                Assert.True(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceHasSameSize()
            {
                var first = new MagickImage(MagickColors.Red, 1, 2);
                var second = new MagickImage(MagickColors.Red, 2, 1);

                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
            {
                var first = new MagickImage(MagickColors.Red, 1, 1);
                var second = new MagickImage(MagickColors.Red, 1, 1);

                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }
        }
    }
}
