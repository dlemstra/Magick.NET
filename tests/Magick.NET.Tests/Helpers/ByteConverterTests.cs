// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ByteConverterTests
    {
        [Fact]
        public void Test_ToArray()
        {
            byte[] value = ByteConverter.ToArray(IntPtr.Zero, 4);
            Assert.Null(value);
        }
    }
}
