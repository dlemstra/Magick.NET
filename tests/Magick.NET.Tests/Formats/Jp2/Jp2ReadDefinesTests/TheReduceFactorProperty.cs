// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class Jp2ReadDefinesTests
    {
        public class TheReduceFactorProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new Jp2ReadDefines
                    {
                        ReduceFactor = 2,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.GrimJP2, settings);

                    Assert.Equal("2", image.Settings.GetDefine(MagickFormat.Jp2, "reduce-factor"));
                }
            }
        }
    }
}
