// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheCreateForReadingMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using (TestStream stream = new TestStream(false, true, true))
                {
                    var exception = Assert.Throws<ArgumentException>("stream", () =>
                    {
                        StreamWrapper.CreateForReading(stream);
                    });

                    Assert.Contains("readable", exception.Message);
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenStreamIsOnlyReadable()
            {
                using (TestStream stream = new TestStream(true, true, true))
                {
                    StreamWrapper.CreateForReading(stream);
                }
            }
        }
    }
}
