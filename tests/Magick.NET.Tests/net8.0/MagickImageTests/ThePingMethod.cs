// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class ThePingMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Ping(ReadOnlySequence<byte>.Empty));
            }

            [Fact]
            public void ShouldPingImage()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var image = new MagickImage();
                image.Ping(new ReadOnlySequence<byte>(bytes));

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public void ShouldSupportMultipleSegments()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                var sequence = TestReadOnlySequence.Create(bytes, 5);
                using var image = new MagickImage();
                image.Ping(sequence);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithReadOnlySequenceAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Ping(ReadOnlySequence<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Ping(new ReadOnlySequence<byte>(bytes), null);

                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(new ReadOnlySequence<byte>(bytes), settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();
                image.Ping(new ReadOnlySequence<byte>(bytes), settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Ping(Span<byte>.Empty));
            }

            [Fact]
            public void ShouldPingImage()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var image = new MagickImage();
                image.Ping(new Span<byte>(bytes));

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithReadOnlySpanAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Ping(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Ping(new Span<byte>(bytes), null);

                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(new Span<byte>(bytes), settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();
                image.Ping(new Span<byte>(bytes), settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }
    }
}

#endif
