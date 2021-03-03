// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheToBase64Method
        {
            [Fact]
            public void ShouldReturnBase64EncodedString()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64();
                    Assert.NotNull(base64);
                    Assert.Equal(11704, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(8778, bytes.Length);
                }
            }

            [Fact]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedFormat()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64(MagickFormat.Jpeg);
                    Assert.NotNull(base64);
                    Assert.Equal(1140, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(853, bytes.Length);
                }
            }

            [Fact]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedDefines()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var defines = new TiffWriteDefines
                    {
                        PreserveCompression = true,
                    };

                    var base64 = image.ToBase64(defines);
                    Assert.NotNull(base64);
                    Assert.Equal(10800, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(8100, bytes.Length);
                }
            }
        }
    }
}
