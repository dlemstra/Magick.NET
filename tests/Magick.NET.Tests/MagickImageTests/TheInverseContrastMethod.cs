// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInverseContrastMethod
    {
        [Fact]
        public void ShouldInvertContrast()
        {
            using (var first = new MagickImage(Files.Builtin.Wizard))
            {
                first.Contrast();
                first.InverseContrast();

                using (var second = new MagickImage(Files.Builtin.Wizard))
                {
                    Assert.InRange(first.Compare(second, ErrorMetric.RootMeanSquared), 0.0031, 0.0034);
                }
            }
        }
    }
}
