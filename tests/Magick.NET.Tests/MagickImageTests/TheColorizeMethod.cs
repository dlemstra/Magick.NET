// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColorizeMethod
    {
        public class WithPercentage
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("color", () => image.Colorize(null, new Percentage(50)));
            }

            [Fact]
            public void ShouldColorizeTheImage()
            {
                using var image = new MagickImage(Files.Builtin.Wizard);
                image.Colorize(MagickColors.Purple, new Percentage(50));

                ColorAssert.Equal(new MagickColor("#c0408000c040"), image, 45, 75);
            }
        }

        public class WithSeparatePercentages
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("color", () => image.Colorize(null, new Percentage(25), new Percentage(50), new Percentage(75)));
            }

            [Fact]
            public void ShouldColorizeTheImage()
            {
                using var image = new MagickImage(Files.Builtin.Wizard);
                image.Colorize(MagickColors.Purple, new Percentage(25), new Percentage(50), new Percentage(75));

                ColorAssert.Equal(new MagickColor("#e01f8000a060"), image, 45, 75);
            }
        }
    }
}
