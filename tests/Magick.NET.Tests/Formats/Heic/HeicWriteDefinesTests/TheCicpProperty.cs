// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheCicpProperty
    {
        [Theory]
        [InlineData("1/13/6/1")]
        [InlineData("1/13/0/1")]
        public void ShouldSetTheDefine(string value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Cicp = value,
            });

            Assert.Equal(value, image.Settings.GetDefine(MagickFormat.Heic, "cicp"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotSetTheDefineWhenNullOrEmpty(string? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Cicp = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "cicp"));
        }
    }
}
