// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngReadDefinesTests
    {
        public class TheSkipProfilesProperty
        {
            [Fact]
            public void ShouldNotSetDefineWhenValueIsInvalid()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines
                    {
                        SkipProfiles = (PngProfileTypes)64,
                    });

                    Assert.Null(image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines
                    {
                        SkipProfiles = PngProfileTypes.Icc | PngProfileTypes.Iptc,
                    });

                    Assert.Equal("Icc,Iptc", image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldSkipProfilesWhenLoadingImage()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PngReadDefines
                    {
                        SkipProfiles = PngProfileTypes.Xmp | PngProfileTypes.Exif,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.FujiFilmFinePixS1ProPNG);
                    Assert.NotNull(image.GetExifProfile());
                    Assert.NotNull(image.GetXmpProfile());

                    image.Read(Files.FujiFilmFinePixS1ProPNG, settings);
                    Assert.Null(image.GetExifProfile());
                    Assert.Null(image.GetXmpProfile());
                }
            }
        }
    }
}
