// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class ThePreserveCicpProperty
    {
        [Theory]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        public void ShouldSetTheDefine(bool value, string expected)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                PreserveCicp = value,
            });

            Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Heic, "preserve-cicp"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                PreserveCicp = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-cicp"));
        }
    }
}
