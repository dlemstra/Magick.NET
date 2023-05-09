// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    public class TheHideAnnotationsProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsTrue()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new PdfReadDefines
                {
                    HideAnnotations = true,
                });

                Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "hide-annotations"));
            }
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsFalse()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new PdfReadDefines
                {
                    HideAnnotations = false,
                });

                Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "hide-annotations"));
            }
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsNotSet()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new PdfReadDefines
                {
                    HideAnnotations = null,
                });

                Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "hide-annotations"));
            }
        }
    }
}
