// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PsdWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnPsd()
        {
            var defines = new PsdWriteDefines();
            Assert.Equal(MagickFormat.Psd, defines.Format);
        }
    }
}
