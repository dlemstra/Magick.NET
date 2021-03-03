// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheSetDefineMethod
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", "test");

                    Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpg, "optimize-coding"));
                    Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }

            [Fact]
            public void ShouldChangeTheBooleanToString()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", true);

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }

            [Fact]
            public void ShouldChangeTheIntToString()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", 42);

                    Assert.Equal("42", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedName()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine("profile:skip", "ICC");
                    Assert.Equal("ICC", image.Settings.GetDefine("profile:skip"));
                }
            }
        }
    }
}
