// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text.RegularExpressions;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheFeaturesProperty
    {
        private static readonly Regex OpenMPVersionRegex = new(@"OpenMP\(\d+(?:\.\d+)*\)", RegexOptions.Compiled);

        [Fact]
        public void ContainsExpectedFeatures()
        {
            var expected = string.Empty;
            var features = OpenMPVersionRegex.Replace(MagickNET.Features, "OpenMP");

            if (Runtime.Is64Bit)
                expected += "Channel-masks(64-bit) ";

            expected += "Cipher ";
#if Q16HDRI
            expected += "HDRI ";
#endif
            if (Runtime.IsWindows)
                expected += "OpenCL ";
#if OPENMP
            expected += "OpenMP ";
#endif
#if DEBUG_TEST
            expected = "Debug " + expected;
#endif

            Assert.Equal(expected, features);
        }
    }
}
