// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    public class TheFormatProperty
    {
        [Fact]
        public void ShouldReturnPdf()
        {
            var defines = new PdfReadDefines();
            Assert.Equal(MagickFormat.Pdf, defines.Format);
        }
    }
}
