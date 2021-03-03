// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        public class TheFitPageProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsSet()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        FitPage = new MagickGeometry(1, 2, 3, 4),
                    });

                    Assert.Equal("3x4+1+2", image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        FitPage = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
                }
            }

            [Fact]
            public void ShouldLimitTheDimensions()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PdfReadDefines
                    {
                        FitPage = new MagickGeometry(50, 40),
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.CartoonNetworkStudiosLogoAI, settings);

                    Assert.True(image.Width <= 50);
                    Assert.True(image.Height <= 40);
                }
            }
        }
    }
}

#endif