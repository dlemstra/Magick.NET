// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new JpegWriteDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "extent"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "quality"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }

            [Fact]
            public void ShouldNotSetAnyDefineForEmptyValues()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new JpegWriteDefines
                    {
                        QuantizationTables = string.Empty,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
                }
            }
        }
    }
}
