// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Reflection;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorsTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldContainTheCorrectNumberOfColors()
        {
            var colorCount = typeof(MagickColors)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(property => property.PropertyType == typeof(MagickColor))
                .Count();

            Assert.Equal(142, colorCount);
        }
    }
}
