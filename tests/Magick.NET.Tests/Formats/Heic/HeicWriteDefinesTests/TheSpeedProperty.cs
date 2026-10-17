// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheSpeedProperty
    {
        [Theory]
        [InlineData(0, "0")]
        [InlineData(5, "5")]
        [InlineData(9, "9")]
        public void ShouldSetTheDefine(int value, string expected)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Speed = value,
            });

            Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Heic, "speed"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                Speed = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "speed"));
        }
    }
}
