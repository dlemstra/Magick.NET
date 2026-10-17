// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheClliProperty
    {
        [Theory]
        [InlineData("1000,400")]
        [InlineData("0,0")]
        [InlineData("65535,65535")]
        public void ShouldSetTheDefine(string value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Clli = value,
            });

            Assert.Equal(value, image.Settings.GetDefine(MagickFormat.Heic, "clli"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotSetTheDefineWhenNullOrEmpty(string? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Clli = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "clli"));
        }
    }
}
