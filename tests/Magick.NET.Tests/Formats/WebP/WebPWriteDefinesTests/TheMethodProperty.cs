// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class WebPWriteDefinesTests
    {
        public class TheMethodProperty : WebPWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        Method = 3,
                    });

                    Assert.Equal("3", image.Settings.GetDefine(MagickFormat.WebP, "method"));
                }
            }
        }
    }
}
