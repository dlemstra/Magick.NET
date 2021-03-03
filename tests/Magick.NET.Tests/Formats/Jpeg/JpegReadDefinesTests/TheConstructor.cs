// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegReadDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefines()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new JpegReadDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "block-smoothing"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "colors"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "fancy-upsampling"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "size"));
                    Assert.Null(image.Settings.GetDefine("profile:skip"));
                }
            }
        }
    }
}
