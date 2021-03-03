// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class TheConstructor : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldNotSetAnyDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffWriteDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "alpha"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "endian"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "predictor"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "preserve-compression"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "rows-per-strip"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "tile-geometry"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "write-layers"));
                }
            }
        }
    }
}
