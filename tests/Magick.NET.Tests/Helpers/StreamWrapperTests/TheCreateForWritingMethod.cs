// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheCreateForWritingMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotWritable()
            {
                using (TestStream stream = new TestStream(true, false, true))
                {
                    var exception = Assert.Throws<ArgumentException>("stream", () =>
                    {
                        StreamWrapper.CreateForWriting(stream);
                    });

                    Assert.Contains("writable", exception.Message);
                }
            }

            [Fact]
            public void ShouldOnlySetReaderWhenStreamIsOnlyReadable()
            {
                using (TestStream stream = new TestStream(false, true, true))
                {
                    var streamWrapper = StreamWrapper.CreateForWriting(stream);
                }
            }
        }
    }
}
