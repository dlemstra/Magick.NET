// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PsdReadDefinesTests
    {
        public class TheAlphaUnblendProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PsdReadDefines
                    {
                        AlphaUnblend = false,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.PlayerPSD, settings);

                    var define = image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend");
                    Assert.Equal("false", define);
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsTrue()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PsdReadDefines
                    {
                        AlphaUnblend = true,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));
                }
            }
        }
    }
}
