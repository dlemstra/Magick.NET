// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheBenchmarkScoreProperty
    {
        [Fact]
        public void ShouldNotBeZero()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotEqual(0.0, device.BenchmarkScore);
            }
        }
    }
}

#endif
