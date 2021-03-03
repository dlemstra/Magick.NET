// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Defines;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegReadDefinesTests
    {
        public class TheSkipProfilesProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new JpegReadDefines
                    {
                        SkipProfiles = JpegProfileTypes.App,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.ImageMagickJPG, settings);

                    Assert.Equal("App", image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineForInvalidValues()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new JpegReadDefines
                    {
                        SkipProfiles = (JpegProfileTypes)64,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.ImageMagickJPG, settings);

                    Assert.Null(image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldSkipTheSpecifiedProfiles()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new JpegReadDefines
                    {
                        SkipProfiles = JpegProfileTypes.Iptc | JpegProfileTypes.Icc,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.FujiFilmFinePixS1ProJPG);
                    Assert.NotNull(image.GetIptcProfile());

                    image.Read(Files.FujiFilmFinePixS1ProJPG, settings);
                    Assert.Null(image.GetIptcProfile());
                    Assert.Equal("Icc,Iptc", image.Settings.GetDefine("profile:skip"));
                }
            }
        }
    }
}
