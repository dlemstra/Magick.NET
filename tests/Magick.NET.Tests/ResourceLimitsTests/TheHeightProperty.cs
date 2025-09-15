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
    [Collection(nameof(IsolatedUnitTest))]
    public class TheHeightProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                var memoryLimit = Runtime.Is64Bit ? (ulong)long.MaxValue : int.MaxValue;
                var maxChannels = 64UL;

                Assert.Equal(memoryLimit / sizeof(QuantumType) / maxChannels, ResourceLimits.Height);
            });
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenChanged()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                var height = ResourceLimits.Height;

                ResourceLimits.Height = 100000U;
                Assert.Equal(100000U, ResourceLimits.Height);

                ResourceLimits.Height = height;
                Assert.Equal(height, ResourceLimits.Height);
            });
        }
    }
}
