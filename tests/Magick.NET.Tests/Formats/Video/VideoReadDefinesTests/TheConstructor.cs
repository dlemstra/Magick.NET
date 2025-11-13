// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class VideoReadDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new VideoReadDefines(MagickFormat.Mp4));

            Assert.Null(image.Settings.GetDefine("video:vsync"));
            Assert.Null(image.Settings.GetDefine("video:pixel-format"));
            Assert.Null(image.Settings.GetDefine("video:intermediate-format"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFormatIsInvalid()
        {
            var exception = Assert.Throws<ArgumentException>("format", () => new VideoReadDefines(MagickFormat.Png));
            Assert.Contains("The specified format is not a video format.", exception.Message);
        }

        [Theory]
        [InlineData(MagickFormat.ThreeGp)]
        [InlineData(MagickFormat.ThreeG2)]
        [InlineData(MagickFormat.APng)]
        [InlineData(MagickFormat.Avi)]
        [InlineData(MagickFormat.Flv)]
        [InlineData(MagickFormat.Mkv)]
        [InlineData(MagickFormat.Mov)]
        [InlineData(MagickFormat.Mpeg)]
        [InlineData(MagickFormat.Mpg)]
        [InlineData(MagickFormat.Mp4)]
        [InlineData(MagickFormat.M2v)]
        [InlineData(MagickFormat.M4v)]
        [InlineData(MagickFormat.WebM)]
        [InlineData(MagickFormat.Wmv)]
        public void ShouldAllowSpecifyingVideoFormats(MagickFormat format)
        {
            var defines = new VideoReadDefines(format);
            Assert.Equal(format, defines.Format);
        }
    }
}
