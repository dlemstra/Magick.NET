// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheToByteArrayMethod
        {
            [Fact]
            public void ShouldReturnImageWithTheSameFormat()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    var data = images.ToByteArray();

                    Assert.NotNull(data);
                    Assert.Equal(9891, data.Length);

                    images.Read(data);

                    Assert.Equal(MagickFormat.Gif, images[0].Format);
                }
            }

            [Fact]
            public void ShouldUseTheFormatOfTheDefines()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    var defines = new TiffWriteDefines
                    {
                        PreserveCompression = true,
                    };

                    var data = images.ToByteArray(defines);

                    Assert.NotNull(data);
                    Assert.Equal(28316, data.Length);

                    images.Read(data);

                    Assert.Equal(MagickFormat.Tiff, images[0].Format);
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    var data = images.ToByteArray(MagickFormat.Tiff);

                    Assert.NotNull(data);
                    Assert.Equal(39494, data.Length);

                    images.Read(data);

                    Assert.Equal(MagickFormat.Tiff, images[0].Format);
                }
            }
        }
    }
}
