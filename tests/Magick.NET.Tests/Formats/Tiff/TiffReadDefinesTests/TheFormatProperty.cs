// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnTiff()
        {
            var defines = new TiffReadDefines();
            Assert.Equal(MagickFormat.Tiff, defines.Format);
        }
    }
}
