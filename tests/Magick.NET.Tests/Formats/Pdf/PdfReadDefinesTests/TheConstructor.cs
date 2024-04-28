// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfReadDefines());

            Assert.Null(image.Settings.GetDefine("authenticate"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "hide-annotations"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "interpolate"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "no-identifier"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "use-trimbox"));
        }
    }
}
