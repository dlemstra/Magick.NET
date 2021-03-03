// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public class TheToPointMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenXAndYNotSet()
            {
                var point = new MagickGeometry(10, 5).ToPoint();

                Assert.Equal(0, point.X);
                Assert.Equal(0, point.Y);
            }

            [Fact]
            public void ShouldReturnCorrectValue()
            {
                var point = new MagickGeometry(1, 2, 3, 4).ToPoint();

                Assert.Equal(1, point.X);
                Assert.Equal(2, point.Y);
            }
        }
    }
}
