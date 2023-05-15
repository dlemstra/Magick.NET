// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMotionBlurMethod
    {
        [Fact]
        public void ShouldMotionBlurTheImage()
        {
            using var motionBlurred = new MagickImage(Files.Builtin.Logo);
            motionBlurred.MotionBlur(4.0, 5.4, 10.6);

            using var original = new MagickImage(Files.Builtin.Logo);

            Assert.InRange(motionBlurred.Compare(original, ErrorMetric.RootMeanSquared), 0.11019, 0.11020);
        }
    }
}
