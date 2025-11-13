// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class Jp2ReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnJp2()
        {
            var defines = new Jp2ReadDefines();
            Assert.Equal(MagickFormat.Jp2, defines.Format);
        }
    }
}
