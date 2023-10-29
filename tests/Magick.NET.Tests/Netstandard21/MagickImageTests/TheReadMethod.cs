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
    public partial class TheReadMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(ReadOnlySequence<byte>.Empty));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var image = new MagickImage();
                image.Read(new ReadOnlySequence<byte>(bytes));

                Assert.Equal(286, image.Width);
                Assert.Equal(67, image.Height);
            }
        }

        public class WithReadOnlySequenceAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(ReadOnlySequence<byte>.Empty, MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(new ReadOnlySequence<byte>(bytes), MagickFormat.Png));

                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(new ReadOnlySequence<byte>(bytes), MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }

            [Fact]
            public void ShouldSupportMultipleSegments()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                var sequence = TestReadOnlySequence.Create(bytes, 5);
                using var image = new MagickImage();
                image.Read(sequence);

                Assert.Equal(286, image.Width);
                Assert.Equal(67, image.Height);
            }
        }

        public class WithReadOnlySequenceAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(ReadOnlySequence<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();

                image.Read(new ReadOnlySequence<byte>(bytes), null);
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

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(new ReadOnlySequence<byte>(bytes), settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReadingBytes()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(new ReadOnlySequence<byte>(bytes), settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Span<byte>.Empty));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var image = new MagickImage();
                image.Read(new Span<byte>(bytes));

                Assert.Equal(286, image.Width);
                Assert.Equal(67, image.Height);
            }
        }

        public class WithReadOnlySpanAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Span<byte>.Empty, MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(new Span<byte>(bytes), MagickFormat.Png));

                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(new Span<byte>(bytes), MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithReadOnlySpanAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(new Span<byte>(bytes), null);
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

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(new Span<byte>(bytes), settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(new Span<byte>(bytes), settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public partial class WithStream
        {
            [Fact]
            public void ShouldReadImageFromMemoryStreamWhereBufferIsPubliclyVisible()
            {
                var data = File.ReadAllBytes(Files.CirclePNG);
                var testBuffer = new byte[data.Length + 10];
                data.CopyTo(testBuffer, index: 10);

                using var stream = new MemoryStream(testBuffer, index: 10, count: testBuffer.Length - 10, false, true);
                using var image = new MagickImage();
                image.Read(stream);
            }
        }
    }
}

#endif
