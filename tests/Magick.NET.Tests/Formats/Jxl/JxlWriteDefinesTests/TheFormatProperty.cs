// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JxlWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnJxl()
        {
            var defines = new JxlWriteDefines();
            Assert.Equal(MagickFormat.Jxl, defines.Format);
        }
    }
}
