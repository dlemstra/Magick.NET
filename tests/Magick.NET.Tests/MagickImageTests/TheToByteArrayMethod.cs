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
        public class TheToByteArrayMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenDefinesIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("defines", () =>
                    {
                        image.ToByteArray(null);
                    });
                }
            }

            [Fact]
            public void ShouldReturnImageWithTheSameFormat()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    var data = image.ToByteArray();

                    Assert.NotNull(data);
                    Assert.InRange(data.Length, 18830, 18831);

                    image.Read(data);

                    Assert.Equal(MagickFormat.Jpeg, image.Format);
                }
            }

            [Fact]
            public void ShouldUseTheFormatOfTheDefines()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var defines = new JpegWriteDefines
                    {
                        OptimizeCoding = true,
                    };

                    var data = image.ToByteArray(defines);

                    Assert.NotNull(data);
                    Assert.InRange(data.Length, 853, 858);

                    image.Read(data);

                    Assert.Equal(MagickFormat.Jpeg, image.Format);
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var data = image.ToByteArray(MagickFormat.Jpeg);

                    Assert.NotNull(data);
                    Assert.InRange(data.Length, 60301, 60304);

                    image.Read(data);

                    Assert.Equal(MagickFormat.Jpeg, image.Format);
                }
            }
        }
    }
}
