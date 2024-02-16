﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffReadDefinesTests
{
    public class TheIgnoreExifPopertiesProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffReadDefines
            {
                IgnoreExifProperties = true,
            });

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenTheValueIsFalse()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffReadDefines
            {
                IgnoreExifProperties = false,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
        }

        [Fact]
        public void ShouldIgnoreTheExifProperties()
        {
            var settings = new MagickReadSettings
            {
                Defines = new TiffReadDefines
                {
                    IgnoreExifProperties = true,
                },
            };

            using var image = new MagickImage();
            image.Read(Files.InvitationTIF);
            Assert.NotNull(image.GetAttribute("exif:PixelXDimension"));

            image.Read(Files.InvitationTIF, settings);
            Assert.Null(image.GetAttribute("exif:PixelXDimension"));
        }
    }
}
