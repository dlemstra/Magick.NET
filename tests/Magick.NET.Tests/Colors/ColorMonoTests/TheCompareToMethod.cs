// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ColorMonoTests
    {
        public class TheCompareToMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenOtherIsNull()
            {
                var color = new ColorMono(true);

                Assert.Equal(1, color.CompareTo(null));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenOtherIsEqual()
            {
                var color = new ColorMono(true);

                Assert.Equal(0, color.CompareTo(color));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenOtherIsLower()
            {
                var color = new ColorMono(false);
                var other = new ColorMono(true);

                Assert.Equal(1, color.CompareTo(other));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenOtherIsHigher()
            {
                var color = new ColorMono(true);
                var other = new ColorMono(false);

                Assert.Equal(-1, color.CompareTo(other));
            }
        }
    }
}
