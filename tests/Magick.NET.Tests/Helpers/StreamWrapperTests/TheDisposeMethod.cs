// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheDisposeMethod
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenCalledTwice()
            {
                using (var stream = new TestStream(true, true, true))
                {
                    var streamWrapper = StreamWrapper.CreateForReading(stream);
                    streamWrapper.Dispose();
                    streamWrapper.Dispose();
                }
            }
        }
    }
}
