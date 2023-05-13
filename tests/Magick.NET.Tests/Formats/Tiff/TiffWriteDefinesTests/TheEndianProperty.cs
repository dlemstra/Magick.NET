// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffWriteDefinesTests
{
    public class TheEndianProperty : TiffWriteDefinesTests
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Settings.SetDefines(new TiffWriteDefines
            {
                Endian = Endian.MSB,
            });

            using var output = WriteTiff(image);

            Assert.Equal("msb", output.GetAttribute("tiff:endian"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenTheValueIsUndefined()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffWriteDefines
            {
                Endian = Endian.Undefined,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "endian"));
        }
    }
}
