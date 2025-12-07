// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegWriteDefinesTests
{
    public class TheHighBitDepthProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            var defines = new JpegWriteDefines
            {
                HighBitDepth = true,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpeg, "high-bit-depth"));
        }

        [Fact]
        public void ShouldEncodeTheImageIn8bitWhenNotSet()
        {
            var defines = new JpegWriteDefines
            {
                HighBitDepth = false,
            };

            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            input.Depth = 16;

            using var memStream = new MemoryStream();
            input.Write(memStream, defines);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            Assert.Equal(8u, output.Depth);
        }

        [Fact]
        public void ShouldEncodeTheImageIn12bit()
        {
            var defines = new JpegWriteDefines
            {
                HighBitDepth = true,
            };

            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            input.Depth = 11;

            using var memStream = new MemoryStream();
            input.Write(memStream, defines);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            Assert.Equal(12u, output.Depth);
        }

        [Theory]
        [InlineData(2, 8)]
        [InlineData(3, 8)]
        [InlineData(4, 8)]
        [InlineData(5, 8)]
        [InlineData(6, 8)]
        [InlineData(7, 8)]
        [InlineData(8, 8)]
        [InlineData(9, 12)]
        [InlineData(10, 12)]
        [InlineData(11, 12)]
        [InlineData(12, 12)]
        [InlineData(13, 12)]
        [InlineData(14, 12)]
        [InlineData(15, 12)]
        [InlineData(16, 12)]
        public void ShouldEncodeTheImageInTheCorrectBitDepthWhenNotCompressingLossless(uint depth, uint expectedDepth)
        {
            var defines = new JpegWriteDefines
            {
                HighBitDepth = true,
            };

            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            input.Depth = depth;

            using var memStream = new MemoryStream();
            input.Write(memStream, defines);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            Assert.Equal(expectedDepth, output.Depth);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        public void ShouldEncodeTheImageInTheSpecifiedDepthWhenCompressingLossless(uint depth)
        {
            var defines = new JpegWriteDefines
            {
                HighBitDepth = true,
            };

            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            input.Depth = depth;
            input.Quality = 100;
            input.Settings.Compression = CompressionMethod.LosslessJPEG;

            using var memStream = new MemoryStream();
            input.Write(memStream, defines);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            Assert.Equal(depth, output.Depth);
        }
    }
}
