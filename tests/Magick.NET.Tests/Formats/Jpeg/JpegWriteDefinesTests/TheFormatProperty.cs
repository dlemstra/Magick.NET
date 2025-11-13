// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnJpeg()
        {
            var defines = new JpegWriteDefines();
            Assert.Equal(MagickFormat.Jpeg, defines.Format);
        }
    }
}
