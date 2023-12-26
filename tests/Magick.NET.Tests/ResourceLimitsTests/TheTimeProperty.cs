// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheTimeProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            Assert.Equal(0U, ResourceLimits.Time);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            var time = ResourceLimits.Time;

            ResourceLimits.Time = 1U;
            Assert.Equal(1U, ResourceLimits.Time);
            ResourceLimits.Time = time;
        }
    }
}
