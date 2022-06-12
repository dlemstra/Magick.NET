// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheContrastMethod
        {
            [Fact]
            public void ShouldEnhanceTheImage()
            {
                using (var first = new MagickImage(Files.Builtin.Wizard))
                {
                    first.Contrast();

                    using (var second = new MagickImage(Files.Builtin.Wizard))
                    {
                        Assert.InRange(first.Compare(second, ErrorMetric.RootMeanSquared), 0.0174, 0.0175);
                    }
                }
            }
        }
    }
}
