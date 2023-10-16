// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheTellMethod
    {
        [Fact]
        public unsafe void ShouldReturnThePositionOfTheWrapper()
        {
            var wrapper = new ByteArrayWrapper();

            var buffer = new byte[42];
            fixed (byte* p = buffer)
            {
                wrapper.Write((IntPtr)p, (UIntPtr)42, IntPtr.Zero);
            }

            var position = wrapper.Tell(IntPtr.Zero);

            Assert.Equal(42, position);
        }

        [Fact]
        public unsafe void ShouldReturnTheOffsetOfTheWrapper()
        {
            var wrapper = new ByteArrayWrapper();
            wrapper.Seek(42, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

            var position = wrapper.Tell(IntPtr.Zero);

            Assert.Equal(42, position);
        }
    }
}
