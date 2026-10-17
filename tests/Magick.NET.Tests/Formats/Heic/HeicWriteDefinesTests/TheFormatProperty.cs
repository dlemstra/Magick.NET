// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnHeic()
        {
            var defines = new HeicWriteDefines();
            Assert.Equal(MagickFormat.Heic, defines.Format);
        }
    }
}
