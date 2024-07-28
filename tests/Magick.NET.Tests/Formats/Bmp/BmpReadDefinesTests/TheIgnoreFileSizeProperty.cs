// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class BmpReadDefinesTests
{
    public class TheIgnoreFileSizeProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenSetToTrue()
        {
            var defines = new BmpReadDefines
            {
                IgnoreFileSize = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Bmp, "ignore-filesize"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenSetToFalse()
        {
            var defines = new BmpReadDefines
            {
                IgnoreFileSize = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Null(image.Settings.GetDefine(MagickFormat.Bmp, "ignore-filesize"));
        }

        [Fact]
        public void ShouldAllowReadingBmpWithInvalidFileSize()
        {
            var defines = new BmpReadDefines
            {
                IgnoreFileSize = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            image.Read(Files.Coders.InvalidCrcBMP);
        }

        [Fact]
        public void ShouldNotAllowReadingBmpWithInvalidFileSizeByDefault()
        {
            var defines = new BmpReadDefines();

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(Files.Coders.InvalidCrcBMP));
            Assert.Contains("length and filesize do not match", exception.Message);
        }
    }
}
