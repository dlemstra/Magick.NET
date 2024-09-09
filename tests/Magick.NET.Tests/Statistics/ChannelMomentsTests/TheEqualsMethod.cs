// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ChannelMomentsTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnCorrectValue()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var moments = image.Moments();
            var first = moments.GetChannel(PixelChannel.Red);
            moments = image.Moments();
            var second = moments.GetChannel(PixelChannel.Red);

            Assert.False(ReferenceEquals(first, second));
            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.True(first.Centroid == second.Centroid);
            Assert.True(first.Centroid.Equals(second.Centroid));
            Assert.True(first.Centroid.Equals((object)second.Centroid));
        }
    }
}
