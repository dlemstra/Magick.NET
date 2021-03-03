// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public class TheCompareToMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenInstancesAreTheSame()
            {
                var first = new MagickGeometry(10, 5);

                Assert.Equal(0, first.CompareTo(first));
            }

            [Fact]
            public void ShouldReturnOneWhenInstancesIsNull()
            {
                var first = new MagickGeometry(10, 5);

                Assert.Equal(1, first.CompareTo(null));
            }

            [Fact]
            public void ShouldReturnZeroWhenInstancesAreEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(10, 5);

                Assert.Equal(0, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnOneWhenInstancesAreNotEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(5, 5);

                Assert.Equal(1, first.CompareTo(second));
            }
        }
    }
}
