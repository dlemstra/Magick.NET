// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheRightToLeftProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                RightToLeft = true,
            });

            Assert.Equal("right-to-left", image.Settings.GetDefine(MagickFormat.Pdf, "page-direction"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void ShouldNotSetTheDefine(bool? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                RightToLeft = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "page-direction"));
        }
    }
}
