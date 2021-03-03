// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ThresholdTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheMinimumAndMaximumToZeroByDefault()
            {
                Threshold point = default;
                Assert.Equal(0.0, point.Minimum);
                Assert.Equal(0.0, point.Maximum);
            }

            [Fact]
            public void ShouldSetTheMinimumAndMaximumValue()
            {
                var point = new Threshold(5, 10);
                Assert.Equal(5.0, point.Minimum);
                Assert.Equal(10.0, point.Maximum);
            }

            [Fact]
            public void ShouldUseTheMinimumValueWhenTValueIsNotSet()
            {
                var point = new Threshold(5);
                Assert.Equal(5.0, point.Minimum);
                Assert.Equal(0.0, point.Maximum);
            }
        }
    }
}
