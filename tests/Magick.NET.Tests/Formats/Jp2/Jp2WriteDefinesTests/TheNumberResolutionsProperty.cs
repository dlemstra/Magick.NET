// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class Jp2WriteDefinesTests
    {
        public class TheNumberResolutionsProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new Jp2WriteDefines
                    {
                        NumberResolutions = 4,
                    });

                    Assert.Equal("4", image.Settings.GetDefine(MagickFormat.Jp2, "number-resolutions"));
                }
            }
        }
    }
}
