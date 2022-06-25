// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheChromaRedPrimaryProperty
        {
            [Fact]
            public void ShouldAllowNullValue()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.ChromaRedPrimary = null;
                }
            }

            [Fact]
            public void ShouldHaveTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    Assert.InRange(image.ChromaRedPrimary.X, 0.64, 0.641);
                    Assert.InRange(image.ChromaRedPrimary.Y, 0.33, 0.331);
                    Assert.InRange(image.ChromaRedPrimary.Z, 0.00, 0.001);
                }
            }

            [Fact]
            public void ShouldHaveTheCorrectValuesWhenChanged()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var info = new PrimaryInfo(0.5, 1.0, 1.5);

                    image.ChromaRedPrimary = info;

                    Assert.InRange(image.ChromaRedPrimary.X, 0.50, 0.501);
                    Assert.InRange(image.ChromaRedPrimary.Y, 1.00, 1.001);
                    Assert.InRange(image.ChromaRedPrimary.Z, 1.50, 1.501);
                }
            }
        }
    }
}
