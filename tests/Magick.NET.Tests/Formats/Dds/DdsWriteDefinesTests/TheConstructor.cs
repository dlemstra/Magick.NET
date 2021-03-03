// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsWriteDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefines()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new DdsWriteDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "cluster-fit"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "compression"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "fast-mipmaps"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "raw"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
                }
            }
        }
    }
}
