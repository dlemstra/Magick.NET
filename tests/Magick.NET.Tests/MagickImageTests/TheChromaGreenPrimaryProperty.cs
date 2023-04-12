// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheChromaGreenPrimaryProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    Assert.InRange(image.ChromaGreenPrimary.X, 0.30, 0.301);
                    Assert.InRange(image.ChromaGreenPrimary.Y, 0.60, 0.601);
                    Assert.InRange(image.ChromaGreenPrimary.Z, 0.00, 0.001);
                }
            }

            [Fact]
            public void ShouldHaveTheCorrectValuesWhenChanged()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var info = new PrimaryInfo(0.5, 1.0, 1.5);

                    image.ChromaGreenPrimary = info;

                    Assert.InRange(image.ChromaGreenPrimary.X, 0.50, 0.501);
                    Assert.InRange(image.ChromaGreenPrimary.Y, 1.00, 1.001);
                    Assert.InRange(image.ChromaGreenPrimary.Z, 1.50, 1.501);
                }
            }
        }
    }
}
