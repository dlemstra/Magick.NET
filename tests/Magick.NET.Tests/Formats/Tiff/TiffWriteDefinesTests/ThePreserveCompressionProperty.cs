// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        public class ThePreserveCompressionProperty : TiffWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefineWhenSetToTrue()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new TiffWriteDefines
                    {
                        PreserveCompression = true,
                    });

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Tiff, "preserve-compression"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenSetToFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffWriteDefines
                    {
                        PreserveCompression = false,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-compression"));
                }
            }
        }
    }
}
