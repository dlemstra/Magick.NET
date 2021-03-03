// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class INativeInstanceExtensionsTests
    {
        [Fact]
        public void GetInstance_ValueIsNull_ReturnsIntPtrZero()
        {
            INativeInstance instance = null;
            Assert.Equal(IntPtr.Zero, instance.GetInstance());
        }
    }
}
