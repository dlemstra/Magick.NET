// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheTitleProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            using var image = new MagickImage(MagickColors.Magenta, 1, 1);
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Title = "magick",
            });

            Assert.Equal("magick", image.Settings.GetDefine(MagickFormat.Pdf, "title"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotSetTheDefineWhenValueIsNullOrEmpty(string? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Title = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "title"));
        }
    }
}
