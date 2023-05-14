// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheClampMethod
    {
        [Fact]
        public void ShouldClampThePixelsToTheQuantumRange()
        {
#if Q16HDRI
            using var image = new MagickImage("xc:cmyka(200%, 200%, 200%, 200%, 2.0)", 1, 1);
            image.Clamp();

            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(0, 0);

            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Cyan)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Magenta)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Yellow)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Black)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Alpha)));
#endif
        }

        [Fact]
        public void ShouldClampThePixelsToTheQuantumRangeForTheSpecifiedChannel()
        {
#if Q16HDRI
            var value = Quantum.Max * 2;
            using var image = new MagickImage("xc:cmyka(200%, 200%, 200%, 200%, 2.0)", 1, 1);
            image.Clamp(Channels.Black | Channels.Yellow);

            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(0, 0);

            Assert.Equal(value, pixel.GetChannel(image.ChannelOffset(PixelChannel.Cyan)));
            Assert.Equal(value, pixel.GetChannel(image.ChannelOffset(PixelChannel.Magenta)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Yellow)));
            Assert.Equal(Quantum.Max, pixel.GetChannel(image.ChannelOffset(PixelChannel.Black)));
            Assert.Equal(value, pixel.GetChannel(image.ChannelOffset(PixelChannel.Alpha)));
#endif
        }
    }
}
