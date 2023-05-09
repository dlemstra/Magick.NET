// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColormapSizeProperty
    {
        [Fact]
        public void ShouldReturnTheSizeOfTheColormap()
        {
            using (var first = new MagickImage(Files.Builtin.Logo))
            {
                Assert.Equal(256, first.ColormapSize);
            }
        }

        [Fact]
        public void ShouldChangeTheSizeOfTheColormap()
        {
            using (var first = new MagickImage(Files.Builtin.Logo))
            {
                first.ColormapSize = 128;

                Assert.Equal(128, first.ColormapSize);
            }
        }
    }
}
