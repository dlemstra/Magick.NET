// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMomentsMethod
    {
        [Fact]
        public void ShouldReturnMomentsOfTheImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var moments = image.Moments();

            Assert.NotNull(moments);

            var redChannel = moments.GetChannel(PixelChannel.Red);

            Assert.NotNull(redChannel);

            Assert.Equal(PixelChannel.Red, redChannel.Channel);
            Assert.InRange(redChannel.Centroid.X, 56.59, 56.60);
            Assert.InRange(redChannel.Centroid.Y, 56.00, 56.01);
            Assert.InRange(redChannel.EllipseAngle, 148.92, 148.93);
            Assert.InRange(redChannel.EllipseAxis.X, 73.53, 73.54);
            Assert.InRange(redChannel.EllipseAxis.Y, 66.82, 66.83);
            Assert.InRange(redChannel.EllipseEccentricity, 0.41, 0.42);
            Assert.InRange(redChannel.EllipseIntensity, 0.79, 0.80);

            var expected = new double[] { 0.2004, 0.0003, 0.0001, 0.0, 0.0, 0.0, 0.0, 0.0 };
            for (var i = 0; i < 8; i++)
            {
                Assert.InRange(redChannel.HuInvariants(i), expected[i] - 0.0001, expected[i] + 0.0001);
            }
        }
    }
}
