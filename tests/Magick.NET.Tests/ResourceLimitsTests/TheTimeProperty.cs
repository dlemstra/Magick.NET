// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheTimeProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(() =>
            {
                Assert.Equal((ulong)long.MaxValue, ResourceLimits.Time);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(() =>
            {
                var time = ResourceLimits.Time;

                ResourceLimits.Time = 1U;
                Assert.Equal(1U, ResourceLimits.Time);

                ResourceLimits.Time = time;
                Assert.Equal(time, ResourceLimits.Time);
            });
        }
    }
}
