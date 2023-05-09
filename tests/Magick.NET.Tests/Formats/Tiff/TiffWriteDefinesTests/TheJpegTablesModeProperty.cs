// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffWriteDefinesTests
{
    public class TheJpegTablesModeProperty : TiffWriteDefinesTests
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.Settings.SetDefines(new TiffWriteDefines
                {
                    JpegTablesMode = TiffJpegTablesMode.Huff | TiffJpegTablesMode.Quant,
                });

                Assert.Equal("3", input.Settings.GetDefine(MagickFormat.Tiff, "jpeg-tables-mode"));
            }
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenTheValueIsNull()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new TiffWriteDefines
                {
                    JpegTablesMode = null,
                });

                Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "jpeg-tables-mode"));
            }
        }
    }
}
