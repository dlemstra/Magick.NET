// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsWriteDefinesTests
    {
        public class TheMipmapsProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    var defines = new DdsWriteDefines
                    {
                        Mipmaps = 2,
                    };

                    image.Settings.SetDefines(defines);

                    Assert.Equal("2", image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                }
            }
        }
    }
}
