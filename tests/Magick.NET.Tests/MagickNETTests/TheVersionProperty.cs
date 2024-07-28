// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public partial class TheVersionProperty
    {
        [Fact]
        public void ShouldContainTheCorrectPlatform()
        {
#if PLATFORM_AnyCPU
            Assert.Contains("AnyCPU", MagickNET.Version);
#elif PLATFORM_x64
            Assert.Contains("x64", MagickNET.Version);
#elif PLATFORM_arm64
            Assert.Contains("arm64", MagickNET.Version);
#else
            Assert.Contains("x86", MagickNET.Version);
#endif
        }

        [Fact]
        public void ShouldContainCorrectQuantum()
        {
#if Q8
            Assert.Contains("Q8", MagickNET.Version);
#elif Q16
            Assert.Contains("Q16", MagickNET.Version);
#else
            Assert.Contains("Q16-HDRI", MagickNET.Version);
#endif
        }

        [Fact]
        public void ShouldContainTheCorrectFramework()
        {
#if NET462
            Assert.Contains("netstandard20", MagickNET.Version);
#else
            Assert.Contains("net8.0", MagickNET.Version);
#endif
        }
    }
}
