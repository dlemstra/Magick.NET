// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DngReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnDng()
        {
            var defines = new DngReadDefines();
            Assert.Equal(MagickFormat.Dng, defines.Format);
        }
    }
}
