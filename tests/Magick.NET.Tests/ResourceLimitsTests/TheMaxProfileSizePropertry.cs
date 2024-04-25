// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheMaxProfileSizePropertry
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            var maxProfileSize = Runtime.Is64Bit ? (ulong)long.MaxValue : int.MaxValue;
            Assert.Equal(maxProfileSize, ResourceLimits.MaxProfileSize);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            var maxProfileSize = ResourceLimits.MaxProfileSize;

            ResourceLimits.MaxProfileSize = 42U;
            Assert.Equal(42U, ResourceLimits.MaxProfileSize);
            ResourceLimits.MaxProfileSize = maxProfileSize;
        }
    }
}
