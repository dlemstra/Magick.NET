// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSegmentMethod
    {
        [Fact]
        public void ShouldSegmentTheImage()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                image.Segment();

                ColorAssert.Equal(new MagickColor("#008300"), image, 77, 30);
                ColorAssert.Equal(new MagickColor("#f9f9f9"), image, 79, 30);
                ColorAssert.Equal(new MagickColor("#00c2fe"), image, 128, 62);
            }
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValues()
        {
            using (var image = new MagickImage(Files.TestPNG))
            {
                using (var other = new MagickImage(Files.TestPNG))
                {
                    image.Segment();
                    other.Segment(ColorSpace.Undefined, 1.0, 1.5);

                    var distortion = image.Compare(other, ErrorMetric.RootMeanSquared);
                    Assert.Equal(0.0, distortion);
                }
            }
        }
    }
}
