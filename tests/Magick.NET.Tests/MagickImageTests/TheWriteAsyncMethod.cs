// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP
using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheWriteAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync(null));
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
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync(null, MagickFormat.Bmp));
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
        }
    }
}
#endif
