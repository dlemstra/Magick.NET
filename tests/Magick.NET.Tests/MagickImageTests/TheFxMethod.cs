// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFxMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsNull()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<ArgumentNullException>("expression", () => image.Fx(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsEmpty()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<ArgumentException>("expression", () => image.Fx(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsInvalid()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<MagickOptionErrorException>(() => image.Fx("foobar"));
        }

        [Fact]
        public void ShouldEvaluateTheExpression()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Fx("b");

            ColorAssert.Equal(MagickColors.Black, image, 183, 83);
            ColorAssert.Equal(MagickColors.White, image, 140, 400);

            image.Fx("1/2", Channels.Green);

            ColorAssert.Equal(new MagickColor("#000080000000"), image, 183, 83);
            ColorAssert.Equal(new MagickColor("#ffff8000ffff"), image, 140, 400);

            image.Fx("1/4", Channels.Alpha);

            ColorAssert.Equal(new MagickColor("#000080000000"), image, 183, 83);
            ColorAssert.Equal(new MagickColor("#ffff8000ffff"), image, 140, 400);

            image.HasAlpha = true;
            image.Fx("1/4", Channels.Alpha);

            ColorAssert.Equal(new MagickColor("#0000800000004000"), image, 183, 83);
            ColorAssert.Equal(new MagickColor("#ffff8000ffff4000"), image, 140, 400);
        }

        [Fact]
        public void ShouldEvaluateExpressionMethod()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Fx("rand()");
        }
    }
}
