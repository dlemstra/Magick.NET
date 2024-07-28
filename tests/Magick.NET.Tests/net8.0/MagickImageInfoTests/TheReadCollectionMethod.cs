// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageInfoTests
{
    public partial class TheReadCollectionMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(ReadOnlySequence<byte>.Empty));
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(Span<byte>.Empty));
            }
        }
    }
}

#endif
