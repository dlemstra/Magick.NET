// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class ThePredictorProperty : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var input = new MagickImage())
                {
                    input.Settings.SetDefines(new TiffWriteDefines
                    {
                        Predictor = 1,
                    });

                    Assert.Equal("1", input.Settings.GetDefine(MagickFormat.Tiff, "predictor"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsNull()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffWriteDefines
                    {
                        Predictor = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "predictor"));
                }
            }
        }
    }
}
