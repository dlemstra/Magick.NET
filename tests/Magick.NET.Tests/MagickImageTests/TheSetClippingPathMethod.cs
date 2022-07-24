// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetClippingPathMethod
        {
            [Fact]
            public void ShouldSetTheClippingPath()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.False(image.HasClippingPath);

                    using (var path = new MagickImage(Files.InvitationTIF))
                    {
                        var clippingPath = path.GetClippingPath();

                        image.SetClippingPath(clippingPath);

                        Assert.True(image.HasClippingPath);
                    }
                }
            }

            [Fact]
            public void ShouldSetTheClippingPathWithTheSpecfiedName()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.False(image.HasClippingPath);

                    using (var path = new MagickImage(Files.InvitationTIF))
                    {
                        var clippingPath = path.GetClippingPath();

                        image.SetClippingPath(clippingPath, "test");

                        Assert.NotNull(image.GetClippingPath("test"));
                        Assert.Null(image.GetClippingPath("#1"));
                    }
                }
            }
        }
    }
}
