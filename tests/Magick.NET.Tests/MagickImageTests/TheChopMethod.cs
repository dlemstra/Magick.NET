// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChopMethod
    {
        [Fact]
        public void ShouldRaiseExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => image.Chop(null));
        }

        [Fact]
        public void ShouldChopTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.Chop(new MagickGeometry(new Percentage(50), new Percentage(50)));

            Assert.Equal(240, image.Width);
            Assert.Equal(320, image.Height);
        }
    }

    public class TheChopHorizontalMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            Assert.Throws<ArgumentException>("width", () => image.ChopHorizontal(-1, -1));
        }
    }

    public class TheChopVerticalMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            Assert.Throws<ArgumentException>("height", () => image.ChopVertical(-1, -1));
        }
    }
}
