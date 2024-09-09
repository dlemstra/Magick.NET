// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSparseColorMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenArgsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("args", () => image.SparseColor(Channels.Red, SparseColorMethod.Barycentric, null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenArgsIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("args", () => image.SparseColor(Channels.Blue, SparseColorMethod.Barycentric, new SparseColorArg[] { }));
        }

        [Fact]
        public void ShouldThrowExceptionWhenInvalidChannelsAreSpecified()
        {
            using var image = new MagickImage();
            var args = new[] { new SparseColorArg(0, 0, MagickColors.SkyBlue) };

            var exception = Assert.Throws<ArgumentException>("channels", () => image.SparseColor(Channels.Black, SparseColorMethod.Barycentric, args));
            Assert.Contains("Invalid channels specified.", exception.Message);
        }

        [Fact]
        public void ShouldInterpolateTheColorsAtTheSpecifiedCoordinates()
        {
            var settings = new MagickReadSettings
            {
                Width = 600,
                Height = 60,
            };

            using var image = new MagickImage("xc:", settings);
            using var before = image.GetPixels();
            var color = before.GetPixel(0, 0).ToColor();
            Assert.NotNull(color);

            ColorAssert.Equal(color, before.GetPixel(599, 59).ToColor());

            var args = new[]
            {
                new SparseColorArg(0, 0, MagickColors.SkyBlue),
                new SparseColorArg(-600, 60, MagickColors.SkyBlue),
                new SparseColorArg(600, 60, MagickColors.Black),
            };

            image.SparseColor(SparseColorMethod.Barycentric, args);

            using var after = image.GetPixels();
            color = after.GetPixel(0, 0).ToColor();
            Assert.NotNull(color);

            ColorAssert.NotEqual(color, after.GetPixel(599, 59).ToColor());
        }
    }
}
