// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFormatExpressionMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("expression", () => image.FormatExpression(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenExpressionIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("expression", () => image.FormatExpression(string.Empty));
        }

        [Fact]
        public void ShouldReturnProfiles()
        {
            using var image = new MagickImage(Files.InvitationTIF);

            Assert.Equal("sRGB IEC61966-2.1", image.FormatExpression("%[profile:icc]"));
        }

        [Fact]
        public void ShouldReturnSignature()
        {
            using var image = new MagickImage(Files.RedPNG);

            Assert.Equal("92f59c51ad61b99b3c9ebd51f1c77b9c80c0478e2fdb7db47831376b1e4a00db", image.FormatExpression("%#"));
        }

        [Fact]
        public void ShouldRaiseWarningForInvalidExpression()
        {
            var count = 0;
            EventHandler<WarningEventArgs> warningDelegate = (sender, arguments) =>
            {
                Assert.NotNull(sender);
                Assert.NotNull(arguments);
                Assert.NotNull(arguments.Message);
                Assert.NotEmpty(arguments.Message);
                Assert.NotNull(arguments.Exception);

                count++;
            };

            using var image = new MagickImage(Files.RedPNG);
            image.Warning += warningDelegate;
            var result = image.FormatExpression("%EOO");
            image.Warning -= warningDelegate;

            Assert.Equal("OO", result);
            Assert.Equal(1, count);
        }
    }
}
