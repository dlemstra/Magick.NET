// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public class QuantumConverterTests
    {
        [Fact]
        public void Test_ToArray()
        {
            QuantumType[] value = QuantumConverter.ToArray(IntPtr.Zero, 4);
            Assert.Null(value);
        }
    }
}
