// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnPng()
        {
            var defines = new PngReadDefines();
            Assert.Equal(MagickFormat.Png, defines.Format);
        }
    }
}
