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
        public class TheFontNamesProperty
        {
            [Fact]
            public void ContainsArial()
            {
                var fontNames = MagickNET.FontNames.ToArray();
                var fontName = fontNames.FirstOrDefault(f => f == "Arial");
                if (fontName == null)
                    throw new XunitException($"Unable to find Arial in font families: {string.Join(",", fontNames)}");
            }
        }
    }
}
