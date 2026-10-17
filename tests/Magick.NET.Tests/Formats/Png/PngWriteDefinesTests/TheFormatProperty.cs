// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldDefaultToPng()
        {
            var defines = new PngWriteDefines();

            Assert.Equal(MagickFormat.Png, defines.Format);
        }

        [Theory]
        [InlineData(MagickFormat.Png)]
        [InlineData(MagickFormat.Png00)]
        [InlineData(MagickFormat.Png8)]
        [InlineData(MagickFormat.Png24)]
        [InlineData(MagickFormat.Png32)]
        [InlineData(MagickFormat.Png48)]
        [InlineData(MagickFormat.Png64)]
        public void ShouldAllowPngFormats(MagickFormat format)
        {
            var defines = new PngWriteDefines
            {
                Format = MagickFormat.Png64,
            };

            defines.Format = format;

            Assert.Equal(format, defines.Format);
        }

        [Theory]
        [InlineData(MagickFormat.Unknown)]
        [InlineData(MagickFormat.Bmp)]
        [InlineData((MagickFormat)int.MaxValue)]
        public void ShouldThrowExceptionWhenFormatIsInvalid(MagickFormat format)
        {
            var defines = new PngWriteDefines
            {
                Format = MagickFormat.Png8,
            };

            var exception = Assert.Throws<InvalidOperationException>(() => defines.Format = format);

            Assert.Equal("The format must be Png, Png00, Png8, Png24, Png32, Png48 or Png64.", exception.Message);
            Assert.Equal(MagickFormat.Png8, defines.Format);
        }

        [Theory]
        [InlineData(MagickFormat.Png)]
        [InlineData(MagickFormat.Png00)]
        [InlineData(MagickFormat.Png8)]
        [InlineData(MagickFormat.Png24)]
        [InlineData(MagickFormat.Png32)]
        [InlineData(MagickFormat.Png48)]
        [InlineData(MagickFormat.Png64)]
        public void ShouldSetTheDefineForTheSpecifiedFormat(MagickFormat format)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                Format = format,
                BitDepth = 8,
            });

            Assert.Equal("8", image.Settings.GetDefine(format, "bit-depth"));
        }
    }
}
