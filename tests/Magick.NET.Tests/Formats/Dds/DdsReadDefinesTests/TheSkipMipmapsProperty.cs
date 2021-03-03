// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsReadDefinesTests
    {
        public class TheSkipMipmapsProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new DdsReadDefines
                    {
                        SkipMipmaps = false,
                    },
                };

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.Coders.TestDDS, settings);

                    Assert.Equal(5, images.Count);
                    Assert.Equal("false", images[0].Settings.GetDefine(MagickFormat.Dds, "skip-mipmaps"));
                }
            }

            [Fact]
            public void ShouldSkipTheMipmaps()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new DdsReadDefines
                    {
                        SkipMipmaps = true,
                    },
                };

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.Coders.TestDDS, settings);

                    Assert.Single(images);
                }
            }
        }
    }
}
