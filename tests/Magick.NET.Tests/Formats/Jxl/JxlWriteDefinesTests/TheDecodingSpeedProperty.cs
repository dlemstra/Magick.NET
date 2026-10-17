// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JxlWriteDefinesTests
{
    public class TheDecodingSpeedProperty
    {
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void ShouldSetTheDefine(int value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new JxlWriteDefines
            {
                DecodingSpeed = value,
            });

            Assert.Equal(value.ToString(), image.Settings.GetDefine(MagickFormat.Jxl, "decoding-speed"));
        }
    }
}
