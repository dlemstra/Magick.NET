// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ShortConverterTests
{
    public class TheToArrayMethod
    {
        [Fact]
        public void ShouldReturnNullWhenIntPtrIsZero()
        {
            var value = ShortConverter.ToArray(IntPtr.Zero, 4);
            Assert.Null(value);
        }
    }
}
