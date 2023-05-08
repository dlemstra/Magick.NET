// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ByteArrayWrapperTests
    {
        public class TheReadMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenBufferIsNull()
            {
                using var wrapper = new ByteArrayWrapper();

                var count = wrapper.Read(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                Assert.Equal(0, count);
            }

            [Fact]
            public unsafe void ShouldReturnZeroWhenNothingShouldBeRead()
            {
                using var wrapper = new ByteArrayWrapper();

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
                using var wrapper = new ByteArrayWrapper();

                var buffer = new byte[10];
                fixed (byte* p = buffer)
                {
                    wrapper.Write((IntPtr)p, (UIntPtr)10, IntPtr.Zero);

                    wrapper.Seek(-5, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

                    var count = wrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                    Assert.Equal(5, count);
                }
            }
        }
    }
}
