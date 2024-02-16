// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ThresholdTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnSingleValueWhenOnlyMinimumIsSet()
        {
            var point = new Threshold(1.2);
            Assert.Equal("1.2", point.ToString());
        }

        [Fact]
        public void ShouldReturnValueWithMinimumAndMaximum()
        {
            var point = new Threshold(1.2, 3.4);
            Assert.Equal("1.2-3.4", point.ToString());
        }
    }
}
