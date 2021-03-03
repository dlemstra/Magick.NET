// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegReadDefinesTests
    {
        public class TheBlockSmoothingProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new JpegReadDefines
                    {
                        BlockSmoothing = true,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.ImageMagickJPG, settings);

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpeg, "block-smoothing"));
                }
            }
        }
    }
}
