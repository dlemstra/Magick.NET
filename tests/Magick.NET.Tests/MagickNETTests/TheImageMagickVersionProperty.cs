// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.InteropServices;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public partial class TheImageMagickVersionProperty
    {
        [Fact]
        public void ShoudlReturnTheCorrectValue()
        {
            var version = "7.1.2-18";
            var architecture = Runtime.IsWindows
                ? Runtime.Is64Bit ? Runtime.Architecture == Architecture.Arm64 ? "arm64" : "x64" : "x86"
                : Runtime.Architecture == Architecture.Arm64 ? "aarch64" : "x86_64";
#if Q8
            var quantum = "Q8";
#elif Q16
            var quantum = "Q16";
#else
            var quantum = "Q16-HDRI";
#endif
            Assert.Equal($"ImageMagick {version} {quantum} {architecture} d4e4b2b35:20260322 https://imagemagick.org", MagickNET.ImageMagickVersion);
        }
    }
}
