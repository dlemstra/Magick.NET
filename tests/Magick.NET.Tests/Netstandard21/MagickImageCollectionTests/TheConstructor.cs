// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheConstructor
        {
            public class WithReadonlySequence
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(ReadOnlySequence<byte>.Empty));
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var input = new MagickImageCollection(new ReadOnlySequence<byte>(bytes), readSettings))
                    {
                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithReadonlySequenceAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(ReadOnlySequence<byte>.Empty, MagickFormat.Png));
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(new ReadOnlySequence<byte>(bytes), MagickFormat.Png))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithReadonlySequenceAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(new ReadOnlySequence<byte>(bytes), null))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
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

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var input = new MagickImageCollection(new Span<byte>(bytes), readSettings))
                    {
                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithReadonlySpanAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Span<byte>.Empty, MagickFormat.Png));
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(new Span<byte>(bytes), MagickFormat.Png))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithReadonlySpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

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
