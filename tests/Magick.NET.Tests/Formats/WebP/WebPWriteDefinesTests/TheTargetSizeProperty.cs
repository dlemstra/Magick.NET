// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class WebPWriteDefinesTests
    {
        public class TheTargetSizeProperty : WebPWriteDefinesTests
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Settings.SetDefines(new WebPWriteDefines
                    {
                        TargetSize = 8192,
                    });

                    Assert.Equal("8192", image.Settings.GetDefine(MagickFormat.WebP, "target-size"));

                    using (var output = new MemoryStream())
                    {
                        image.Write(output, MagickFormat.WebP);

                        Assert.Equal(7594, output.Length);
                    }
                }
            }
        }
    }
}
