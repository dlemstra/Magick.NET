// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheWriteMethod
        {
            public class WithBufferWriter
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("bufferWriter", () => images.Write((IBufferWriter<byte>)null));
                    }
                }
            }

            public class WithBufferWriterAndFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("bufferWriter", () => images.Write((IBufferWriter<byte>)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        input.Write(bufferWriter, MagickFormat.Tiff);
                        Assert.Equal(MagickFormat.Png, input[0].Format);

                        using (var output = new MagickImageCollection(bufferWriter.WrittenSpan))
                        {
                            Assert.Single(output);
                            Assert.Equal(MagickFormat.Tiff, output[0].Format);
                        }
                    }
                }
            }

            public class WithBufferWriterAndWriteDefines
            {
                [Fact]
                public void ShouldThrowExceptionWhenBufferWriterIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var defines = new TiffWriteDefines();

                        Assert.Throws<ArgumentNullException>("bufferWriter", () => images.Write((IBufferWriter<byte>)null, defines));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenDefinesIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        Assert.Throws<ArgumentNullException>("defines", () => images.Write(bufferWriter, null));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        var bufferWriter = new ArrayBufferWriter<byte>();

                        var defines = new TiffWriteDefines()
                        {
                            Endian = Endian.MSB,
                        };

                        input.Write(bufferWriter, MagickFormat.Tiff);
                        Assert.Equal(MagickFormat.Png, input[0].Format);

                        using (var output = new MagickImageCollection())
                        {
                            output.Read(bufferWriter.WrittenSpan);

                            Assert.Single(output);
                            Assert.Equal(MagickFormat.Tiff, output[0].Format);
                        }
                    }
                }
            }
        }
    }
}

#endif
