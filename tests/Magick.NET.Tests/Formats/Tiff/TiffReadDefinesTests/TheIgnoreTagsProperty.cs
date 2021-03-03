// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffReadDefinesTests
    {
        public class TheIgnoreTagsProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines
                    {
                        IgnoreTags = new[] { "1234" },
                    });

                    Assert.Equal("1234", image.Settings.GetDefine(MagickFormat.Tiff, "ignore-tags"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines
                    {
                        IgnoreTags = new string[] { },
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));
                }
            }
        }
    }
}
