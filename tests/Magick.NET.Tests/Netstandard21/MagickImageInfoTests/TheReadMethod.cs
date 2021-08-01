// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        public partial class TheReadMethod
        {
            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    var imageInfo = new MagickImageInfo();

                    Assert.Throws<ArgumentException>("data", () => imageInfo.Read(Span<byte>.Empty));
                }
            }
        }
    }
}

#endif
