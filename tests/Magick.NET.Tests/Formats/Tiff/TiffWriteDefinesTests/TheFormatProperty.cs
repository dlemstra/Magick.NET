// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnTiff()
        {
            var defines = new TiffWriteDefines();
            Assert.Equal(MagickFormat.Tiff, defines.Format);
        }
    }
}
