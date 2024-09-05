// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using NSubstitute;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TheSumSquaredDistanceMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCustomImplementationDoesNotHaveExpectedChannels()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var perceptualHash = Substitute.For<IPerceptualHash>();
            perceptualHash.GetChannel(PixelChannel.Blue).Returns((IChannelPerceptualHash)null);

            var exception = Assert.Throws<NotSupportedException>(() => phash.SumSquaredDistance(perceptualHash));
            Assert.Equal("The other perceptual hash should contain a red, green and blue channel.", exception.Message);
        }

        [Fact]
        public void ShouldReturnTheDifference()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();

            using var other = new MagickImage(Files.MagickNETIconPNG);
            other.HasAlpha = false;

            Assert.Equal(3U, other.ChannelCount);

            var otherPhash = other.PerceptualHash();

#if Q8
            OpenCLValue.Assert(391.07, 390.34, phash.SumSquaredDistance(otherPhash));
#elif Q16
            OpenCLValue.Assert(365.65, 365.36, phash.SumSquaredDistance(otherPhash));
#else
            OpenCLValue.Assert(356.02, 356.02, phash.SumSquaredDistance(otherPhash));
#endif
        }
    }
}
