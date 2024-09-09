// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetClippingPathMethod
    {
        [Fact]
        public void ShouldSetTheClippingPath()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);

            Assert.Null(image.GetClippingPath());

            using var path = new MagickImage(Files.InvitationTIF);
            var clippingPath = path.GetClippingPath();
            Assert.NotNull(clippingPath);

            image.SetClippingPath(clippingPath);

            Assert.NotNull(image.GetClippingPath());
        }

        [Fact]
        public void ShouldSetTheClippingPathWithTheSpecifiedName()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);

            Assert.Null(image.GetClippingPath());

            using var path = new MagickImage(Files.InvitationTIF);
            var clippingPath = path.GetClippingPath();
            Assert.NotNull(clippingPath);

            image.SetClippingPath(clippingPath, "test");

            Assert.NotNull(image.GetClippingPath("test"));
            Assert.Null(image.GetClippingPath("#1"));
        }
    }
}
