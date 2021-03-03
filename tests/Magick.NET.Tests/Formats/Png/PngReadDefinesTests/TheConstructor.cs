// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngReadDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
                    Assert.Null(image.Settings.GetDefine("profile:skip"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "swap-bytes"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "chunk-cache-max"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "chunk-malloc-max"));
                }
            }
        }
    }
}
