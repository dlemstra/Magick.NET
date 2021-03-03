// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheExtentProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new JpegWriteDefines
                {
                    Extent = 5,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("5KB", image.Settings.GetDefine(MagickFormat.Jpeg, "extent"));
                }
            }

            [Fact]
            public void ShouldLimitTheSizeOfTheOutputFile()
            {
                var defines = new JpegWriteDefines
                {
                    Extent = 10,
                };

                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        image.Settings.SetDefines(defines);

                        image.Format = MagickFormat.Jpeg;
                        image.Write(memStream);
                        Assert.True(memStream.Length < 10000);
                    }
                }
            }
        }
    }
}
