// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DcmReadDefinesTests
{
    public class TheFixByteOrderProperty
    {
        [Fact]
        public void ShoulBeSetWhenTheValueIsFalse()
        {
            var defines = new DcmReadDefines()
            {
                FixByteOrder = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Dcm, "fix-byte-order"));
        }

        [Fact]
        public void ShouldBeSetWhenTheValueIsTrue()
        {
            var defines = new DcmReadDefines()
            {
                FixByteOrder = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Dcm, "fix-byte-order"));
        }
    }
}
