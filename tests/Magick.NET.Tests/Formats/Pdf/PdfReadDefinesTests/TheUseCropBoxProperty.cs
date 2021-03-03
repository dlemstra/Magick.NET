// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        public class TheUseCropBoxProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsTrue()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        UseCropBox = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }

            [Fact]
            public void ShouldSetTheDefineWhenValueIsFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        UseCropBox = false,
                    });

                    Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        UseCropBox = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }
        }
    }
}
