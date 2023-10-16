// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheSeekMethod
    {
        [Fact]
        public unsafe void ShouldReturnTheNewOffset()
        {
            var wrapper = new ByteArrayWrapper();

            var result = wrapper.Seek(10, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);

            Assert.Equal(10, result);
        }

        [Fact]
        public unsafe void ShouldNotChangeTheSizeOfTheBytes()
        {
            var wrapper = new ByteArrayWrapper();

            var result = wrapper.Seek(10, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);

            Assert.Empty(wrapper.GetBytes());
        }

        [Fact]
        public unsafe void ShouldReturnTheNewOffsetStartingFromTheCurrentPosition()
        {
            var wrapper = new ByteArrayWrapper();

            var buffer = new byte[42];
            fixed (byte* p = buffer)
            {
                wrapper.Write((IntPtr)p, (UIntPtr)32, IntPtr.Zero);
            }

            var result = wrapper.Seek(10, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

            Assert.Equal(42, result);
        }

        [Fact]
        public unsafe void ShouldReturnCorrectValueFromEnd()
        {
            var wrapper = new ByteArrayWrapper();

            var buffer = new byte[42];
            fixed (byte* p = buffer)
            {
                wrapper.Write((IntPtr)p, (UIntPtr)buffer.Length, IntPtr.Zero);

                var result = wrapper.Seek(0, (IntPtr)SeekOrigin.End, IntPtr.Zero);
                Assert.Equal(42, result);
            }
        }

        [Fact]
        public void ShouldReturnMinusOneForInvalidOffset()
        {
            var wrapper = new ByteArrayWrapper();
            var result = wrapper.Seek(-10, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

            Assert.Equal(-1, result);
        }

        [Fact]
        public void ShouldNotChangeOffsetWhenValueIsInvalid()
        {
            var wrapper = new ByteArrayWrapper();
            wrapper.Seek(-10, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

            Assert.Equal(0, wrapper.Tell(IntPtr.Zero));
        }

        [Fact]
        public void ShouldReturnMinusOneForInvalidWhence()
        {
            var wrapper = new ByteArrayWrapper();
            var result = wrapper.Seek(0, (IntPtr)3, IntPtr.Zero);

            Assert.Equal(-1, result);
        }
    }
}
