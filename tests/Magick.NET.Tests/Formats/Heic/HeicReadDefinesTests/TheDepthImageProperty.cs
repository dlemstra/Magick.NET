// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class HeicReadDefinesTests
    {
        public class TheDepthImageProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new HeicReadDefines
                    {
                        DepthImage = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Heic, "depth-image"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenSetToFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new HeicReadDefines
                    {
                        DepthImage = false,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "depth-image"));
                }
            }
        }
    }
}
