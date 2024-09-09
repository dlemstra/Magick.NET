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

            var cyanOffset = image.ChannelOffset(PixelChannel.Cyan);
            Assert.NotNull(cyanOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(cyanOffset.Value));

            var magentaOffset = image.ChannelOffset(PixelChannel.Magenta);
            Assert.NotNull(magentaOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(magentaOffset.Value));

            var yellowOffset = image.ChannelOffset(PixelChannel.Yellow);
            Assert.NotNull(yellowOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(yellowOffset.Value));

            var blackOffset = image.ChannelOffset(PixelChannel.Black);
            Assert.NotNull(blackOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(blackOffset.Value));

            var alphaOffset = image.ChannelOffset(PixelChannel.Alpha);
            Assert.NotNull(alphaOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(alphaOffset.Value));
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

            var cyanOffset = image.ChannelOffset(PixelChannel.Cyan);
            Assert.NotNull(cyanOffset);
            Assert.Equal(value, pixel.GetChannel(cyanOffset.Value));

            var magentaOffset = image.ChannelOffset(PixelChannel.Magenta);
            Assert.NotNull(magentaOffset);
            Assert.Equal(value, pixel.GetChannel(magentaOffset.Value));

            var yellowOffset = image.ChannelOffset(PixelChannel.Yellow);
            Assert.NotNull(yellowOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(yellowOffset.Value));

            var blackOffset = image.ChannelOffset(PixelChannel.Black);
            Assert.NotNull(blackOffset);
            Assert.Equal(Quantum.Max, pixel.GetChannel(blackOffset.Value));

            var alphaOffset = image.ChannelOffset(PixelChannel.Alpha);
            Assert.NotNull(alphaOffset);
            Assert.Equal(value, pixel.GetChannel(alphaOffset.Value));
#endif
        }
    }
}
