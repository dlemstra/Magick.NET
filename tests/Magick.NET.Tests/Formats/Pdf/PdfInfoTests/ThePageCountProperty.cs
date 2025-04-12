// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Formats;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class PdfInfoTests
{
    public class ThePageCountProperty
    {
        [Fact]
        public void ShouldReturnTheNumberOfPages()
        {
            if (!Ghostscript.IsAvailable)
                throw SkipException.ForSkip("Ghostscript is not available");

            var pdfInfo = PdfInfo.Create(Files.Coders.SamplePDF);
            Assert.Equal(2U, pdfInfo.PageCount);
        }

        [Fact]
        public void ShouldReturnTheNumberOfPagesForPasswordProtectedFile()
        {
            if (!Ghostscript.IsAvailable)
                throw SkipException.ForSkip("Ghostscript is not available");

            var pdfInfo = PdfInfo.Create(Files.Coders.PdfExamplePasswordOriginalPDF, "test");
            Assert.Equal(4U, pdfInfo.PageCount);
        }
    }
}
