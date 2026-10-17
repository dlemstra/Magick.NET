// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    public class TheStopOnErrorProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenTrue()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfReadDefines
            {
                StopOnError = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "stop-on-error"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void ShouldNotSetTheDefineWhenFalseOrNull(bool? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfReadDefines
            {
                StopOnError = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "stop-on-error"));
        }
    }
}
