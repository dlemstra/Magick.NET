// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheAreaProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(() =>
            {
                if (ResourceLimits.Area < 100000000U)
                    throw new XunitException("Invalid memory limit: " + ResourceLimits.Area);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(() =>
            {
                var area = ResourceLimits.Area;

                ResourceLimits.Area = 10000000U;
                Assert.Equal(10000000U, ResourceLimits.Area);
                ResourceLimits.Area = area;
            });
        }
    }
}
