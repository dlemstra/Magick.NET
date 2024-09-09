// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSteganoMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWatermarkIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("watermark", () => image.Stegano(null!));
        }

        [Fact]
        public void ShouldAddDigitalWatermark()
        {
            using var message = new MagickImage("label:Magick.NET is the best!", 200, 20);

            using var image = new MagickImage(Files.Builtin.Wizard);
            image.Stegano(message);

            using var tempFile = new TemporaryFile(".png");
            image.Write(tempFile.File);

            var settings = new MagickReadSettings
            {
                Format = MagickFormat.Stegano,
                Width = 200,
                Height = 20,
            };
            using var hiddenMessage = new MagickImage(tempFile.File, settings);

            Assert.InRange(message.Compare(hiddenMessage, ErrorMetric.RootMeanSquared), 0, 0.001);
        }
    }
}
