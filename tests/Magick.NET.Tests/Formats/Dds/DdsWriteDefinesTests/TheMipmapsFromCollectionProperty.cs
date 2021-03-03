// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsWriteDefinesTests
    {
        public class TheMipmapsFromCollectionProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    var defines = new DdsWriteDefines
                    {
                        MipmapsFromCollection = true,
                        Mipmaps = 4, // this is ignored
                    };

                    image.Settings.SetDefines(defines);

                    Assert.Equal("fromlist", image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                }
            }

            [Fact]
            public void ShouldBeIgnoredWhenSetToFalse()
            {
                using (var image = new MagickImage())
                {
                    var defines = new DdsWriteDefines
                    {
                        MipmapsFromCollection = false,
                        Mipmaps = 4,
                    };

                    image.Settings.SetDefines(defines);

                    Assert.Equal("4", image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                }
            }
        }
    }
}
