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

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheWidthProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                if (OperatingSystem.Is64Bit)
                {
                    Assert.Equal(1844674407370955161U / sizeof(QuantumType), ResourceLimits.Width);
                }
                else
                {
                    Assert.Equal(429496729U / sizeof(QuantumType), ResourceLimits.Width);
                }
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    var width = ResourceLimits.Width;

                    ResourceLimits.Width = 200000U;
                    Assert.Equal(200000U, ResourceLimits.Width);
                    ResourceLimits.Width = width;
                });
            }
        }
    }
}
