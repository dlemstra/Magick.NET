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
    public partial class MagickImageTests
    {
        public partial class TheWriteAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync((Stream)null));
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync((Stream)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                await input.WriteAsync(stream, MagickFormat.Tiff);
                                Assert.Equal(MagickFormat.Png, input.Format);

                                memoryStream.Position = 0;
                                using (var output = new MagickImage(stream))
                                {
                                    Assert.Equal(MagickFormat.Tiff, output.Format);
                                }
                            }
                        }
                    }
                }
            }

            public class WithStreamAndWriteDefines
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        var defines = new JpegWriteDefines();

                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync((Stream)null, defines));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenWriteDefinesIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Assert.ThrowsAsync<ArgumentNullException>("defines", () => image.WriteAsync(stream, null));
                        }
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var stream = new MemoryStream())
                        {
                            var defines = new JpegWriteDefines
                            {
                                DctMethod = JpegDctMethod.Fast,
                            };

                            await input.WriteAsync(stream, defines);
                            Assert.Equal(MagickFormat.Png, input.Format);

                            stream.Position = 0;
                            using (var output = new MagickImage())
                            {
                                await output.ReadAsync(stream);

                                Assert.Equal(MagickFormat.Jpeg, output.Format);
                            }
                        }
                    }
                }
            }
        }
    }
}
#endif
