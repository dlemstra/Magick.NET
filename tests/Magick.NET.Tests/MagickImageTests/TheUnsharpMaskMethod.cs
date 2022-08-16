// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheUnsharpMaskMethod
        {
            [Fact]
            public void ShouldSharpenTheImage()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var original = image.Clone())
                    {
                        image.UnsharpMask(7.0, 3.0);

#if Q8 || Q16
                        Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.06476, 0.06478);
#else
                        Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.10234, 0.10235);
#endif
                    }
                }
            }

            [Fact]
            public void ShouldChangeTheSpecifiedChannels()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var original = image.Clone())
                    {
                        image.UnsharpMask(7.0, 3.0, Channels.Green);

                        Assert.Equal(0, original.Compare(image, ErrorMetric.RootMeanSquared));
                    }
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        image.UnsharpMask(7.0, 3.0);
                        other.UnsharpMask(7.0, 3.0, 1.0, 0.05, Channels.Composite);

                        Assert.Equal(0, other.Compare(image, ErrorMetric.RootMeanSquared));
                    }
                }
            }
        }
    }
}
