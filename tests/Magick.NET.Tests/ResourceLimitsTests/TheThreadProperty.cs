// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheThreadProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(() =>
            {
                if (ResourceLimits.Thread < 1U)
                    throw new XunitException("Invalid thread limit: " + ResourceLimits.Thread);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(() =>
            {
#if OPENMP
                var thread = ResourceLimits.Thread;

                Assert.NotEqual(1U, ResourceLimits.Thread);

                ResourceLimits.Thread = 1U;
                Assert.Equal(1U, ResourceLimits.Thread);

                ResourceLimits.Thread = thread;
                Assert.Equal(thread, ResourceLimits.Thread);
#else
                Assert.Equal(1U, ResourceLimits.Thread);
                ResourceLimits.Thread = 2U;

                Assert.Equal(1U, ResourceLimits.Thread);
#endif
            });
        }
    }
}
