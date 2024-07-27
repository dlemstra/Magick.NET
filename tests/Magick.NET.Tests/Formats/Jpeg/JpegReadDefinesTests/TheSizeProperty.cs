// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegReadDefinesTests
{
    public class TheSizeProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            var settings = new MagickReadSettings
            {
                Defines = new JpegReadDefines
                {
                    Size = new MagickGeometry(61, 59),
                },
            };

            using var image = new MagickImage();
            image.Read(Files.ImageMagickJPG, settings);

            Assert.Equal("61x59", image.Settings.GetDefine(MagickFormat.Jpeg, "size"));
        }

        [Fact]
        public void ShouldReduceTheSize()
        {
            var settings = new MagickReadSettings
            {
                Defines = new JpegReadDefines
                {
                    Size = new MagickGeometry(61, 59),
                },
            };

            using var image = new MagickImage();
            image.Read(Files.ImageMagickJPG, settings);

            Assert.Equal(62U, image.Width);
            Assert.Equal(59U, image.Height);
        }
    }
}
