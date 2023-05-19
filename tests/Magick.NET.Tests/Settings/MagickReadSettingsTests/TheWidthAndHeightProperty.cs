// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheWidthAndHeightProperty
    {
        [Fact]
        public void ShouldNotUseHeightWhenNotSet()
        {
            using var image = new MagickImage();
            var settings = new MagickReadSettings
            {
                Width = 10,
            };

            image.Read("xc:fuchsia", settings);

            Assert.Equal(10, image.Width);
            Assert.Equal(1, image.Height);
        }

        [Fact]
        public void ShouldNotUseWidthWhenNotSet()
        {
            using var image = new MagickImage();
            var settings = new MagickReadSettings
            {
                Width = null,
                Height = 20,
            };

            image.Read("xc:fuchsia", settings);

            Assert.Equal(1, image.Width);
            Assert.Equal(20, image.Height);
        }

        [Fact]
        public void ShouldUseWidthAndHeightWhenSpecified()
        {
            using var image = new MagickImage();
            var settings = new MagickReadSettings
            {
                Width = 30,
                Height = 40,
            };

            image.Read("xc:fuchsia", settings);

            Assert.Equal(30, image.Width);
            Assert.Equal(40, image.Height);
        }
    }
}
