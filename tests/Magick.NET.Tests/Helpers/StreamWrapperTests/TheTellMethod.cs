// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheTellMethod
        {
            [Fact]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringTelling()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new TellExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                        {
                            byte[] buffer = new byte[255];
                            fixed (byte* p = buffer)
                            {
                                long count = streamWrapper.Tell(IntPtr.Zero);
                                Assert.Equal(-1, count);
                            }
                        }
                    }
                }
            }
        }
    }
}
