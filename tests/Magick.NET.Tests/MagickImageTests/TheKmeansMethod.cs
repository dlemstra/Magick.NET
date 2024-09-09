// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheKmeansMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Kmeans(null!));
        }

        [Fact]
        public void ShouldReduceTheNumberOfColors()
        {
            var settings = new KmeansSettings
            {
                NumberColors = 5,
            };

            using var image = new MagickImage(Files.Builtin.Logo);
            image.Kmeans(settings);

            ColorAssert.Equal(new MagickColor("#f0fb6f8c3098"), image, 430, 225);
        }
    }
}
