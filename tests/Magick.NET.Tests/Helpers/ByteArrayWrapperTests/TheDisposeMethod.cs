// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ByteArrayWrapperTests
{
    public class TheDisposeMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenCalledTwice()
        {
            var wrapper = new ByteArrayWrapper();

            wrapper.Dispose();
            wrapper.Dispose();
        }
    }
}
