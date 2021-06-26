// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheConstructor
        {
            public class WithSpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Span<byte>.Empty));
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var input = new MagickImageCollection(new Span<byte>(bytes), readSettings))
                    {
                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithSpanAndMagickFormat
            {

                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Span<byte>.Empty, MagickFormat.Png));
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(new Span<byte>(bytes), MagickFormat.Png))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithSpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(new Span<byte>(bytes), null))
                    {
                        Assert.Single(images);
                    }
                }
            }
        }
    }
}

#endif
