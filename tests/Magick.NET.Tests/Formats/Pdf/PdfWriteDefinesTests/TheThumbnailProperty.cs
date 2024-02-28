// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PdfWriteDefinesTests
{
    public class TheThumbnailProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            using var image = new MagickImage(MagickColors.Magenta, 1, 1);
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Thumbnail = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Pdf, "thumbnail"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsNotSet()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfWriteDefines
            {
                Thumbnail = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "thumbnail"));
        }
    }
}
