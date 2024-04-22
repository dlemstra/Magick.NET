// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheMaxMemoryRequestProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            if (ResourceLimits.MaxMemoryRequest < 100000000U)
                throw new XunitException("Invalid memory limit: " + ResourceLimits.MaxMemoryRequest);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            var oldMemory = ResourceLimits.MaxMemoryRequest;
            var newMemory = (ulong)(ResourceLimits.MaxMemoryRequest * 0.8);

            ResourceLimits.MaxMemoryRequest = newMemory;
            Assert.Equal(newMemory, ResourceLimits.MaxMemoryRequest);
            ResourceLimits.MaxMemoryRequest = oldMemory;
        }
    }
}
