// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class StreamWrapperTests
{
    public class TheCreateForReadingMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenStreamIsNotReadable()
        {
            using var stream = TestStream.ThatCannotRead();

            var exception = Assert.Throws<ArgumentException>("stream", () => StreamWrapper.CreateForReading(stream));
            ExceptionAssert.Contains("readable", exception);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenStreamIsOnlyReadable()
        {
            using var stream = TestStream.ThatCanOnlyRead();
            using var wrapper = StreamWrapper.CreateForReading(stream);
        }
    }
}
