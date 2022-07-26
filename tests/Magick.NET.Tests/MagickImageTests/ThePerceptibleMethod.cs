// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class ThePerceptibleMethod
        {
            [Fact]
            public void ShouldChangeTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Perceptible(Quantum.Max * 0.4);

                    ColorAssert.Equal(new MagickColor("#f79868"), image, 300, 210);
                    ColorAssert.Equal(new MagickColor("#666692"), image, 410, 405);
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultChannels()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Perceptible(Quantum.Max * 0.4, Channels.Composite);

                    ColorAssert.Equal(new MagickColor("#f79868"), image, 300, 210);
                    ColorAssert.Equal(new MagickColor("#666692"), image, 410, 405);
                }
            }

            [Fact]
            public void ShouldChangeTheSpecifiedChannels()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Perceptible(Quantum.Max * 0.4, Channels.Green);

                    ColorAssert.Equal(new MagickColor("#f79868"), image, 300, 210);
                    ColorAssert.Equal(new MagickColor("#226692"), image, 410, 405);
                }
            }

            [Fact]
            public void ShouldChangeTheSpecifiedChannelsWithTrueColorAlphaImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.ColorType = ColorType.TrueColorAlpha;

                    image.Perceptible(Quantum.Max * 0.4, Channels.Green);

                    ColorAssert.Equal(new MagickColor("#f79868"), image, 300, 210);
                    ColorAssert.Equal(new MagickColor("#226692"), image, 410, 405);
                }
            }
        }
    }
}
