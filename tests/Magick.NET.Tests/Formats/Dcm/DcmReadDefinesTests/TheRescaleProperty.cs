// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DcmReadDefinesTests
{
    public class TheRescaleProperty
    {
        [Fact]
        public void ShoulBeSetWhenTheValueIsFalse()
        {
            var defines = new DcmReadDefines()
            {
                Rescale = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Dcm, "rescale"));
        }

        [Fact]
        public void ShouldBeSetWhenTheValueIsTrue()
        {
            var defines = new DcmReadDefines()
            {
                Rescale = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Dcm, "rescale"));
        }
    }
}
