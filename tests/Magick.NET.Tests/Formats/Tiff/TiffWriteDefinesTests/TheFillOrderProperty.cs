// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class TheFillOrderProperty : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.SetDefines(new TiffWriteDefines
                    {
                        FillOrder = Endian.LSB,
                    });

                    Assert.Equal("LSB", input.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsUndefined()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffWriteDefines
                    {
                        FillOrder = Endian.Undefined,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
                }
            }
        }
    }
}
