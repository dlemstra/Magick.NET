// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "author"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "create-epoch"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "creator"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "keywords"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "modify-epoch"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "no-identifier"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "producer"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "subject"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "thumbnail"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "title"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "version"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFormatIsInvalid()
        {
            var exception = Assert.Throws<ArgumentException>("format", () => new PdfWriteDefines(MagickFormat.Png));
            Assert.Contains("The specified format is not a pdf format.", exception.Message);
        }

        [Theory]
        [InlineData(MagickFormat.Pdf)]
        [InlineData(MagickFormat.Pdfa)]
        public void ShouldAllowSpecifyingPdfFormats(MagickFormat format)
        {
            var defines = new PdfWriteDefines(format);
            Assert.Equal(format, defines.Format);
        }

        [Fact]
        public void ShouldDefaultToPdf()
        {
            var defines = new PdfWriteDefines();
            Assert.Equal(MagickFormat.Pdf, defines.Format);
        }
    }
}
