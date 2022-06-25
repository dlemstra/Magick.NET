// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheChromaWhitePointProperty
        {
            [Fact]
            public void ShouldAllowNullValue()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.ChromaWhitePoint = null;
                }
            }

            [Fact]
            public void ShouldHaveTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    Assert.InRange(image.ChromaWhitePoint.X, 0.3127, 0.31271);
                    Assert.InRange(image.ChromaWhitePoint.Y, 0.329, 0.3291);
                    Assert.InRange(image.ChromaWhitePoint.Z, 0.00, 0.001);
                }
            }

            [Fact]
            public void ShouldHaveTheCorrectValuesWhenChanged()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var info = new PrimaryInfo(0.5, 1.0, 1.5);

                    image.ChromaWhitePoint = info;

                    Assert.InRange(image.ChromaWhitePoint.X, 0.50, 0.501);
                    Assert.InRange(image.ChromaWhitePoint.Y, 1.00, 1.001);
                    Assert.InRange(image.ChromaWhitePoint.Z, 1.50, 1.501);
                }
            }
        }
    }
}
