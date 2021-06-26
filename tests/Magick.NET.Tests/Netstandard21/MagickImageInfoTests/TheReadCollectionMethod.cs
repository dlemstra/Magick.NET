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
        public partial class TheReadCollectionMethod
        {
            public class WithSpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(Span<byte>.Empty));
                }
            }
        }
    }
}

#endif
