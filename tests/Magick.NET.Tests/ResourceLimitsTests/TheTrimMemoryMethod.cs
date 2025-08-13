// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

#pragma warning disable CA1416 // Validate platform compatibility

public partial class ResourceLimitsTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheTrimMemoryMethod
    {
        [Fact]
        public void ShouldThrowExceptionOnWindows()
        {
            Assert.SkipUnless(Runtime.IsWindows, "Will only throw exception on Windows.");

            Assert.Throws<NotSupportedException>(static () => ResourceLimits.TrimMemory());
        }

        [Fact]
        public void ShouldReturnTrueOrFalse()
        {
            Assert.SkipWhen(Runtime.IsWindows, "Will throw exception on Windows.");

            if (Runtime.IsWindows)
                return;

            var result = ResourceLimits.TrimMemory();
            if (result)
                Assert.True(result);
            else
                Assert.False(result);
        }
    }
}

#pragma warning restore CA1416 // Validate platform compatibility
