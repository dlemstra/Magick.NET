// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheWriteMethod
    {
        [Fact]
        public void ShouldReturnZeroWhenBufferIsNull()
        {
            using var wrapper = new ByteArrayWrapper();

            var count = wrapper.Write(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
            Assert.Equal(0, count);
        }

        [Fact]
        public unsafe void ShouldReturnZeroWhenNothingShouldBeWritten()
        {
            using var wrapper = new ByteArrayWrapper();

            var buffer = new byte[255];
            fixed (byte* p = buffer)
            {
                var count = wrapper.Write((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                Assert.Equal(0, count);
            }
        }

        [Fact]
        public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeWritten()
        {
            using var wrapper = new ByteArrayWrapper();

            var buffer = new byte[5];
            fixed (byte* p = buffer)
            {
                var count = wrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                Assert.Equal(5, count);

                wrapper.Seek(10, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

                count = wrapper.Write((IntPtr)p, (UIntPtr)4, IntPtr.Zero);
                Assert.Equal(4, count);

                Assert.Equal(19, wrapper.GetBytes().Length);
            }
        }

        [Fact]
        public unsafe void ShouldCalculateRemainingBytesInBytesCorrectly()
        {
            using var wrapper = new ByteArrayWrapper();
            wrapper.Seek(ByteArrayWrapper.BufferSize + 2, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);
            wrapper.Seek(ByteArrayWrapper.BufferSize + 1, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);

            var buffer = new byte[5] { 1, 2, 3, 4, 5 };
            fixed (byte* p = buffer)
            {
                var count = wrapper.Write((IntPtr)p, (UIntPtr)buffer.Length, IntPtr.Zero);
                Assert.Equal(5, count);

                var bytes = wrapper.GetBytes().Reverse().Take(5).Reverse();
                Assert.Equal(buffer, bytes);
            }
        }
    }
}
