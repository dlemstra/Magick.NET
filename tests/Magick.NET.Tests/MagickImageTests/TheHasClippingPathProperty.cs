// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheHasClippingPathProperty
        {
            [Fact]
            public void ShouldReturnFalseWhenImageHasNoClippingPath()
            {
                using (var noPath = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.False(noPath.HasClippingPath);
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenImageHasClippingPath()
            {
                using (var hasPath = new MagickImage(Files.InvitationTIF))
                {
                    Assert.True(hasPath.HasClippingPath);
                }
            }
        }
    }
}
