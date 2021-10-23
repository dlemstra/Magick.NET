// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class CaptionReadDefinesTests
    {
        public class TheStartPointsizeProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new CaptionReadDefines
                {
                    StartFontPointsize = 42,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("42", image.Settings.GetDefine(MagickFormat.Caption, "start-pointsize"));
                }
            }
        }
    }
}
