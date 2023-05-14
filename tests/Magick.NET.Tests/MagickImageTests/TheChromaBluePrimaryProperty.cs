// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChromaBluePrimaryProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectDefaultValues()
        {
            using var image = new MagickImage(Files.SnakewarePNG);

            Assert.InRange(image.ChromaBluePrimary.X, 0.15, 0.151);
            Assert.InRange(image.ChromaBluePrimary.Y, 0.06, 0.061);
            Assert.InRange(image.ChromaBluePrimary.Z, 0.00, .001);
        }

        [Fact]
        public void ShouldHaveTheCorrectValuesWhenChanged()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var info = new PrimaryInfo(0.5, 1.0, 1.5);
            image.ChromaBluePrimary = info;

            Assert.InRange(image.ChromaBluePrimary.X, 0.50, 0.501);
            Assert.InRange(image.ChromaBluePrimary.Y, 1.00, 1.001);
            Assert.InRange(image.ChromaBluePrimary.Z, 1.50, 1.501);
        }
    }
}
