// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheMemoryProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                if (ResourceLimits.Memory < 100000000U)
                    throw new XunitException("Invalid memory limit: " + ResourceLimits.Memory);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    var oldMemory = ResourceLimits.Memory;
                    var newMemory = (ulong)(ResourceLimits.Memory * 0.9);

                    ResourceLimits.Memory = newMemory;
                    Assert.Equal(newMemory, ResourceLimits.Memory);
                    ResourceLimits.Memory = oldMemory;
                });
            }
        }
    }
}
