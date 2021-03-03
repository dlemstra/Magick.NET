// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheWriteMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenBufferIsNull()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        int count = streamWrapper.Write(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                        Assert.Equal(0, count);
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnZeroWhenNothingShouldBeWritten()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        byte[] buffer = new byte[255];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Write((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                            Assert.Equal(0, count);
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringWriting()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new WriteExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                        {
                            byte[] buffer = new byte[10];
                            fixed (byte* p = buffer)
                            {
                                int count = streamWrapper.Write((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                                Assert.Equal(-1, count);
                            }
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeWritten()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        byte[] buffer = new byte[5];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                            Assert.Equal(5, count);
                        }
                    }
                }
            }
        }
    }
}
