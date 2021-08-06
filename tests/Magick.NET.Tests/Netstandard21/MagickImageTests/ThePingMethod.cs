// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.Buffers;
using System.IO;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class ThePingMethod
        {
            public class WithReadOnlySequence
            {
                [Fact]
                public void ShouldThrowExceptionWhenSequenceIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(ReadOnlySequence<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                        image.Ping(new ReadOnlySequence<byte>(bytes));

                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }

                [Fact]
                public void ShouldSupportMultipleSegments()
                {
                    using (var image = new MagickImage())
                    {
                        var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var sequence = TestReadOnlySequence.Create(bytes);

                        image.Ping(sequence);

                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySequenceAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenSequenceIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(ReadOnlySequence<byte>.Empty, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        var bytes = File.ReadAllBytes(Files.CirclePNG);
                        image.Ping(new ReadOnlySequence<byte>(bytes), null);

                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(new ReadOnlySequence<byte>(bytes), settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
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

                    using (var image = new MagickImage())
                    {
                        image.Ping(new ReadOnlySequence<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(Span<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                        image.Ping(new Span<byte>(bytes));

                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }
            }

            public class WithReadOnlySpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(Span<byte>.Empty, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        var bytes = File.ReadAllBytes(Files.CirclePNG);
                        image.Ping(new Span<byte>(bytes), null);

                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(new Span<byte>(bytes), settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
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

                    using (var image = new MagickImage())
                    {
                        image.Ping(new Span<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
                    }
                }
            }
        }
    }
}

#endif
