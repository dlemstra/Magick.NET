// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheFxMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsNull()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentNullException>("expression", () => images.Fx(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentException>("expression", () => images.Fx(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsInvalid()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Purple, 1, 1),
            };

            Assert.Throws<MagickOptionErrorException>(() => images.Fx("foobar"));
        }

        [Fact]
        public void ShouldEvaluateTheExpression()
        {
            using var images = new MagickImageCollection();

            var logo = new MagickImage(Files.Builtin.Logo);
            images.Add(logo);

            var floppedLogo = logo.Clone();
            floppedLogo.Flop();
            images.Add(floppedLogo);

            using var result = images.Fx("(u+v)/2", Channels.Green);

            ColorAssert.Equal(new MagickColor("#ffff9f1fffff"), result, 250, 375);
            ColorAssert.Equal(new MagickColor("#22229f1f9292"), result, 375, 375);
        }
    }
}
