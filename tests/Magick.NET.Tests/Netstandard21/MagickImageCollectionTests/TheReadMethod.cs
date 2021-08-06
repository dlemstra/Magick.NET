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
    public partial class MagickImageCollectionTests
    {
        public partial class TheReadMethod
        {
            public class WithReadOnlySequence
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(ReadOnlySequence<byte>.Empty));
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
                        images.Read(new ReadOnlySequence<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }

                [Fact]
                public void ShouldSupportMultipleSegments()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var sequence = TestReadOnlySequence.Create(bytes);

                        images.Read(sequence);

                        Assert.Equal(286, images[0].Width);
                        Assert.Equal(67, images[0].Height);
                    }
                }
            }

            public class WithReadOnlySequenceAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(ReadOnlySequence<byte>.Empty, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new ReadOnlySequence<byte>(bytes), MagickFormat.Png));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }
            }

            public class WithReadOnlySequenceAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentException>("data", () => images.Read(ReadOnlySequence<byte>.Empty, settings));
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

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new ReadOnlySequence<byte>(bytes), settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(new ReadOnlySequence<byte>(bytes), null);

                        Assert.Single(images);
                    }
                }
            }

            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty));
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
                        images.Read(new Span<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            public class WithReadonlySpanAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new Span<byte>(bytes), MagickFormat.Png));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }
            }

            public class WithReadonlySpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty, settings));
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

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new Span<byte>(bytes), settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(new Span<byte>(bytes), null);

                        Assert.Single(images);
                    }
                }
            }
        }
    }
}

#endif
