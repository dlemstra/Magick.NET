// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
    }
}
