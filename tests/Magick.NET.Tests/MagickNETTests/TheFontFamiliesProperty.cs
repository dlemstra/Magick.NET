// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheFontFamiliesProperty
        {
            [Fact]
            public void ContainsArial()
            {
                var fontFamilies = MagickNET.FontFamilies.ToArray();
                var fontFamily = fontFamilies.FirstOrDefault(f => f == "Arial");
                if (fontFamily == null)
                    throw new XunitException($"Unable to find Arial in font families: {string.Join(",", fontFamilies)}");
            }

            [Fact]
            public void ContainsNoDuplicates()
            {
                var fontFamilies = MagickNET.FontFamilies.ToArray();
                Assert.Equal(fontFamilies.Count(), fontFamilies.Distinct().Count());
            }
        }
    }
}
