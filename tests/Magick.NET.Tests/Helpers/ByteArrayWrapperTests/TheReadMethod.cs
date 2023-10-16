// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheReadMethod
    {
        [Fact]
        public void ShouldReturnZeroWhenBufferIsNull()
        {
            var wrapper = new ByteArrayWrapper();

            var count = wrapper.Read(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
            Assert.Equal(0, count);
        }

        [Fact]
        public unsafe void ShouldReturnZeroWhenNothingShouldBeRead()
        {
            var wrapper = new ByteArrayWrapper();

            var buffer = new byte[255];
            fixed (byte* p = buffer)
            {
                var count = wrapper.Read((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                Assert.Equal(0, count);
            }
        }

        [Fact]
        public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeRead()
        {
            var wrapper = new ByteArrayWrapper();

            var offset = 0L;
            var buffer = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            fixed (byte* p = buffer)
            {
                var count = wrapper.Write((IntPtr)p, (UIntPtr)buffer.Length, IntPtr.Zero);
                Assert.Equal(10, count);

                offset = wrapper.Seek(0, (IntPtr)SeekOrigin.Current, IntPtr.Zero);
                Assert.Equal(10, offset);

                count = wrapper.Write((IntPtr)p, (UIntPtr)buffer.Length, IntPtr.Zero);
                Assert.Equal(10, count);
            }

            offset = wrapper.Seek(15, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);
            Assert.Equal(15, offset);

            buffer = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            fixed (byte* p = buffer)
            {
                var count = wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                Assert.Equal(5, count);
                Assert.Equal(20, wrapper.Tell(IntPtr.Zero));
                Assert.Equal(5, buffer[0]);
                Assert.Equal(6, buffer[1]);
                Assert.Equal(7, buffer[2]);
                Assert.Equal(8, buffer[3]);
                Assert.Equal(9, buffer[4]);
            }
        }
    }
}
