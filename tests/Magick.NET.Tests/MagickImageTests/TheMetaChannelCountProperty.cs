// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMetaChannelCountProperty
    {
        [Fact]
        public void ShouldReturnZeroForImagesWithoutMetaChannel()
        {
            using var image = new MagickImage(Files.RoseSparkleGIF);
            Assert.Equal(0U, image.MetaChannelCount);
        }

        [Fact]
        public void ShouldChangeTheNumberOfMetaChannels()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            image.BackgroundColor = MagickColors.Purple;
            Assert.Equal(3U, image.ChannelCount);
            Assert.Equal(0U, image.MetaChannelCount);

            image.MetaChannelCount = 1;

            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(0, 0);
            var channel = pixels.GetChannelIndex(PixelChannel.Meta0);
            Assert.NotNull(channel);

            pixel.SetChannel((uint)channel, Quantum.Max);

            using var stream = new MemoryStream();
            image.Write(stream);
            stream.Position = 0;

            using var output = new MagickImage(stream);
            Assert.Equal(4U, output.ChannelCount);

            using var outputPixels = image.GetPixelsUnsafe();
            pixel = outputPixels.GetPixel(0, 0);
            channel = outputPixels.GetChannelIndex(PixelChannel.Meta0);
            Assert.NotNull(channel);
            Assert.Equal(Quantum.Max, pixel.GetChannel(channel.Value));
        }
    }
}
