// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class BmpWriteDefinesTests
{
    public class TheSubtypeProperty
    {
        [Fact]
        public void ShouldBeUsed()
        {
            var defines = new BmpWriteDefines
            {
                Subtype = BmpSubtype.RGB555,
            };

            using var image = new MagickImage(Files.Builtin.Logo);
            image.Format = MagickFormat.Bmp;
            image.ColorType = ColorType.TrueColor;

            long length;

            using var memStream = new MemoryStream();
            image.Write(memStream);
            length = memStream.Length;

            using var memStream2 = new MemoryStream();
            image.Write(memStream2);
            Assert.Equal(length, memStream2.Length);

            image.Settings.SetDefines(defines);

            using var memStream3 = new MemoryStream();
            image.Write(memStream3);
            Assert.True(memStream3.Length < length);
        }
    }
}
