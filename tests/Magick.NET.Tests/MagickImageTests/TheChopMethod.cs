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

            Assert.Throws<ArgumentNullException>("geometry", () => image.Chop(null!));
        }

        [Fact]
        public void ShouldChopTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.Chop(new MagickGeometry(new Percentage(50), new Percentage(50)));

            Assert.Equal(240U, image.Width);
            Assert.Equal(320U, image.Height);
        }
    }
}
