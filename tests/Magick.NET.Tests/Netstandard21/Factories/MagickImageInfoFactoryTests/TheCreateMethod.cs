// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoFactoryTests
    {
        public partial class TheCreateMethod
        {
            public class WithSpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    var factory = new MagickImageInfoFactory();

                    Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty));
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageInfoFactory();
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);

                    var info = factory.Create(new Span<byte>(data));

                    Assert.IsType<MagickImageInfo>(info);
                    Assert.Equal(123, info.Width);
                }
            }
        }
    }
}

#endif
