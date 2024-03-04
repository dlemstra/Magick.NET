// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheVersionProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            using var image = new MagickImage(MagickColors.Magenta, 1, 1);
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Version = 1.42,
            });

            Assert.Equal("1.4", image.Settings.GetDefine(MagickFormat.Pdf, "version"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsNotSet()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Version = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "version"));
        }
    }
}
