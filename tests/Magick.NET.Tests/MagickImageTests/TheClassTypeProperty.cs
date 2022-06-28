// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheClassTypeProperty
        {
            [Fact]
            public void ShouldHaveDirectAsDefaultValue()
            {
                using (var image = new MagickImage())
                {
                    Assert.Equal(ClassType.Direct, image.ClassType);
                }
            }

            [Fact]
            public void ShouldChangeTheClassType()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    Assert.Equal(ClassType.Direct, image.ClassType);

                    image.ClassType = ClassType.Pseudo;
                    Assert.Equal(ClassType.Pseudo, image.ClassType);

                    image.ClassType = ClassType.Direct;
                    Assert.Equal(ClassType.Direct, image.ClassType);
                }
            }
        }
    }
}
