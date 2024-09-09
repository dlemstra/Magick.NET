// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheOrientationProperty
    {
        [Fact]
        public void ShouldOverwriteTheExifOrientation()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

            var profile = image.GetExifProfile();
            Assert.NotNull(profile);

            var exifOrientation = profile.GetValue(ExifTag.Orientation)?.Value;

            Assert.Equal((ushort)1, exifOrientation);

            Assert.Equal(OrientationType.TopLeft, image.Orientation);

            profile.SetValue(ExifTag.Orientation, (ushort)OrientationType.RightTop);
            image.SetProfile(profile);

            image.Orientation = OrientationType.LeftBottom;

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            using var output = new MagickImage(stream);

            profile = output.GetExifProfile();
            Assert.NotNull(profile);

            exifOrientation = profile.GetValue(ExifTag.Orientation)?.Value;

            Assert.Equal((ushort)8, exifOrientation);
            Assert.Equal(OrientationType.LeftBottom, image.Orientation);
        }
    }
}
