// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnHeic()
        {
            var defines = new HeicReadDefines();
            Assert.Equal(MagickFormat.Heic, defines.Format);
        }
    }
}
