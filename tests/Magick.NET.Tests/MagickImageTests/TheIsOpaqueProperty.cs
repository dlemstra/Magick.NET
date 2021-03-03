// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheIsOpaqueProperty
        {
            [Fact]
            public void ShouldReturnTrueWhenImageIsOpaque()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.True(image.IsOpaque);
                }
            }

            [Fact]
            public void ShouldReturnFalseWhenImageIsNotOpaque()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Alpha(AlphaOption.Transparent);
                    Assert.False(image.IsOpaque);
                }
            }
        }
    }
}
