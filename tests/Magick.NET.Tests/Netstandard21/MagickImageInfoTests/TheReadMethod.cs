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
    public partial class TheReadMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("data", () => imageInfo.Read(ReadOnlySequence<byte>.Empty));
            }
        }

        public class WithReadOnlySpan
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

#endif
