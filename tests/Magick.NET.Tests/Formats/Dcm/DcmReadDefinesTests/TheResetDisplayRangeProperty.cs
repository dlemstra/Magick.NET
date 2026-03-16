// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DcmReadDefinesTests
{
    public class TheResetDisplayRangeProperty
    {
        [Fact]
        public void ShouldNotBeSetWhenTheValueIsFalse()
        {
            var defines = new DcmReadDefines()
            {
                ResetDisplayRange = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Null(image.Settings.GetDefine(MagickFormat.Dcm, "display-range"));
        }

        [Fact]
        public void ShouldBeSetWhenTheValueIsTrue()
        {
            var defines = new DcmReadDefines()
            {
                ResetDisplayRange = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("reset", image.Settings.GetDefine(MagickFormat.Dcm, "display-range"));
        }
    }
}
