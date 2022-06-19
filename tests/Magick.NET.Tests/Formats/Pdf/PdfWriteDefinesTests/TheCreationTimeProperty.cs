// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfWriteDefinesTests
    {
        public class TheCreationTimeProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsSet()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfWriteDefines
                    {
                        CreationTime = new DateTime(1990, 1, 2, 3, 4, 5, DateTimeKind.Utc),
                    });

                    Assert.Equal("631249445", image.Settings.GetDefine(MagickFormat.Pdf, "create-epoch"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfWriteDefines
                    {
                        CreationTime = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "create-epoch"));
                }
            }
        }
    }
}
