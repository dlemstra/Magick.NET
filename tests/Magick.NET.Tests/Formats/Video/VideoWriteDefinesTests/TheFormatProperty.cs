// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class VideoWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnVideo()
        {
            var defines = new VideoWriteDefines(MagickFormat.Mpg);
            Assert.Equal(MagickFormat.Mpg, defines.Format);
        }
    }
}
