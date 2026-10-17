// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class CaptionReadDefinesTests
{
    public class TheSplitProperty
    {
        [Theory]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        public void ShouldSetTheDefine(bool value, string expected)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new CaptionReadDefines
            {
                Split = value,
            });

            Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Caption, "split"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new CaptionReadDefines
            {
                Split = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Caption, "split"));
        }
    }
}
