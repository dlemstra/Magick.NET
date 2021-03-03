// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsWriteDefinesTests
    {
        public class TheWeightByAlphaProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    var defines = new DdsWriteDefines
                    {
                        WeightByAlpha = false,
                    };

                    image.Settings.SetDefines(defines);

                    Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
                }
            }
        }
    }
}
