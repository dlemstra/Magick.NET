// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class ResourceLimitsTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheWidthProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            var memoryLimit = Runtime.Is64Bit ? (ulong)long.MaxValue : int.MaxValue;
            var maxChannels = 64UL;

            Assert.Equal(memoryLimit / sizeof(QuantumType) / maxChannels, ResourceLimits.Width);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            var width = ResourceLimits.Width;

            ResourceLimits.Width = 200000U;
            Assert.Equal(200000U, ResourceLimits.Width);
            ResourceLimits.Width = width;
        }
    }
}
