// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheDiskProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                Assert.Equal((ulong)long.MaxValue, ResourceLimits.Disk);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                var disk = ResourceLimits.Disk;

                ResourceLimits.Disk = 40000U;
                Assert.Equal(40000U, ResourceLimits.Disk);

                ResourceLimits.Disk = disk;
                Assert.Equal(disk, ResourceLimits.Disk);
            });
        }
    }
}
