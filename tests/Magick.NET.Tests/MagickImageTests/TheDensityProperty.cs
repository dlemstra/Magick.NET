// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDensityProperty
    {
        [Fact]
        public void ShouldNotChangeWhenValueIsNull()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

            Assert.Equal(300, image.Density.X);

            image.Density = null!;

            Assert.Equal(300, image.Density.X);
        }

        [Fact]
        public void ShouldUpdateExifProfile()
        {
            using var memStream = new MemoryStream();
            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

            var profile = input.GetExifProfile();
            Assert.NotNull(profile);

            var value = profile.GetValue(ExifTag.XResolution);
            Assert.NotNull(value);

            Assert.Equal("300", value.ToString());

            input.Density = new Density(72);
            input.Write(memStream);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);

            profile = output.GetExifProfile();
            Assert.NotNull(profile);

            value = profile.GetValue(ExifTag.XResolution);
            Assert.NotNull(value);

            Assert.Equal("72", value.ToString());
        }

        [Fact]
        public void ShouldSetTheCorrectDimensionsWhenReadingImage()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.Density);

            image.Settings.Density = new Density(100);

            image.Read(Files.Logos.MagickNETSVG);
            Assert.Equal(new Density(100, DensityUnit.Undefined), image.Density);
            Assert.Equal(524U, image.Width);
            Assert.Equal(252U, image.Height);
        }

        [Fact]
        public void ShouldUpdateTheDensityOfTheExifProfileInsideThe8BimProfile()
        {
            using var input = new MagickImage(Files.EightBimJPG);
            input.Density = new Density(96);

            using var stream = new MemoryStream();
            input.Write(stream);

            var profile = input.GetExifProfile();
            Assert.NotNull(profile);

            var value = profile.GetValue(ExifTag.XResolution);
            Assert.NotNull(value);

            Assert.Equal(96.0, value.Value.ToDouble());

            stream.Position = 0;

            using var output = new MagickImage(stream);
            output.Read(stream);

            Assert.Equal(96.0, input.Density.X);

            profile = output.GetExifProfile();
            Assert.NotNull(profile);

            value = profile.GetValue(ExifTag.XResolution);
            Assert.NotNull(value);

            Assert.Equal(96.0, value.Value.ToDouble());
        }
    }
}
