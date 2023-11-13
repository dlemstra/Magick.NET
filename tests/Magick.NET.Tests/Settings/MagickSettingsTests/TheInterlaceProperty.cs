// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheInterlaceProperty
    {
        [Fact]
        public void ShouldReturnThecorrectDefaultValue()
        {
            using var image = new MagickImage(MagickColors.Fuchsia, 100, 60);

            Assert.Equal(Interlace.NoInterlace, image.Settings.Interlace);
        }

        [Fact]
        public void ShouldBeUsedWhenWritingJpegImage()
        {
            using var image = new MagickImage(MagickColors.Fuchsia, 100, 60);
            using var memStream = new MemoryStream();

            image.Settings.Interlace = Interlace.Undefined;
            image.Write(memStream, MagickFormat.Jpeg);

            memStream.Position = 0;
            image.Read(memStream);

            Assert.Equal(Interlace.Jpeg, image.Interlace);
        }
    }
}
