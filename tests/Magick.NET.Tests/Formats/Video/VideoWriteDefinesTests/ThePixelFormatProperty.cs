// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class VideoWriteDefinesTests
{
    public class ThePixelFormatProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            using var image = new MagickImage(MagickColors.Magenta, 1, 1);
            image.Settings.SetDefines(new VideoWriteDefines(MagickFormat.Mp4)
            {
                PixelFormat = "magick",
            });

            Assert.Equal("magick", image.Settings.GetDefine("video:pixel-format"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotSetTheDefineWhenValueIsNullOrEmpty(string? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new VideoWriteDefines(MagickFormat.Mp4)
            {
                PixelFormat = value,
            });

            Assert.Null(image.Settings.GetDefine("video:pixel-format"));
        }
    }
}
