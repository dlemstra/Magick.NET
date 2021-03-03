// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheRemoveDefineMethod
        {
            [Fact]
            public void ShouldRemoveTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", "test");

                    image.Settings.RemoveDefine(MagickFormat.Jpeg, "optimize-coding");
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }
        }
    }
}
