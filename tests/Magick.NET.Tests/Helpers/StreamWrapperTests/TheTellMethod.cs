// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class StreamWrapperTests
{
    public class TheTellMethod
    {
        [Fact]
        public unsafe void ShouldReturnThePositionOfTheWrapper()
        {
            using var memStream = new MemoryStream();
            using var wrapper = StreamWrapper.CreateForReading(memStream);

            var buffer = new byte[42];
            fixed (byte* p = buffer)
            {
                wrapper.Write((IntPtr)p, (UIntPtr)42, IntPtr.Zero);
            }

            var position = wrapper.Tell(IntPtr.Zero);

            Assert.Equal(42, position);
        }

        [Fact]
        public void ShouldNotAddThePositionOfTheStream()
        {
            using var memStream = new MemoryStream();
            memStream.Position = 42;

            using var wrapper = StreamWrapper.CreateForReading(memStream);
            var position = wrapper.Tell(IntPtr.Zero);

            Assert.Equal(0, position);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringTelling()
        {
            using var memStream = new MemoryStream();
            using var stream = new TellExceptionStream(memStream);
            using var wrapper = StreamWrapper.CreateForReading(stream);

            var position = wrapper.Tell(IntPtr.Zero);
            Assert.Equal(-1, position);
        }
    }
}
