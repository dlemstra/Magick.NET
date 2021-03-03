// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheBackgroundColorProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsDisposed()
            {
                var image = new MagickImage();
                image.Dispose();

                Assert.Throws<ObjectDisposedException>(() =>
                {
                    image.BackgroundColor = MagickColors.PaleGreen;
                });
            }

            [Fact]
            public void ShouldSetTheBackgroundColorWhenReadingImage()
            {
                using (var image = new MagickImage())
                {
                    ColorAssert.Equal(MagickColors.White, image.Settings.BackgroundColor);

                    image.Read(Files.Logos.MagickNETSVG);
                    ColorAssert.Equal(MagickColors.White, image, 0, 0);

                    image.Settings.BackgroundColor = MagickColors.Yellow;
                    image.Read(Files.Logos.MagickNETSVG);
                    ColorAssert.Equal(MagickColors.Yellow, image, 0, 0);
                }
            }
        }
    }
}
