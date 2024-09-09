// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRegionMaskMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenArrayIsNull()
        {
            using var green = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => green.RegionMask(null!));
        }

        [Fact]
        public void ShouldBeUsedWhenCompositingAnImage()
        {
            using var red = new MagickImage("xc:red", 100, 100);
            using var green = new MagickImage("xc:green", 100, 100);
            green.RegionMask(new MagickGeometry(10, 10, 50, 50));

            green.Composite(red, CompositeOperator.SrcOver);

            ColorAssert.Equal(MagickColors.Green, green, 0, 0);
            ColorAssert.Equal(MagickColors.Red, green, 10, 10);
            ColorAssert.Equal(MagickColors.Green, green, 60, 60);

            green.RemoveRegionMask();

            green.Composite(red, CompositeOperator.SrcOver);

            ColorAssert.Equal(MagickColors.Red, green, 0, 0);
            ColorAssert.Equal(MagickColors.Red, green, 10, 10);
            ColorAssert.Equal(MagickColors.Red, green, 60, 60);
        }
    }
}
