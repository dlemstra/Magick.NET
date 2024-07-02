// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class ThePreserveiCCPProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                PreserveiCCP = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenSetToFalse()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                PreserveiCCP = false,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
        }
    }
}
