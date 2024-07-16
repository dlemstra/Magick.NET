// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using System;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheColorTypeProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                ColorType = ColorType.Grayscale,
            });
            Assert.Equal("0", image.Settings.GetDefine(MagickFormat.Png, "color-type"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                ColorType = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "color-type"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorTypeIsOptimize()
        {
            using var image = new MagickImage();
            var optimizeException = Assert.Throws<ArgumentException>(() => image.Settings.SetDefines(new PngWriteDefines
            {
                ColorType = ColorType.Optimize,
            }));

            Assert.Equal($"Unsupported color type: {ColorType.Optimize}", optimizeException.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorTypeIsInvalid()
        {
            using var image = new MagickImage();
            var invalidColorTypeException = Assert.Throws<ArgumentException>(() => image.Settings.SetDefines(new PngWriteDefines
            {
                ColorType = (ColorType)999,
            }));

            Assert.Equal($"Unsupported color type: 999", invalidColorTypeException.Message);
        }
    }
}
