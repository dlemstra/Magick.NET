// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class BmpWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldDefaultToBmp()
        {
            var defines = new BmpWriteDefines();

            Assert.Equal(MagickFormat.Bmp, defines.Format);
        }

        [Theory]
        [InlineData(MagickFormat.Bmp)]
        [InlineData(MagickFormat.Bmp2)]
        [InlineData(MagickFormat.Bmp3)]
        public void ShouldAllowBmpFormats(MagickFormat format)
        {
            var defines = new BmpWriteDefines
            {
                Format = format,
            };

            Assert.Equal(format, defines.Format);
        }

        [Theory]
        [InlineData(MagickFormat.Unknown)]
        [InlineData(MagickFormat.Png)]
        [InlineData((MagickFormat)int.MaxValue)]
        public void ShouldThrowExceptionWhenFormatIsInvalid(MagickFormat format)
        {
            var defines = new BmpWriteDefines();

            var exception = Assert.Throws<InvalidOperationException>(() => defines.Format = format);

            Assert.Equal("The format must be Bmp, Bmp2 or Bmp3.", exception.Message);
            Assert.Equal(MagickFormat.Bmp2, defines.Format);
        }
    }
}
