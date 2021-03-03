// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheListLengthProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                Assert.Equal((ulong)long.MaxValue, ResourceLimits.ListLength);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    var listLength = ResourceLimits.ListLength;

                    ResourceLimits.ListLength = 32U;
                    Assert.Equal(32U, ResourceLimits.ListLength);
                    ResourceLimits.ListLength = listLength;
                });
            }
        }
    }
}
