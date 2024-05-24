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
            Assert.Equal(0, image.MetaChannelCount);
        }

        [Fact]
        public void ShouldChangeTheNumberOfMetaChannels()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            image.BackgroundColor = MagickColors.Purple;
            Assert.Equal(3, image.ChannelCount);
            Assert.Equal(0, image.MetaChannelCount);

            image.MetaChannelCount = 1;

            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(0, 0);
            var channel = pixels.GetIndex(PixelChannel.Meta0);
            pixel.SetChannel(channel, Quantum.Max);

            using var stream = new MemoryStream();
            image.Write(stream);
            stream.Position = 0;

            using var output = new MagickImage(stream);
            Assert.Equal(4, output.ChannelCount);
        }
    }
}
