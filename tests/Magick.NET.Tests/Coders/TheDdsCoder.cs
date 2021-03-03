// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheDdsCoder
    {
        [Fact]
        public void ShouldUseDxt1AsTheDefaultCompression()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                using (var output = WriteDds(input))
                {
                    Assert.Equal(CompressionMethod.DXT1, output.Compression);
                }
            }
        }

        [Fact]
        public void ShouldUseDxt5AsTheDefaultCompressionForImagesWithAnAlphaChannel()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.Alpha(AlphaOption.Set);

                using (var output = WriteDds(input))
                {
                    Assert.Equal(CompressionMethod.DXT5, output.Compression);
                }
            }
        }

        private static MagickImage WriteDds(MagickImage input)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                input.Format = MagickFormat.Dds;
                input.Write(memStream);
                memStream.Position = 0;

                return new MagickImage(memStream);
            }
        }
    }
}
