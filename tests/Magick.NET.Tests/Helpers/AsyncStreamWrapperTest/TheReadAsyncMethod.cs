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
    public class TheReadAsyncMethod
    {
        [Fact]
        public async Task ShouldReturnZeroWhenBufferIsNull()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForReading(stream);

            void ReadSync()
            {
                var count = wrapper.Read(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                Assert.Equal(0, count);
            }

            await wrapper.ReadAsync(ReadSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldReturnZeroWhenNothingShouldBeRead()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForReading(stream);

            unsafe void ReadSync()
            {
                var buffer = new byte[255];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Read((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                    Assert.Equal(0, count);
                }
            }

            await wrapper.ReadAsync(ReadSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringReading()
        {
            using var memStream = new MemoryStream();
            using var stream = new ReadExceptionStream(memStream);
            using var wrapper = AsyncStreamWrapper.CreateForReading(stream);

            unsafe void ReadSync()
            {
                var buffer = new byte[10];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                    Assert.Equal(-1, count);

                    count = wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                    Assert.Equal(-1, count);
                }
            }

            await wrapper.ReadAsync(ReadSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldReturnTheNumberOfBytesThatCouldBeRead()
        {
            using var stream = new MemoryStream(new byte[5]);
            using var wrapper = AsyncStreamWrapper.CreateForReading(stream);

            unsafe void ReadSync()
            {
                var buffer = new byte[10];
                fixed (byte* p = buffer)
                {
                    var count = wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                    Assert.Equal(5, count);
                }
            }

            await wrapper.ReadAsync(ReadSync, CancellationToken.None);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenOperationIsCancelled()
        {
            using var stream = new MemoryStream();
            using var wrapper = AsyncStreamWrapper.CreateForReading(stream);

            using var cancellationTokenSource = new CancellationTokenSource();

            unsafe void ReadSync()
            {
                cancellationTokenSource.Cancel();

                var buffer = new byte[10];
                fixed (byte* p = buffer)
                {
                    wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                }

                fixed (byte* p = buffer)
                {
                    wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                }
            }

            await Assert.ThrowsAsync<OperationCanceledException>(() => wrapper.ReadAsync(ReadSync, cancellationTokenSource.Token));
        }
    }
}
