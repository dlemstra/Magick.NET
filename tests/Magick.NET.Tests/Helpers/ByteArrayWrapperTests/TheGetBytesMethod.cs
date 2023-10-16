// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheGetBytesMethod
    {
        [Fact]
        public unsafe void ShouldOnlyReturnTheWrittenBytes()
        {
            var wrapper = new ByteArrayWrapper();
            wrapper.Seek(42, (IntPtr)SeekOrigin.Current, IntPtr.Zero);
            wrapper.Seek(0, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);

            var buffer = new byte[5];
            fixed (byte* p = buffer)
            {
                wrapper.Write((IntPtr)p, (UIntPtr)3, IntPtr.Zero);
            }

            var bytes = wrapper.GetBytes();
            Assert.Equal(3, bytes.Length);
        }
    }
}
