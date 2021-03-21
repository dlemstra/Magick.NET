// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class WebPWriteDefinesTests
    {
        public class TheAlphaFilteringProperty : WebPWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        AlphaFiltering = WebPAlphaFiltering.Best,
                    });

                    Assert.Equal("2", image.Settings.GetDefine(MagickFormat.WebP, "alpha-filtering"));
                }
            }
        }
    }
}
