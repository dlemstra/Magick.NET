// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffWriteDefinesTests
{
    public class TheAlphaProperty : TiffWriteDefinesTests
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Settings.SetDefines(new TiffWriteDefines
            {
                Alpha = TiffAlpha.Associated,
            });

            image.Alpha(AlphaOption.Set);

            using var output = WriteTiff(image);
            Assert.Equal("associated", output.GetAttribute("tiff:alpha"));
        }
    }
}
