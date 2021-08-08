// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheWriteMethod
        {
            public class WithBufferWriter
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<QuantumType>)null));
                    }
                }
            }

            public class WithBufferWriterAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<QuantumType>)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        input.Write(bufferWriter, MagickFormat.Tiff);

                        using (var output = new MagickImage(bufferWriter.WrittenSpan))
                        {
                            Assert.Equal(MagickFormat.Tiff, output.Format);
                        }
                    }
                }
            }

            public class WithBufferWriterAndWriteDefines
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        var defines = new JpegWriteDefines();

                        Assert.Throws<ArgumentNullException>("bufferWriter", () => image.Write((IBufferWriter<QuantumType>)null, defines));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenDefinesIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        Assert.Throws<ArgumentNullException>("defines", () => image.Write(bufferWriter, null));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        var defines = new JpegWriteDefines
                        {
                            DctMethod = JpegDctMethod.Fast,
                        };

                        input.Write(bufferWriter, defines);
                        Assert.Equal(MagickFormat.Png, input.Format);

                        using (var output = new MagickImage(bufferWriter.WrittenSpan))
                        {
                            Assert.Equal(MagickFormat.Jpeg, output.Format);
                        }
                    }
                }
            }
        }
    }
}
#endif
