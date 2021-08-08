// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheWriteAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null));
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.RoseSparkleGIF))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                await input.WriteAsync(stream, MagickFormat.Tiff);

                                memoryStream.Position = 0;
                                using (var output = new MagickImageCollection(stream))
                                {
                                    Assert.Equal(3, output.Count);

                                    for (var i = 0; i < 3; i++)
                                        Assert.Equal(MagickFormat.Tiff, output[i].Format);
                                }
                            }
                        }
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFormatIsNotWritable()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await Assert.ThrowsAsync<MagickMissingDelegateErrorException>(() => input.WriteAsync(memoryStream, MagickFormat.Xc));
                        }
                    }
                }
            }

            public class WithStreamAndWriteDefines
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var defines = new TiffWriteDefines();

                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null, defines));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenDefinesIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(stream, null));
                        }
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var stream = new MemoryStream())
                        {
                            var defines = new TiffWriteDefines()
                            {
                                Endian = Endian.MSB,
                            };

                            await input.WriteAsync(stream, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input[0].Format);

                            using (var output = new MagickImageCollection())
                            {
                                stream.Position = 0;
                                await output.ReadAsync(stream);

                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }
        }
    }
}

#endif
