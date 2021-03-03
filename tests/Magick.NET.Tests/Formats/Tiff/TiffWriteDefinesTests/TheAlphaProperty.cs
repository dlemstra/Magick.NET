// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class TheAlphaProperty : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.SetDefines(new TiffWriteDefines
                    {
                        Alpha = TiffAlpha.Associated,
                    });

                    input.Alpha(AlphaOption.Set);

                    using (var output = WriteTiff(input))
                    {
                        Assert.Equal("associated", output.GetAttribute("tiff:alpha"));
                    }
                }
            }
        }
    }
}
