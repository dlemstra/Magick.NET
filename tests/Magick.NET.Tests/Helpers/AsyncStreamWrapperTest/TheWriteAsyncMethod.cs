// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class AsyncStreamWrapperTest
{
    public class TheWriteAsyncMethod
    {
        [Fact]
        public async Task ShouldReturnZeroWhenBufferIsNull()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForWriting(stream);

            unsafe void WriteSync()
            {
                var count = wrapper.Write(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                Assert.Equal(0, count);
            }

            await wrapper.WriteAsync(WriteSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldReturnZeroWhenNothingShouldBeWritten()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForWriting(stream);

            unsafe void WriteSync()
            {
                var buffer = new byte[255];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Write((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                    Assert.Equal(0, count);
                }
            }

            await wrapper.WriteAsync(WriteSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringWriting()
        {
            using var memStream = new MemoryStream();
            using var stream = new WriteExceptionStream(memStream);
            using var wrapper = AsyncStreamWrapper.CreateForWriting(stream);

            unsafe void WriteSync()
            {
                var buffer = new byte[10];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Write((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                    Assert.Equal(-1, count);
                }
            }

            await wrapper.WriteAsync(WriteSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldReturnTheNumberOfBytesThatCouldBeWritten()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForWriting(stream);

            unsafe void WriteSync()
            {
                var buffer = new byte[5];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                    Assert.Equal(5, count);
                }
            }

            await wrapper.WriteAsync(WriteSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenOperationIsCancelled()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForWriting(stream);

            using var cancellationTokenSource = new CancellationTokenSource();

            unsafe void WriteSync()
            {
                cancellationTokenSource.Cancel();

                var buffer = new byte[5];
                fixed (byte* p = buffer)
                {
                    wrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                }

                fixed (byte* p = buffer)
                {
                    wrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                }
            }

            await Assert.ThrowsAsync<OperationCanceledException>(() => wrapper.WriteAsync(WriteSync, cancellationTokenSource.Token));
        }
    }
}
