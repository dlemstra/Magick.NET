// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngReadDefinesTests
    {
        public class ThePreserveiCCPProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenSetToTrue()
            {
                var defines = new PngReadDefines
                {
                    PreserveiCCP = true,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenSetToFalse()
            {
                var defines = new PngReadDefines
                {
                    PreserveiCCP = false,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
                }
            }
        }
    }
}
