// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class WebPWriteDefinesTests
    {
        public class TheEmulateJpegSizeProperty : WebPWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefineWhenSetToTrue()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        EmulateJpegSize = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.WebP, "emulate-jpeg-size"));
                }
            }

            [Fact]
            public void ShouldSetTheDefineWhenSetToFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        EmulateJpegSize = false,
                    });

                    Assert.Equal("false", image.Settings.GetDefine(MagickFormat.WebP, "emulate-jpeg-size"));
                }
            }
        }
    }
}
