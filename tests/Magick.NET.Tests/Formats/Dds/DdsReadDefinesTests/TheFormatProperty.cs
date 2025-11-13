// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DdsReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnDds()
        {
            var defines = new DdsReadDefines();
            Assert.Equal(MagickFormat.Dds, defines.Format);
        }
    }
}
