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
        public partial class ThePingMethod
        {
            public class WithReadOnlySequence
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(ReadOnlySequence<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new ReadOnlySequence<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                        Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
                    }
                }

                [Fact]
                public void ShouldSupportMultipleSegments()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var sequence = TestReadOnlySequence.Create(bytes);

                        images.Ping(sequence);

                        Assert.Single(images);
                        Assert.Equal(286, images[0].Width);
                        Assert.Equal(67, images[0].Height);
                        Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySequencenAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new ReadOnlySequence<byte>(bytes), null);

                        Assert.Single(images);
                        Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(Span<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new Span<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                        Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new Span<byte>(bytes), null);

                        Assert.Single(images);
                        Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
                    }
                }
            }
        }
    }
}

#endif

