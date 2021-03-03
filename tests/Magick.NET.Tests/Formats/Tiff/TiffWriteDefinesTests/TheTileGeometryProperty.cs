// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class TheTileGeometryProperty : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new TiffWriteDefines
                    {
                        TileGeometry = new MagickGeometry(1, 2),
                    });

                    Assert.Equal("1x2", image.Settings.GetDefine(MagickFormat.Tiff, "tile-geometry"));
                }
            }
        }
    }
}
