// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        public class TheInterpolateProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsTrue()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        Interpolate = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "interpolate"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        Interpolate = false,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "interpolate"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        Interpolate = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "interpolate"));
                }
            }
        }
    }
}
