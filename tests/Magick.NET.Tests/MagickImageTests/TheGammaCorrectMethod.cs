// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGammaCorrectMethod
    {
        [Fact]
        public void ShouldGammaCorrectTheImage()
        {
            using var original = new MagickImage(Files.InvitationTIF);
            using var image = original.Clone();
            image.GammaCorrect(2.0);

            var difference = image.Compare(original, ErrorMetric.RootMeanSquared);
            Assert.InRange(difference, 0.07, 0.071);
        }

        [Fact]
        public void ShouldGammaCorrectTheSpecifiedChannel()
        {
            using var original = new MagickImage(Files.InvitationTIF);
            using var image = original.Clone();
            image.GammaCorrect(2.0, Channels.Red);

            var difference = image.Compare(original, ErrorMetric.RootMeanSquared);
            Assert.InRange(difference, 0.0360, 0.0362);
        }
    }
}
