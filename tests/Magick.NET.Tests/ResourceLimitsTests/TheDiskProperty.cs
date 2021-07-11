// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        [CollectionDefinition(nameof(RunTestsSeparately))]
        public class TheDiskProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                Assert.Equal((ulong)long.MaxValue, ResourceLimits.Disk);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                var disk = ResourceLimits.Disk;

                ResourceLimits.Disk = 40000U;
                Assert.Equal(40000U, ResourceLimits.Disk);
                ResourceLimits.Disk = disk;
            }
        }
    }
}
