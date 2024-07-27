// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFillColorProperty
    {
        [Fact]
        public void ShouldDefaultToBlack()
        {
            using var image = new MagickImage();

            ColorAssert.Equal(MagickColors.Black, image.Settings.FillColor);
        }

        [Fact]
        public void CanBeSetToNull()
        {
            using var image = new MagickImage();
            image.Settings.FillColor = null;

            Assert.Null(image.Settings.FillColor);
        }

        [Fact]
        public void ShouldBeUseWhenDrawingCaption()
        {
            using var image = new MagickImage(MagickColors.Transparent, 100, 100);
            IPixel<QuantumType> pixelA;
            image.Settings.FillColor = MagickColors.Red;
            image.Read("caption:Magick.NET");

            Assert.Equal(100U, image.Width);
            Assert.Equal(100U, image.Height);

            using var pixelsA = image.GetPixels();
            pixelA = pixelsA.GetPixel(63, 6);

            ColorAssert.NotEqual(MagickColors.Transparent, pixelA.ToColor());

            IPixel<QuantumType> pixelB;
            image.Settings.FillColor = MagickColors.Yellow;
            image.Read("caption:Magick.NET");
            using var pixelsB = image.GetPixels();
            pixelB = pixelsB.GetPixel(63, 6);

            ColorAssert.NotEqual(MagickColors.Transparent, pixelB.ToColor());
            ColorAssert.NotEqual(pixelA.ToColor(), pixelB.ToColor());
        }
    }
}
