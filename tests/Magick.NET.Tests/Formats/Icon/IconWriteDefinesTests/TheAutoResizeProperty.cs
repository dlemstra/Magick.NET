// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class IconWriteDefinesTests
{
    public class TheAutoResizeProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenSetToTrue()
        {
            var defines = new IconWriteDefines
            {
                AutoResize = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Icon, "auto-resize"));
        }

        [Fact]
        public void ShouldSetTheDefineWhenSetToFalse()
        {
            var defines = new IconWriteDefines
            {
                AutoResize = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Icon, "auto-resize"));
        }
    }
}
