// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheColorTypeProperty
    {
        [Fact]
        public void ShouldDefaultToUndefined()
        {
            using var image = new MagickImage();

            Assert.Equal(ColorType.Undefined, image.Settings.ColorType);
        }

        [Fact]
        public void ShouldBeUsedWhenWritingTheImage()
        {
            using var image = new MagickImage();
            image.Read(Files.Builtin.Wizard);

            Assert.NotEqual(ColorType.TrueColor, image.ColorType);

            image.Settings.ColorType = ColorType.TrueColor;

            using var memStream = new MemoryStream();
            image.Format = MagickFormat.Jpg;
            image.Write(memStream);
            memStream.Position = 0;

            using var result = new MagickImage(memStream);

            Assert.Equal(ColorType.TrueColor, result.ColorType);
        }
    }
}
