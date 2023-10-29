// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageInfoFactoryTests
{
    public partial class TheCreateMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(ReadOnlySequence<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageInfoFactory();
                var info = factory.Create(new ReadOnlySequence<byte>(data));

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageInfoFactory();
                var info = factory.Create(new Span<byte>(data));

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }
    }
}

#endif
