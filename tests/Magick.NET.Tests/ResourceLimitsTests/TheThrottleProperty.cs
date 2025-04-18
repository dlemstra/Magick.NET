// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheThrottleProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(() =>
            {
                Assert.Equal(0U, ResourceLimits.Throttle);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(() =>
            {
                var throttle = ResourceLimits.Throttle;

                ResourceLimits.Throttle = 1U;
                Assert.Equal(1U, ResourceLimits.Throttle);

                ResourceLimits.Throttle = throttle;
                Assert.Equal(throttle, ResourceLimits.Throttle);
            });
        }
    }
}
