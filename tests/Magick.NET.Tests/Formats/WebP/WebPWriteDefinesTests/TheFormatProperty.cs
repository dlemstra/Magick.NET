// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class WebPWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnVideo()
        {
            var defines = new WebPWriteDefines();
            Assert.Equal(MagickFormat.WebP, defines.Format);
        }
    }
}
