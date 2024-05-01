// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheNoIdentifierProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                NoIdentifier = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "no-identifier"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenSetToFalse()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                NoIdentifier = false,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "no-identifier"));
        }
    }
}
