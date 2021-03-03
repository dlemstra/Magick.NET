// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheFeaturesProperty
        {
            [Fact]
            public void ContainsExpectedFeatures()
            {
                var expected = "Cipher DPC ";
#if Q16HDRI
                expected += "HDRI ";
#endif
                if (OperatingSystem.IsWindows)
                    expected += "OpenCL ";
#if OPENMP
                if (OperatingSystem.IsWindows)
                    expected += "OpenMP(2.0) ";
                else
                    expected += "OpenMP(4.5) ";
#endif
#if DEBUG_TEST
                expected = "Debug " + expected;
#endif

                Assert.Equal(expected, MagickNET.Features);
            }
        }
    }
}
