// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class BytesTests
{
    public partial class TheCreateAsyncMethod
    {
        [Fact]
        public Task ShouldThrowExceptionWhenStreamIsNull()
            => Assert.ThrowsAsync<ArgumentNullException>("stream", () => Bytes.CreateAsync(null, CancellationToken.None));

        [Fact]
        public async Task ShouldThrowExceptionWhenStreamIsEmpty()
        {
            using var memStream = new MemoryStream();

            await Assert.ThrowsAsync<ArgumentException>("stream", () => Bytes.CreateAsync(memStream, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenStreamCannotRead()
        {
            using var stream = TestStream.ThatCannotRead();

            await Assert.ThrowsAsync<ArgumentException>("stream", () => Bytes.CreateAsync(stream, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenStreamIsTooLong()
        {
            using var stream = TestStream.ThatCannotWrite();
            stream.SetLength(long.MaxValue);

            await Assert.ThrowsAsync<ArgumentException>("length", () => Bytes.CreateAsync(stream, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldSetPropertiesWhenStreamIsFileStream()
        {
            using var fileStream = File.OpenRead(Files.ImageMagickJPG);
            var bytes = await Bytes.CreateAsync(fileStream, CancellationToken.None);

            Assert.Equal(18749, bytes.Length);

            var data = bytes.GetData();
            Assert.NotNull(data);
            Assert.Equal(18749, data.Length);
        }

        [Fact]
        public async Task ShouldSetPropertiesWhenStreamIsNonSeekable()
        {
            using var stream = new NonSeekableStream(Files.ImageMagickJPG);
            var bytes = await Bytes.CreateAsync(stream, CancellationToken.None);

            Assert.Equal(18749, bytes.Length);
            Assert.NotNull(bytes.GetData());
        }
    }
}

#endif
