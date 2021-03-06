// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DngReadDefinesTests
    {
        public class TheDisableAutoBrightnessProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new DngReadDefines
                {
                    DisableAutoBrightness = true,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Dng, "no-auto-bright"));
                }
            }
        }
    }
}
