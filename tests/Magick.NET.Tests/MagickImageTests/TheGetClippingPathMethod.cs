// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGetClippingPathMethod
    {
        [Fact]
        public void ShouldReturnTheFirstClippingPath()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var clippingPath = image.GetClippingPath();
                Assert.NotNull(clippingPath);
            }
        }

        [Fact]
        public void ShouldReturnTheSpecifiedClippingPath()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var clippingPath = image.GetClippingPath("#1");
                Assert.NotNull(clippingPath);
            }
        }
    }
}
