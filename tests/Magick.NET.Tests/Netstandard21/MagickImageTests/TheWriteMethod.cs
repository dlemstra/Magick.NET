// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheWriteMethod
    {
        public class WithBufferWriter
        {
            [Fact]
            public void ShouldThrowExceptionWhenBufferWriterIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<byte>)null));
            }
        }

        public class WithBufferWriterAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenBufferWriterIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<byte>)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                var bufferWriter = new ArrayBufferWriter<byte>();
                using var input = new MagickImage(Files.CirclePNG);
                input.Write(bufferWriter, MagickFormat.Tiff);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(bufferWriter.WrittenSpan);

                Assert.Equal(MagickFormat.Tiff, output.Format);
            }
        }

        public class WithBufferWriterAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenBufferWriterIsNull()
            {
                var defines = new JpegWriteDefines();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<byte>)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenDefinesIsNull()
            {
                var bufferWriter = new ArrayBufferWriter<byte>();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("defines", () => image.Write(bufferWriter, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                var bufferWriter = new ArrayBufferWriter<byte>();
                var defines = new JpegWriteDefines
                {
                    DctMethod = JpegDctMethod.Fast,
                };
                using var input = new MagickImage(Files.CirclePNG);
                input.Write(bufferWriter, defines);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(bufferWriter.WrittenSpan);

                Assert.Equal(MagickFormat.Jpeg, output.Format);
            }
        }
    }
}
#endif
