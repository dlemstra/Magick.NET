// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class WebPWriteDefinesTests
    {
        public class TheTargetPsnrProperty : WebPWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        TargetPsnr = 42.24,
                    });

                    Assert.Equal("42.24", image.Settings.GetDefine(MagickFormat.WebP, "target-psnr"));
                }
            }
        }
    }
}
