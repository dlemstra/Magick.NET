// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TestPerceptualHash : IPerceptualHash
    {
        public IChannelPerceptualHash GetChannel(PixelChannel channel)
            => null;

        public double SumSquaredDistance(IPerceptualHash other)
        {
            throw new System.NotImplementedException();
        }
    }

    public class TheSumSquaredDistanceMethod
    {
        [Fact]
        public void ShouldThrowNotSupportedExceptionIfCustomImplementationDoesNotHaveExpectedChannels()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var exception = Assert.Throws<NotSupportedException>(() => phash.SumSquaredDistance(new TestPerceptualHash()));
            Assert.Equal("other IPerceptualHash must have Red, Green and Blue channel", exception.Message);
        }

        [Fact]
        public void ShouldReturnTheDifference()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();

            using var other = new MagickImage(Files.MagickNETIconPNG);
            other.HasAlpha = false;

            Assert.Equal(3, other.ChannelCount);

            var otherPhash = other.PerceptualHash();

#if Q8
            OpenCLValue.Assert(394.74, 394.90, phash.SumSquaredDistance(otherPhash), 0.01);
#elif Q16
            OpenCLValue.Assert(395.35, 395.39, phash.SumSquaredDistance(otherPhash), 0.02);
#else
            OpenCLValue.Assert(395.60, 395.68, phash.SumSquaredDistance(otherPhash), 0.02);
#endif
        }
    }
}
