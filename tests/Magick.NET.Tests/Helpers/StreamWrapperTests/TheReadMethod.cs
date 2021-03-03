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
        public class TheReadMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenBufferIsNull()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        int count = streamWrapper.Read(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                        Assert.Equal(0, count);
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnZeroWhenNothingShouldBeRead()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        byte[] buffer = new byte[255];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Read((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                            Assert.Equal(0, count);
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringReading()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new ReadExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                        {
                            byte[] buffer = new byte[10];
                            fixed (byte* p = buffer)
                            {
                                int count = streamWrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                                Assert.Equal(-1, count);
                            }
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeRead()
            {
                using (var stream = new MemoryStream(new byte[5]))
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        byte[] buffer = new byte[10];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                            Assert.Equal(5, count);
                        }
                    }
                }
            }
        }
    }
}
