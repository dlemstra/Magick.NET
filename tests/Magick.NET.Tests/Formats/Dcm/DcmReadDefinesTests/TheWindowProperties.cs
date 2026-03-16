// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DcmReadDefinesTests
{
    public class TheWindowProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValueWhenOnlyCenterIsSet()
        {
            var defines = new DcmReadDefines()
            {
                WindowCenter = 42,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("42x", image.Settings.GetDefine(MagickFormat.Dcm, "window"));
        }

        [Fact]
        public void ShouldSetTheCorrectValueWhenOnlyWidthIsSet()
        {
            var defines = new DcmReadDefines()
            {
                WindowWidth = 42,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("x42", image.Settings.GetDefine(MagickFormat.Dcm, "window"));
        }

        [Fact]
        public void ShouldSetTheCorrectValueWheWidthAndCenterAreSet()
        {
            var defines = new DcmReadDefines()
            {
                WindowCenter = 24,
                WindowWidth = 42,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("24x42", image.Settings.GetDefine(MagickFormat.Dcm, "window"));
        }
    }
}
