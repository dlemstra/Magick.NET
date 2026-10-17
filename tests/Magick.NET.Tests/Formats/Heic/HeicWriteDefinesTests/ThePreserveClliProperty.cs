// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class ThePreserveClliProperty
    {
        [Theory]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        public void ShouldSetTheDefine(bool value, string expected)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                PreserveClli = value,
            });

            Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Heic, "preserve-clli"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                PreserveClli = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-clli"));
        }
    }
}
