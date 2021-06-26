// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageFactoryTests
    {
        public partial class TheCreateMethod
        {
            public class WithSpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty));
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(new Span<byte>(data)))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }
        }
    }
}

#endif
