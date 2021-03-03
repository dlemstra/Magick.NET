// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class CaptionReadDefinesTests
    {
        public class TheMaxFontPointsizeProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Width = 100,
                    Height = 100,
                    Defines = new CaptionReadDefines
                    {
                        MaxFontPointsize = 42,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read("caption:123", settings);

                    Assert.Equal("42", image.Settings.GetDefine(MagickFormat.Caption, "max-pointsize"));
                }
            }

            [Fact]
            public void ShouldLimitTheFontSize()
            {
                var settings = new MagickReadSettings
                {
                    Width = 100,
                    Height = 80,
                    Defines = new CaptionReadDefines
                    {
                        MaxFontPointsize = 15,
                    },
                };

                using (var image = new MagickImage("caption:testing 1 2 3", settings))
                {
                    ColorAssert.Equal(MagickColors.White, image, 32, 64);
                }
            }
        }
    }
}
