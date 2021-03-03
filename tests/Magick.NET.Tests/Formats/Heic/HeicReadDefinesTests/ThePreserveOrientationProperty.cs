// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class HeicReadDefinesTests
    {
        public class ThePreserveOrientationProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new HeicReadDefines
                    {
                        PreserveOrientation = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Heic, "preserve-orientation"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenSetToFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new HeicReadDefines
                    {
                        PreserveOrientation = false,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-orientation"));
                }
            }
        }
    }
}
