// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JxlWriteDefinesTests
    {
        public class TheDepthImageProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new JxlWriteDefines
                    {
                        Effort = 2,
                    });

                    Assert.Equal("2", image.Settings.GetDefine(MagickFormat.Jxl, "effort"));
                }
            }
        }
    }
}
