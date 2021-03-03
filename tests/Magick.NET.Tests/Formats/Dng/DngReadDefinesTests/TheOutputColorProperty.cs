// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DngReadDefinesTests
    {
        public class TheOutputColorProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new DngReadDefines
                {
                    OutputColor = DngOutputColor.KodakProPhotoRGB,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("4", image.Settings.GetDefine(MagickFormat.Dng, "output_color"));
                }
            }
        }
    }
}
