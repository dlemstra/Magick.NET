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

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheFillColorProperty
        {
            [Fact]
            public void ShouldHaveBlackAsTheDefaultColor()
            {
                using (var image = new MagickImage())
                {
                    ColorAssert.Equal(MagickColors.Black, image.Settings.FillColor);
                }
            }

            [Fact]
            public void CanBeSetToNull()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.FillColor = null;

                    Assert.Null(image.Settings.FillColor);
                }
            }

            [Fact]
            public void ShouldBeUseWhenDrawingCaption()
            {
                using (var image = new MagickImage(MagickColors.Transparent, 100, 100))
                {
                    IPixel<QuantumType> pixelA;
                    image.Settings.FillColor = MagickColors.Red;
                    image.Read("caption:Magick.NET");

                    Assert.Equal(100, image.Width);
                    Assert.Equal(100, image.Height);

                    using (var pixels = image.GetPixels())
                    {
                        pixelA = pixels.GetPixel(64, 6);
                    }

                    IPixel<QuantumType> pixelB;
                    image.Settings.FillColor = MagickColors.Yellow;
                    image.Read("caption:Magick.NET");
                    using (var pixels = image.GetPixels())
                    {
                        pixelB = pixels.GetPixel(64, 6);
                    }

                    ColorAssert.NotEqual(pixelA.ToColor(), pixelB.ToColor());
                }
            }
        }
    }
}
